using Client;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataModels.Dtos;
using Enums;
using EShopMobile.Helpers;
using EShopMobile.Pages;
using EShopMobile.Pages.Orders;
using EShopMobile.Pages.Products;
using Helpers;

namespace EShopMobile.ViewModels.Products
{
    [QueryProperty(nameof(Category), nameof(Category))]
    [QueryProperty(nameof(Product), nameof(Product))]
    public partial class ProductsViewModel : ObservableObject
    {
        private readonly ClientHelper _client;

        [ObservableProperty]
        private List<ProductDto> products;

        [ObservableProperty]
        private ProductDto product;

        [ObservableProperty]
        private string category;

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private int quantity = 1;

        [ObservableProperty]
        private decimal finalPrice;

        [ObservableProperty]
        private CustomerDto customer;

        [ObservableProperty]
        private bool createaccount;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string confirmpassword;

        [ObservableProperty]
        private INavigation navigation;

        [ObservableProperty]
        private List<ProductRatesDto> rates;

        [ObservableProperty]
        private Picker categoryPicker;

        [ObservableProperty]
        private Picker pageSizePicker;

        [ObservableProperty]
        private Picker orderByPicker;

        [ObservableProperty]
        private Picker orderTypePicker;

        [ObservableProperty]
        private string freeText;

        [ObservableProperty]
        private List<ProductDto> filteredProducts;

        [ObservableProperty]
        private StackLayout pageNumberStack;

        [ObservableProperty]
        private ScrollView scrollView;

        public ProductsViewModel()
        {
            _client = new ClientHelper();
        }

        [RelayCommand]
        public async void CompleteOrder()
        {
            if (Customer == null)
                return;

            MessageDto message;

            var errors = string.Empty;
            errors += string.IsNullOrEmpty(Customer.Name) ? "Name " : "";
            errors += string.IsNullOrEmpty(Customer.City) ? "City " : "";
            errors += string.IsNullOrEmpty(Customer.Address) ? "Address " : "";
            errors += string.IsNullOrEmpty(Customer.Email) ? "Email " : "";
            errors += string.IsNullOrEmpty(errors) ? "" : "Not Valid";

            if (!string.IsNullOrEmpty(errors))
            {
                await AlertService.DisplayAlert("Registration Issue", errors, "Ok");
                return;
            }

            if (Createaccount && (Password != Confirmpassword || Password.Length < 6))
            {
                errors = Password != Confirmpassword
                ? "Passwords must be identical."
                : "The Password must be at least 6 characters long.";
                IsLoading = false;
                await AlertService.DisplayAlert("Registration Issue", errors, "Ok");
                return;
            }

            if (Createaccount)
            {
                var user = new UserDto
                {
                    Email = Customer.Email,
                    Password = Password,
                    UserType = UserType.User,
                    RegDate = DateTime.Now,
                    LoginDate = DateTime.Now,
                };
                user = await _client.UserClient.PostAsync(user, "Users");
                Session.SetUser(user);
                Customer.UserId = user.Id;

                message = new MessageDto
                {
                    Email = user.Email,
                    Subject = "New User Created",
                    Body = EmailHelper.NewUserCreatedHtml(Customer, user, "New User Created")
                };

                await _client.MessagesClient.PostAsync(message, $"Messages/SendMessage");
            }

            if (Customer.Id == 0)
            {
                Customer.RegDate = DateTime.Now;
                Customer = await _client.CustomerClient.PostAsync(Customer, "Customers");
            }

            if (Createaccount)
                Session.SetCustomer(Customer);

            var order = new OrderDto
            {
                CustomerId = Customer.Id,
                FinalPrice = FinalPrice,
                OrderDate = DateTime.Now,
                Completed = true
            };

            order = await _client.OrderClient.PostAsync(order, "Orders");

            var orderProducts = Products.Select(s => new OrderProductsDto
            {
                ProductId = s.Id,
                ProductName = s.Name,
                Quantity = s.Quantity,
                Price = s.Price,
                OrderId = order.Id
            }).ToList();

            orderProducts = (await _client.OrderProductClient.PostListAsync(orderProducts, $"Orders/{order.Id}/Products")).ToList();
            Session.SetCartProducts(null);

            await _client.ProductClient.PutListAsync(Products, "Products");

            message = new MessageDto
            {
                Email = Customer.Email,
                Subject = "Order Details",
                Body = EmailHelper.CreateOrderHtml(Customer, Products, order.Id, "Order Details")
            };

            await _client.MessagesClient.PostAsync(message, $"Messages/SendMessage");

            await Shell.Current.GoToAsync($"//{nameof(HomePage)}/{nameof(OrderFormPage)}",
                new Dictionary<string, object>
                {
                    ["Order"] = order,
                    [nameof(Products)] = Products,
                    [nameof(FinalPrice)] = FinalPrice,
                });
        }

        [RelayCommand]
        public void Search()
        {
            GetProducts();
        }

        public async void GetProducts(int pageNumber = 1)
        {
            FilteredProducts?.Clear();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                IsLoading = true;
            });

            Category = ((Category)CategoryPicker?.SelectedItem).ToString();

            if (!(Products?.Any() ?? false) || Products.All(w => w.Category != Enum.Parse<Category>(Category)))
            {
                var str = "Products" + (!string.IsNullOrEmpty(Category) ? "/?category=" + (short)Enum.Parse<Category>(Category) : "");
                Products = await _client.ProductClient.GetListAsync(str);
            }
            var result = Products;

            pageNumber = pageNumber > 0 ? pageNumber - 1 : 0;
            var pageSize = (PageSize)PageSizePicker?.SelectedItem;
            var skip = pageNumber * (short)pageSize;
            var take = (short)pageSize;
            Func<ProductDto, bool> wherePredicate = null;

            Func<ProductDto, object> orderPredicate = o => (OrderBy)OrderByPicker?.SelectedItem switch
            {
                OrderBy.Price => o.Price,
                OrderBy.Quantity => o.Quantity,
                OrderBy.Rate => o.Rate,
                _ => o.Name
            };

            if (!string.IsNullOrEmpty(FreeText))
            {
                var search = FreeText.Trim();
                wherePredicate = w => w.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) != -1
                    || (w.Quantity ?? 0).ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) != -1
                    || w.Price.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) != -1
                    || (w.Rate ?? 0).ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) != -1;
            }

            if (wherePredicate != null)
            {
                result = result
                .Where(wherePredicate)
                .ToList();
            }

            if ((OrderType)OrderTypePicker?.SelectedItem == OrderType.Descending)
                result = result.OrderByDescending(orderPredicate).ToList();
            else
                result = result.OrderBy(orderPredicate).ToList();

            var pages = (result.Count / (short)pageSize) + (result.Count % (short)pageSize > 0 ? 1 : 0);
            FilteredProducts = result.Skip(skip).Take(take).ToList();

            PageNumberStack.Clear();
            for (int i = 1; i <= pages; i++)
            {
                var btn = new Button()
                {
                    Text = i.ToString(),
                    TextColor = Colors.White,
                    BackgroundColor = Color.FromHex("6c757d"),
                    Padding = 10,
                    HorizontalOptions = LayoutOptions.Center
                };
                btn.Clicked += PageChanged;
                PageNumberStack.Children.Add(btn);
            }

            MainThread.BeginInvokeOnMainThread(() =>
            {
                IsLoading = false;
            });
        }

        public async void ProductNavigation()
        {
            await Shell.Current.GoToAsync(nameof(ProductFormPage),
                new Dictionary<string, object>
                {
                    [nameof(Product)] = Product
                });
        }

        public async Task GetProductRates()
        {
            if (Product == null)
                return;
            Rates = (await _client.ProductRatesClient.GetListAsync($"Products/Rates/{Product.Id}")).ToList();
        }

        private async void PageChanged(object sender, EventArgs e)
        {
            var btn = sender as Button;
            await ScrollView.ScrollToAsync(0, 0, true);
            GetProducts(Convert.ToInt32(btn.Text));
        }
    }
}
