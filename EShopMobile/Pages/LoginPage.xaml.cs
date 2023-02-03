namespace EShopMobile;

using EShopMobile.Helpers;
using EShopMobile.ViewModels;

public partial class LoginPage : ContentPage
{
    private Session _session;

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

        if (vm.Customer != null)
        {
            await Shell.Current.GoToAsync(nameof(HomePage));
        }
    }
}

