using Client;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataModels.Dtos;
using Enums;
using EShopMobile.Helpers;
using EShopMobile.Pages;

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
        private string confirmpassword;

        [ObservableProperty]
        private CustomerDto customer;

        [ObservableProperty]
        private UserDto user;

        [ObservableProperty]
        public bool isLoading;

        [ObservableProperty]
        private INavigation navigation;

        [RelayCommand]
        async void SignIn()
        {
            IsLoading = true;

            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                var message = string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Password) 
                    ? "Email and Password are required"
                    : $"{(string.IsNullOrEmpty(Email) ? "Email" : "Password")} is required";
                IsLoading = false;
                await _alert.DisplayAlert("Login failed", message, "Ok", "Cancel");
                return;
            }

            User = await _client.UserClient.GetAsync("Users/GetUserByEmailAndPassword/?email=" + Email + "&password=" + Password);
            if (User == null)
            {
                IsLoading = false;
                await _alert.DisplayAlert("Login failed", "Invalid email or password, please try again", "Ok", "Cancel");
                return;
            }

            Customer = await _client.CustomerClient.GetAsync("Customers/User/" + User.Id);
            _session.SetCustomer(Customer);
            _session.SetUser(User);
            User.LoginDate = DateTime.Now;
            await _client.UserClient.PutAsync(User, "Users/" + User.Id);
            (Shell.Current as AppShell).ChangeMenu(User?.UserType);
            await Shell.Current.GoToAsync("..");
            IsLoading = false;
        }

        [RelayCommand]
        async void SignUp()
        {
            await Shell.Current.GoToAsync(nameof(SignUpPage));
        }

        [RelayCommand]
        async void SignUpResult()
        {
            if (Customer == null || Customer.Id != 0)
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

            if (Password != Confirmpassword || Password.Length < 6)
            {
                message = Password != Confirmpassword
                ? "Passwords must be identical."
                : "The Password must be at least 6 characters long.";
                IsLoading = false;
                await _alert.DisplayAlert("Registration Issue", message, "Ok", "Cancel");
                return;
            }

            User = new UserDto
            {
                Email = Customer.Email,
                Password = Password,
                UserType = UserType.User,
                RegDate = DateTime.Now,
                LoginDate = DateTime.Now,
            };
            User = await _client.UserClient.PostAsync(User, "Users");
            _session.SetUser(User);
            Customer.UserId = User.Id;
            Customer.RegDate = DateTime.Now;
            Customer = await _client.CustomerClient.PostAsync(Customer, "Customers");
            _session.SetCustomer(Customer);

            var page = Navigation.NavigationStack.LastOrDefault();
            Navigation.RemovePage(page);
            await Shell.Current.GoToAsync("..");
        }
    }
}
