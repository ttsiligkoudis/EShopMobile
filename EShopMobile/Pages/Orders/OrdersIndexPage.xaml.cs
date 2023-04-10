using DataModels.Dtos;
using EShopMobile.ViewModels.Orders;
using EShopMobile.ViewModels.Products;

namespace EShopMobile.Pages.Orders;

public partial class OrdersIndexPage : BasePage
{
	public OrdersIndexPage(OrderViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var vm = (OrderViewModel)BindingContext;
        if (!(vm.Orders?.Any() ?? false))
            Task.Run(vm.GetOrders);
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var vm = (OrderViewModel)BindingContext;
        vm.Order = e.CurrentSelection[0] as OrderDto;
        vm.OrderNavigation();
    }
}