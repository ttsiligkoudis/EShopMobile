using EShopMobile.ViewModels;

namespace EShopMobile.Pages.Users;

public partial class ResetPasswordPage : ContentPage
{
	public ResetPasswordPage(LoginViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var vm = (LoginViewModel)BindingContext;
        vm.Navigation = Navigation;
        var authorized = await vm.CheckUsersToken();
        if (!authorized)
        {
            var page = Navigation.NavigationStack.LastOrDefault();
            await Shell.Current.GoToAsync(nameof(ForgotPasswordPage));
            Navigation.RemovePage(page);
            var msg = "The link is not valid please fill your email to try again";
            await DisplayAlert("Error", msg, "Ok", "Cancel");
        }
    }
}