using EShopMobile.ViewModels;
using EShopMobile.Helpers;
using DataModels.Dtos;

namespace EShopMobile.Pages;

public partial class MyProfilePage : BasePage
{
    private Session _session;

	public MyProfilePage(HomeViewModel vm)
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
        if (vm.User == null)
        {
            Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }

    private async void LoginBtn_Clicked(object sender, EventArgs e)
    {
        Preferences.Remove(nameof(CustomerDto));
        Preferences.Remove(nameof(UserDto));
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }
}