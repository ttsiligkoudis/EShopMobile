using CommunityToolkit.Mvvm.ComponentModel;
using EShopMobile.Models;
using EShopMobile.Helpers;
using CommunityToolkit.Mvvm.Input;
using EShopMobile.Pages.Customers;
using EShopMobile.Pages.Products;
using EShopMobile.Pages.Orders;

namespace EShopMobile.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly ClientHelper _client;
        private readonly Session _session;
        private readonly IAlertService _alert;

        [ObservableProperty]
        private Customer customer;

        [ObservableProperty]
        private User user;

        public HomeViewModel(IAlertService alert)
        {
            _client = new ClientHelper();
            _session = new Session();
            _alert = alert;
            Customer = _session.GetCustomer();
            User = _session.GetUser();
        }

        [RelayCommand]
        async void Logout()
        {
            Preferences.Remove(nameof(Customer));
            Preferences.Remove(nameof(User));
            await Shell.Current.GoToAsync("///" + nameof(LoginPage));
        }

        [RelayCommand]
        async void CustomersNavigation()
        {
            await Shell.Current.GoToAsync(nameof(CustomersIndexPage));
        }

        [RelayCommand]
        async void OrdersNavigation()
        {
            await Shell.Current.GoToAsync(nameof(OrdersIndexPage));
        }

        [RelayCommand]
        async void ProductsNavigation()
        {
            await Shell.Current.GoToAsync(nameof(ProductsIndexPage));
        }
    }
}
