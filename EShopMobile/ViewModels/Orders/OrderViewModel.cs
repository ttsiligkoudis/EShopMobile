
using CommunityToolkit.Mvvm.ComponentModel;
using EShopMobile.Helpers;
using DataModels.Dtos;
using EShopMobile.Pages.Orders;
using Client;

namespace EShopMobile.ViewModels.Orders
{
    [QueryProperty(nameof(Order), nameof(Order))]
    [QueryProperty(nameof(Products), nameof(Products))]
    [QueryProperty(nameof(FinalPrice), nameof(FinalPrice))]
    public partial class OrderViewModel : ObservableObject
    {
        private readonly ClientHelper _client;
        private readonly Session _session;

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

        public OrderViewModel()
        {
            _client = new ClientHelper();
            _session = new Session();
        }

        public async Task GetOrders()
        {
            IsLoading = true;
            var customer = _session.GetCustomer();
            var user = _session.GetUser();
            if (customer != null && user != null)
            {
                Orders = user.UserType == Enums.UserType.Admin 
                    ? (await _client.OrderClient.GetListAsync("Orders")).ToList()
                    : (await _client.OrderClient.GetListAsync($"Orders/Customer/{customer.Id}")).ToList();
            }
            IsLoading = false;
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
    }
}
