using EShopMobile.Pages.Customers;
using EShopMobile.Pages.Orders;
using EShopMobile.Pages.Products;
using EShopMobile.Pages.Users;

namespace EShopMobile;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(CustomersIndexPage), typeof(CustomersIndexPage));
        Routing.RegisterRoute(nameof(CustomerFormPage), typeof(CustomerFormPage));
        Routing.RegisterRoute(nameof(OrderCreationPage), typeof(OrderCreationPage));
        Routing.RegisterRoute(nameof(OrdersIndexPage), typeof(OrdersIndexPage));
        Routing.RegisterRoute(nameof(OrderFormPage), typeof(OrderFormPage));
        Routing.RegisterRoute(nameof(ProductsIndexPage), typeof(ProductsIndexPage));
        Routing.RegisterRoute(nameof(ProductFormPage), typeof(ProductFormPage));
        Routing.RegisterRoute(nameof(UsersIndexPage), typeof(UsersIndexPage));
        Routing.RegisterRoute(nameof(UserFormPage), typeof(UserFormPage));
    }
}
