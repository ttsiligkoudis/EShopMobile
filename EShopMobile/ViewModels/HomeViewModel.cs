using Client;
using CommunityToolkit.Mvvm.ComponentModel;
using DataModels.Dtos;
using EShopMobile.Helpers;
using EShopMobile.Pages.Products;

namespace EShopMobile.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly IClient _client;

        [ObservableProperty]
        private CustomerDto customer;

        [ObservableProperty]
        private UserDto user;

        [ObservableProperty]
        private List<ProductDto> products;

        [ObservableProperty]
        private ProductDto product;

        [ObservableProperty]
        private string category;

        [ObservableProperty]
        private List<ProductDto> savedProducts;

        public HomeViewModel(IClient client)
        {
            _client = client;
            Customer = Session.GetCustomer();
            User = Session.GetUser();
        }

        public async Task GetRandomProducts()
        {
            Products = await _client.GetAsync<List<ProductDto>>("Products/Random/?length=4");
        }

        public async void ProductsNavigation()
        {
            await Shell.Current.GoToAsync(nameof(ProductsIndexPage),
                new Dictionary<string, object>
                {
                    [nameof(Category)] = Category
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
    }
}
