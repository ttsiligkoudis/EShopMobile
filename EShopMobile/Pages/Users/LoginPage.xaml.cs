using EShopMobile.Helpers;
using EShopMobile.ViewModels;

namespace EShopMobile.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var vm = (LoginViewModel)BindingContext;
        vm.Customer = Session.GetCustomer();
        vm.User = Session.GetUser();
        (Shell.Current as AppShell).ChangeMenu(vm.User?.UserType);
        if (vm.Customer != null)
            await Shell.Current.GoToAsync("..");
    }
}