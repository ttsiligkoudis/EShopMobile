
using CommunityToolkit.Mvvm.ComponentModel;
using EShopMobile.Helpers;
using DataModels.Dtos;
using EShopMobile.Pages.Orders;
using Client;
using Enums;
using CommunityToolkit.Mvvm.Input;
using System.Text;

namespace EShopMobile.ViewModels.Orders
{
    [QueryProperty(nameof(Order), nameof(Order))]
    [QueryProperty(nameof(Products), nameof(Products))]
    [QueryProperty(nameof(FinalPrice), nameof(FinalPrice))]
    [QueryProperty(nameof(Rate), nameof(Rate))]
    public partial class OrderViewModel : ObservableObject
    {
        private readonly ClientHelper _client;

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private List<OrderDto> orders;

        [ObservableProperty]
        private OrderDto order;

        [ObservableProperty]
        private List<ProductDto> products;

        [ObservableProperty]
        private decimal finalPrice;

        [ObservableProperty]
        private StackLayout pageNumberStack;

        [ObservableProperty]
        private ScrollView scrollView;

        [ObservableProperty]
        private ProductRatesDto rate;

        public OrderViewModel()
        {
            _client = new ClientHelper();
        }

        [RelayCommand]
        public async void SaveRate()
        {
            if (Rate.Id == 0)
            {
                Rate = await _client.ProductRatesClient.PostAsync(Rate, "Products/Rate");
            }
            else
            {
                await _client.ProductRatesClient.PutAsync(Rate, $"Products/Rate/{Rate.Id}");
            }

            await Shell.Current.GoToAsync("..");
        }

        public async void GetOrders(int pageNumber = 1)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                IsLoading = true;
            });

            var customer = Session.GetCustomer();
            var user = Session.GetUser();

            if (customer == null || user == null)
                return;

            var str = "Orders" + (user.UserType == UserType.User ? $"/Customer/{customer.Id}" : string.Empty);
            var result = await _client.OrderClient.GetListAsync(str);

            pageNumber = pageNumber > 0 ? pageNumber - 1 : 0;
            var pageSize = PageSize.Ten;
            var skip = pageNumber * (short)pageSize;
            var take = (short)pageSize;

            var pages = (result.Count / (short)pageSize) + (result.Count % (short)pageSize > 0 ? 1 : 0);
            Orders = result.Skip(skip).Take(take).ToList();

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

        public async void OrderNavigation()
        {
            if (Order == null)
                return;

            var orderProducts = await _client.OrderProductClient.GetListAsync($"Orders/{Order.Id}/Products");
            Products = orderProducts.Select(s => new ProductDto
            {
                Id = s.ProductId,
                Name = s.ProductName,
                Price = s.Price,
                Quantity = s.Quantity,
                Image = s.Product.Image,
                Category = s.Product.Category,
                Description = s.Product.Description
            }).ToList();

            FinalPrice = Products.Select(s => s.Price * s.Quantity ?? 0).Sum();

            await Shell.Current.GoToAsync(nameof(OrderFormPage),
                new Dictionary<string, object>
                {
                    [nameof(Order)] = Order,
                    [nameof(Products)] = Products,
                    [nameof(FinalPrice)] = FinalPrice,
                });
        }

        private async void PageChanged(object sender, EventArgs e)
        {
            var btn = sender as Button;
            await ScrollView.ScrollToAsync(0, 0, true);
            GetOrders(Convert.ToInt32(btn.Text));
        }

        public async void RateNavigation(string productID)
        {
            Rate = await _client.ProductRatesClient.GetAsync($"Products/Rate/?productId={productID}&customerId={Order.CustomerId}");
            Rate ??= new ProductRatesDto
            {
                CustomerId = Order.CustomerId,
                ProductId = Convert.ToInt32(productID)
            };
            Rate.CustomerName = Order.CustomerName;
            Rate.ProductName = Products.FirstOrDefault(w => w.Id == Rate.ProductId)?.Name;

            await Shell.Current.GoToAsync(nameof(RatePage),
                new Dictionary<string, object>
                {
                    [nameof(Rate)] = Rate
                });
        }
    }
}
