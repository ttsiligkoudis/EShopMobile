using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common;
using Client;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataModels.Dtos;
using Enums;
using EShopMobile.Helpers;
using EShopMobile.Pages;
using Helpers;
using Android.Gms.Auth.Api;
using Android.Gms.Common.Apis;
using static Android.Gms.Common.Apis.GoogleApiClient;
using Microsoft.Extensions.Configuration;

namespace EShopMobile.ViewModels
{
    [QueryProperty(nameof(UserID), nameof(UserID))]
    [QueryProperty(nameof(Token), nameof(Token))]
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IClient _client;
        private readonly IConfiguration _configuration;

        public LoginViewModel(IClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
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

        [ObservableProperty]
        private int userID;

        [ObservableProperty]
        private string token;

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
                await AlertService.DisplayAlert("Login failed", message, "Ok");
                return;
            }

            User = await _client.GetAsync<UserDto>("Users/GetUserByEmailAndPassword/?email=" + Email + "&password=" + Password);
            if (User == null)
            {
                IsLoading = false;
                await AlertService.DisplayAlert("Login failed", "Invalid email or password, please try again", "Ok");
                return;
            }

            Customer = await _client.GetAsync<CustomerDto>("Customers/User/" + User.Id);
            Session.SetCustomer(Customer);
            Session.SetUser(User);
            User.LoginDate = DateTime.Now;
            await _client.PutAsync(User, "Users/" + User.Id);
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

            if (Password != Confirmpassword || Password.Length < 6)
            {
                errors = Password != Confirmpassword
                ? "Passwords must be identical."
                : "The Password must be at least 6 characters long.";
                IsLoading = false;
                await AlertService.DisplayAlert("Registration Issue", errors, "Ok");
                return;
            }

            IsLoading = true;
            var user = await _client.GetAsync<UserDto>("Users/GetUserByEmail/?email=" + Customer.Email);
            if (user != null)
            {
                IsLoading = false;
                errors = "Email already exists";
                await AlertService.DisplayAlert("Registration Issue", errors, "Ok");
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
            User = await _client.PostAsync(User, "Users");
            Session.SetUser(User);
            Customer.UserId = User.Id;
            Customer.RegDate = DateTime.Now;
            Customer = await _client.PostAsync(Customer, "Customers");
            Session.SetCustomer(Customer);

            var message = new MessageDto
            {
                Email = User.Email,
                Subject = "New User Created",
                Body = EmailHelper.NewUserCreatedHtml(Customer, User, "New User Created")
            };

            await _client.PostAsync(message, $"Messages/SendMessage");

            IsLoading = false;
            var page = Navigation.NavigationStack.LastOrDefault();
            Navigation.RemovePage(page);
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        async void ForgotPassword()
        {
            await Shell.Current.GoToAsync(nameof(ForgotPasswordPage));
        }

        [RelayCommand]
        async void ForgotPasswordResult()
        {
            string msg;
            if (string.IsNullOrWhiteSpace(Email))
            {
                msg = "Email is required";
                await AlertService.DisplayAlert("Error", msg, "Ok");
                return;
            }

            IsLoading = true;
            var user = await _client.GetAsync<UserDto>("Users/GetUserByEmail/?email=" + Email);
            if (user == null)
            {
                msg = "Email does not exist";
                await AlertService.DisplayAlert("Error", msg, "Ok");
                return;
            }

            var token = Guid.NewGuid().ToString();
            user.PasswordResetToken = token;
            await _client.PutAsync(user, "Users/" + user.Id);

            var message = new MessageDto
            {
                Email = user.Email,
                Subject = "Password Reset",
                Body = EmailHelper.PasswordResetHtml(user, "Password Reset")
            };

            await _client.PostAsync(message, $"Messages/SendMessage");

            IsLoading = false;

            msg = "An email was sent successfully, please follow the email instructions to reset your password.";
            await AlertService.DisplayAlert("Operation Completed Successfully", msg, "Ok");

            await Shell.Current.GoToAsync("..");
        }

        public async Task<bool> CheckUsersToken()
        {
            var user = await _client.GetAsync<UserDto>("Users/" + UserID);
            Email = user?.Email;
            Password = string.Empty;
            Confirmpassword = string.Empty;
            return user?.PasswordResetToken == Token;
        }

        [RelayCommand]
        async void ResetPasswordResult()
        {
            string msg;
            if (Password != Confirmpassword || Password.Length < 6)
            {
                msg = Password != Confirmpassword
                ? "Passwords must be identical."
                : "The Password must be at least 6 characters long.";
                await AlertService.DisplayAlert("Error", msg, "Ok");
                return;
            }

            IsLoading = true;
            var user = await _client.GetAsync<UserDto>("Users/GetUserByEmail/?email=" + Email);

            if (user == null || user.PasswordResetToken != Token)
            {
                IsLoading = false;
                msg = "The link is not valid please fill your email to try again";
                await AlertService.DisplayAlert("Error", msg, "Ok");
            }

            user.Password = Password;
            user.PasswordResetToken = null;
            await _client.PutAsync(user, "Users/" + user.Id);

            Session.SetUser(null);
            Session.SetCustomer(null);
            IsLoading = false;
            var page = Navigation.NavigationStack.LastOrDefault();
            await Shell.Current.GoToAsync(nameof(LoginPage));
            Navigation.RemovePage(page);
            msg = "Password changed successfully, please procced with log in";
            await AlertService.DisplayAlert("Operation Completed Successfully", msg, "Ok");
        }

        [RelayCommand]
        public async void GoogleLogin()
        {
            try
            {
                var googleSignInOptions = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                    .RequestIdToken(_configuration["Authentication:Google:ClientId"])
                    .RequestEmail()
                    .Build();

                var googleApiClient = new Builder(MauiApplication.Current.ApplicationContext)
                    .AddApi(Auth.GOOGLE_SIGN_IN_API, googleSignInOptions)
                    .AddScope(new Scope(Scopes.Profile))
                    .Build();

                var signInIntent = Auth.GoogleSignInApi.GetSignInIntent(googleApiClient);

                Platform.CurrentActivity.StartActivityForResult(signInIntent, 1);

                googleApiClient.Connect();
            }
            catch (Exception ex)
            {
                await AlertService.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
