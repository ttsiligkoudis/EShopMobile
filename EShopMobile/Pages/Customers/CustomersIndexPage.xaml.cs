using EShopMobile.ViewModels.Customers;

namespace EShopMobile.Pages.Customers;

public partial class CustomersIndexPage : BasePage
{

    public CustomersIndexPage(CustomersViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var vm = (CustomersViewModel)BindingContext;
        if (!(vm.Customers?.Any() ?? false))
        {
            Task.Run(vm.GetCustomers);
        }
    }
}