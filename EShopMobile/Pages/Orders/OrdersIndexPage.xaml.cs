using DataModels.Dtos;
using EShopMobile.ViewModels.Orders;

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
        vm.PageNumberStack = PageNumberStack;
        vm.ScrollView = ScrollView;
        vm.GetOrders();
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var vm = (OrderViewModel)BindingContext;
        vm.Order = e.CurrentSelection[0] as OrderDto;
        vm.OrderNavigation();
    }
}