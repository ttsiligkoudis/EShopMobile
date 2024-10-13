
using EShopMobile.Pages;
using EShopMobile.Pages.Orders;
using EShopMobile.ViewModels;
using EShopMobile.ViewModels.Products;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace EShopMobile;

public partial class App : Application
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
            var httpClient = new HttpClient();
            var client = new Client.Client(httpClient);

            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.appsettings.json");
            var config = new ConfigurationBuilder().AddJsonStream(stream).Build();

            var page = appAction.Id switch
            {
                "cart" => new CartPage(new ProductsViewModel(client)),
                "login" => new LoginPage(new LoginViewModel(client, config)),
                _ => default(Page)
            };

            if (page != null)
            {
                await Current.MainPage.Navigation.PopToRootAsync();
                await Current.MainPage.Navigation.PushAsync(page);
            }
        });
    }
}
