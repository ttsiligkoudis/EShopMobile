using Client;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataModels.Dtos;
using Enums;
using EShopMobile.Helpers;
using EShopMobile.Pages.Orders;
using EShopMobile.Pages.Products;

namespace EShopMobile.ViewModels.Products
{
    [QueryProperty(nameof(Category), nameof(Category))]
    [QueryProperty(nameof(Product), nameof(Product))]
    public partial class ProductsViewModel : ObservableObject
    {
        private readonly ClientHelper _client;
        private readonly Session _session;
        private readonly IAlertService _alert;

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

        public ProductsViewModel(IAlertService alert)
        {
            _client = new ClientHelper();
            _session = new Session();
            _alert = alert;
        }

        [RelayCommand]
        public async void CompleteOrder()
        {
            if (Customer == null)
                return;
            
            var message = string.Empty;
            message += string.IsNullOrEmpty(Customer.Name) ? "Name " : "";
            message += string.IsNullOrEmpty(Customer.City) ? "City " : "";
            message += string.IsNullOrEmpty(Customer.Address) ? "Address " : "";
            message += string.IsNullOrEmpty(Customer.Email) ? "Email " : "";
            message += string.IsNullOrEmpty(message) ? "" : "Not Valid";

            if (!string.IsNullOrEmpty(message))
            {
                await _alert.DisplayAlert("Registration Issue", message, "Ok", "Cancel");
                return;
            }

            if (Createaccount && (Password != Confirmpassword || Password.Length < 6))
            {
                message = Password != Confirmpassword
                ? "Passwords must be identical."
                : "The Password must be at least 6 characters long.";
                IsLoading = false;
                await _alert.DisplayAlert("Registration Issue", message, "Ok", "Cancel");
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
                _session.SetUser(user);
                Customer.UserId = user.Id;
            }

            if (Customer.Id == 0)
            {
                Customer.RegDate = DateTime.Now;
                Customer = await _client.CustomerClient.PostAsync(Customer, "Customers");
            }

            if (Createaccount)
                _session.SetCustomer(Customer);

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
            _session.SetCartProducts(null);

            await _client.ProductClient.PutListAsync(Products, "Products");
            
            await Shell.Current.GoToAsync("//Home/" + nameof(OrderFormPage),
                new Dictionary<string, object>
                {
                    ["Order"] = order,
                    [nameof(Products)] = Products,
                    [nameof(FinalPrice)] = FinalPrice,
                });
        }

        public async Task GetProducts()
        {
            IsLoading = true;
            var str = "Products" + (!string.IsNullOrEmpty(Category) ? "/?category=" + (short)Enum.Parse<Category>(Category) : "");
            Products = (await _client.ProductClient.GetListAsync(str)).ToList();
            IsLoading = false;
        }

        public async void ProductNavigation()
        {
            await Shell.Current.GoToAsync(nameof(ProductFormPage),
                new Dictionary<string, object>
                {
                    [nameof(Product)] = Product
                });
        }

        public async void GetProductRates()
        {
            if (Product == null)
                return;
            Rates = (await _client.ProductRatesClient.GetListAsync($"Products/Rates/{Product.Id}")).ToList();
        }
    }
}
