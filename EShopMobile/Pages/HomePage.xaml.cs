using EShopMobile.Helpers;
using EShopMobile.Models;
using EShopMobile.ViewModels;

namespace EShopMobile;

public partial class HomePage : ContentPage
{
    private Session _session;

    public HomePage(HomeViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
        _session = new Session();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var vm = (HomeViewModel)BindingContext;
        vm.Customer = _session.GetCustomer();
        vm.User = _session.GetUser();
    }
}