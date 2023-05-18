using EShopMobile.Helpers;
using EShopMobile.ViewModels;

namespace EShopMobile.Pages;

public partial class ForgotPasswordPage : ContentPage
{
    public ForgotPasswordPage(LoginViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var vm = (LoginViewModel)BindingContext;
    }
}