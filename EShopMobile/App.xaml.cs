
using EShopMobile.Pages;
using EShopMobile.Pages.Orders;
using EShopMobile.ViewModels;
using EShopMobile.ViewModels.Products;

namespace EShopMobile;

public partial class App : Microsoft.Maui.Controls.Application
{
	public App(AppShell appShell)
	{
        //Current.UserAppTheme = AppTheme.Light;
        InitializeComponent();

        MainPage = appShell;
    }

    public static void HandleAppActions(AppAction appAction)
    {
        Current.Dispatcher.Dispatch(async () =>
        {
            var page = appAction.Id switch
            {
                "cart" => new CartPage(new ProductsViewModel()),
                "login" => new LoginPage(new LoginViewModel()),
                _ => default(Microsoft.Maui.Controls.Page)
            };

            if (page != null)
            {
                await Current.MainPage.Navigation.PopToRootAsync();
                await Current.MainPage.Navigation.PushAsync(page);
            }
        });
    }
}
