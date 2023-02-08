using EShopMobile.ViewModels;
using EShopMobile.Helpers;
using EShopMobile.ViewModels.Customers;

namespace EShopMobile.Pages.Customers;

public partial class CustomersIndexPage : ContentPage
{
    private Session _session;

    public CustomersIndexPage(CustomersViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _session = new Session();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var vm = (CustomersViewModel)BindingContext;
        vm.Customer = _session.GetCustomer();
        vm.User = _session.GetUser();
        Task.Run(vm.GetCustomers);
    }
}