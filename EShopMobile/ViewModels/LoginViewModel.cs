using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EShopMobile.Models;
using EShopMobile.Helpers;

namespace EShopMobile.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly ClientHelper _client;
        private readonly Session _session;
        private readonly IAlertService _alert;

        public LoginViewModel(IAlertService alert)
        {
            _client = new ClientHelper();
            _session = new Session();
            _alert = alert;
            
        }
        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private Customer customer;

        [ObservableProperty]
        private User user;

        [RelayCommand]
        async void Login()
        {
            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
            {
                User = await _client.UserClient.GetAsync("Users/GetUserByEmailAndPassword/?email=" + Email + "&password=" + Password);

                if (User == null)
                {
                    await _alert.DisplayAlert("Login failed", "Invalid email or password, please try again", "Ok", "Cancel");
                }
                else
                {
                    Customer = await _client.CustomerClient.GetAsync("Customers/User/" + User.Id);
                    if (Customer != null)
                    {
                        _session.SetCustomer(Customer);
                        _session.SetUser(User);
                    }
                    User.LoginDate = DateTime.Now;
                    await _client.UserClient.PutAsync(User, "Users/" + User.Id);

                    await Shell.Current.GoToAsync(nameof(HomePage));
                }
            }
        }
    }
}
