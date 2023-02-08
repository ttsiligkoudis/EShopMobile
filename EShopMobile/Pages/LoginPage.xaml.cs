using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui.Behaviors;
using EShopMobile.Helpers;
using EShopMobile.ViewModels;

namespace EShopMobile;
public partial class LoginPage : ContentPage
{
    private Session _session;
    public Style MauiStyle { get; }

    public LoginPage(LoginViewModel vm)
	{
        InitializeComponent();
        BindingContext = vm;
        _session = new Session();
        UserName.Behaviors.Add(new EmailValidationBehavior
        {
            InvalidStyle = new Style<Entry>(Entry.TextColorProperty, Colors.Red),
            ValidStyle = new Style<Entry>(Entry.TextColorProperty, Colors.Black),
            Flags = ValidationFlags.ValidateOnUnfocusing,
        });
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

