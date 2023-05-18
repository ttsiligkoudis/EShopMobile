using EShopMobile.Pages;
using EShopMobile.Pages.Customers;
using EShopMobile.Pages.Orders;
using EShopMobile.Pages.Products;
using EShopMobile.Helpers;
using DataModels.Dtos;
using Enums;
using EShopMobile.Pages.Users;
using System.Runtime.CompilerServices;

namespace EShopMobile;

public partial class AppShell : Shell
{
    public AppShell()
	{
        InitializeComponent();

        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(CustomersIndexPage), typeof(CustomersIndexPage));
        Routing.RegisterRoute(nameof(OrdersIndexPage), typeof(OrdersIndexPage));
        Routing.RegisterRoute(nameof(OrderFormPage), typeof(OrderFormPage));
        Routing.RegisterRoute(nameof(ProductsIndexPage), typeof(ProductsIndexPage));
        Routing.RegisterRoute(nameof(ProductFormPage), typeof(ProductFormPage));
        Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
        Routing.RegisterRoute(nameof(ContactPage), typeof(ContactPage));
        Routing.RegisterRoute(nameof(MyProfilePage), typeof(MyProfilePage));
        Routing.RegisterRoute(nameof(CartPage), typeof(CartPage));
        Routing.RegisterRoute(nameof(SavedPage), typeof(SavedPage));
        Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
        Routing.RegisterRoute(nameof(ForgotPasswordPage), typeof(ForgotPasswordPage));
        Routing.RegisterRoute(nameof(ResetPasswordPage), typeof(ResetPasswordPage));
        Routing.RegisterRoute(nameof(RatePage), typeof(RatePage));
        Routing.RegisterRoute(nameof(AuthenticationPage), typeof(AuthenticationPage));
    }

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        if (propertyName == "CurrentItem" && CurrentItem.Route.Contains("Page"))
        {
            var routes = new List<string>
            {
                nameof(CustomersIndexPage),
                nameof(OrdersIndexPage),
                nameof(ProductsIndexPage)
            };

            if (routes.Contains(CurrentItem.Route))
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Current.GoToAsync("//" + CurrentItem.Route);
                });
            }
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var user = Session.GetUser();
        ChangeMenu(user?.UserType);
    }

    protected override bool OnBackButtonPressed()
    {
        var page = Navigation.NavigationStack.LastOrDefault();
        if (CurrentPage.Title != "Home" && page == null)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Current.GoToAsync("//" + nameof(HomePage));
            });
            return true;
        }
        return base.OnBackButtonPressed();
    }

    private async void LogBtn_Clicked(object sender, EventArgs e)
    {
        Session.SetCustomer(null);
        Session.SetUser(null);
        await Current.GoToAsync(nameof(LoginPage));
        Current.FlyoutIsPresented = false;
    }

    public void ChangeMenu(UserType? userType)
    {
        Customers.IsVisible = userType.HasValue && userType.Value.HasFlag(UserType.Admin);
        Orders.IsVisible = userType.HasValue;
        LogoutBtn.IsVisible = userType.HasValue;
        LoginBtn.IsVisible = !userType.HasValue;
    }
}
