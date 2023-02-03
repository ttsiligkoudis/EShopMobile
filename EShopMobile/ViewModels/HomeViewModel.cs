using CommunityToolkit.Mvvm.ComponentModel;
using EShopMobile.Models;
using EShopMobile.Helpers;
using CommunityToolkit.Mvvm.Input;
using EShopMobile.Pages.Customers;

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
    }
}
