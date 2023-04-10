using EShopMobile.ViewModels;

namespace EShopMobile.Pages;

public partial class SignUpPage : ContentPage
{
	public SignUpPage(LoginViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
		var vm = (LoginViewModel)BindingContext;
		vm.Customer ??= new();
		vm.Navigation = Navigation;
    }
}