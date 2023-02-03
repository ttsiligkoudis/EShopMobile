using CommunityToolkit.Maui;
using EShopMobile.Helpers;
using EShopMobile.Pages.Customers;
using EShopMobile.Pages.Orders;
using EShopMobile.Pages.Products;
using EShopMobile.Pages.Users;
using EShopMobile.ViewModels;
namespace EShopMobile;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddTransient<LoginPage>();
		builder.Services.AddTransient<HomePage>();
		builder.Services.AddTransient<CustomersIndexPage>();
        builder.Services.AddTransient<CustomerFormPage>();
        builder.Services.AddTransient<OrderCreationPage>();
        builder.Services.AddTransient<OrdersIndexPage>();
        builder.Services.AddTransient<OrderFormPage>();
        builder.Services.AddTransient<ProductsIndexPage>();
        builder.Services.AddTransient<ProductFormPage>();
        builder.Services.AddTransient<UsersIndexPage>();
        builder.Services.AddTransient<UserFormPage>();

        builder.Services.AddTransient<LoginViewModel>();
		builder.Services.AddTransient<HomeViewModel>();

        builder.Services.AddSingleton<IAlertService, AlertService>();

        return builder.Build();
	}
}
