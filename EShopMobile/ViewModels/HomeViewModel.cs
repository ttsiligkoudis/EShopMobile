using Client;
using CommunityToolkit.Mvvm.ComponentModel;
using DataModels.Dtos;
using EShopMobile.Helpers;
using EShopMobile.Pages.Products;

namespace EShopMobile.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly ClientHelper _client;
        private readonly Session _session;

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

        public HomeViewModel()
        {
            _client = new ClientHelper();
            _session = new Session();
            Customer = _session.GetCustomer();
            User = _session.GetUser();
        }

        public async void GetRandomProducts()
        {
            Products = (await _client.ProductClient.GetListAsync("Products/Random/?length=4")).ToList();
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
