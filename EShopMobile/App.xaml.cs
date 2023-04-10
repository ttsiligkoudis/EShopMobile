using EShopMobile.Pages.Orders;
using EShopMobile.ViewModels.Products;

namespace EShopMobile;

public partial class App : Application
{
	public App(AppShell appShell)
	{
        //Current.UserAppTheme = AppTheme.Light;
        InitializeComponent();

		MainPage = appShell;
	}
}
