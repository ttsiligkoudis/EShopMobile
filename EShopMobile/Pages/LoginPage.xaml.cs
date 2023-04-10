using EShopMobile.Helpers;
using EShopMobile.ViewModels;
using DataModels;

namespace EShopMobile.Pages;

public partial class LoginPage : ContentPage
{
    private Session _session;
    public Style MauiStyle { get; }

    public LoginPage(LoginViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _session = new Session();
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var vm = (LoginViewModel)BindingContext;
        vm.Customer = _session.GetCustomer();
        vm.User = _session.GetUser();
        (Shell.Current as AppShell).ChangeMenu(vm.User?.UserType);
        if (vm.Customer != null)
            await Shell.Current.GoToAsync("..");
    }
}