using EShopMobile.Pages;
using EShopMobile.Pages.Customers;
using EShopMobile.Pages.Orders;
using EShopMobile.Pages.Products;
using EShopMobile.Pages.Users;
using EShopMobile.ViewModels;
using EShopMobile.ViewModels.Customers;
using EShopMobile.ViewModels.Orders;
using EShopMobile.ViewModels.Products;
using Maui.GoogleMaps.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Reflection;

namespace EShopMobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
	{
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
#if ANDROID
            .UseGoogleMaps()
#elif IOS
            .UseGoogleMaps("AIzaSyBVFIHfFViJdjlWHxnds2M8de8DFbK0Zjg")
#endif
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("FontAwesomeRegular.otf", "FontRegular");
                fonts.AddFont("FontAwesomeBrands.otf", "FontBrands");
                fonts.AddFont("FontAwesomeSolid.otf", "FontSolid");
            })
            .ConfigureEssentials(essentials =>
            {
                essentials
                    .AddAppAction("cart", "Cart")
                    .AddAppAction("login", "Login")
                    .OnAppAction(App.HandleAppActions);
            });

        builder.Services.AddTransient<AppShell>();
        builder.Services.AddTransient<LoginPage>();
		builder.Services.AddTransient<HomePage>();
		builder.Services.AddTransient<CustomersIndexPage>();
        builder.Services.AddTransient<OrdersIndexPage>();
        builder.Services.AddTransient<OrderFormPage>();
        builder.Services.AddTransient<ProductsIndexPage>();
        builder.Services.AddTransient<ProductFormPage>();
        builder.Services.AddTransient<AboutPage>();
        builder.Services.AddTransient<ContactPage>();
        builder.Services.AddTransient<MyProfilePage>();
        builder.Services.AddTransient<CartPage>();
        builder.Services.AddTransient<SavedPage>();
        builder.Services.AddTransient<SignUpPage>();
        builder.Services.AddTransient<ForgotPasswordPage>();
        builder.Services.AddTransient<ResetPasswordPage>();
        builder.Services.AddTransient<RatePage>();
        builder.Services.AddTransient<AuthenticationPage>();

        builder.Services.AddTransient<LoginViewModel>();
		builder.Services.AddTransient<HomeViewModel>();
        builder.Services.AddTransient<CustomersViewModel>();
        builder.Services.AddTransient<ProductsViewModel>();
        builder.Services.AddTransient<OrderViewModel>();

        builder.Services.AddScoped<Client.IClient, Client.Client>(provider =>
        {
            var httpClient = new HttpClient();
            return new Client.Client(httpClient);
        });

        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.appsettings.json");
        var config = new ConfigurationBuilder().AddJsonStream(stream).Build();
        builder.Configuration.AddConfiguration(config);

        return builder.Build();
	}

    public static async Task<bool> GetLocationPermission()
    {
        var LocationAlwaysPermission = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();
        var LocationWhenInUsePermission = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
        return LocationAlwaysPermission.HasFlag(PermissionStatus.Granted) || LocationWhenInUsePermission.HasFlag(PermissionStatus.Granted);
    }

    public static void RequestLocationPermission()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
        });
    }

    public static async Task<bool> CheckLocationPermission()
    {
        var hasPermission = await GetLocationPermission();

        if (!hasPermission)
        {
            RequestLocationPermission();
            hasPermission = await GetLocationPermission();
        }
        return hasPermission;
    }

    public static T Clone<T>(this T source)
    {
        var serialized = JsonConvert.SerializeObject(source);
        return JsonConvert.DeserializeObject<T>(serialized);
    }
}
