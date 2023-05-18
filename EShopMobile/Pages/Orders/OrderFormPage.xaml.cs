using EShopMobile.ViewModels.Orders;

namespace EShopMobile.Pages.Orders;

public partial class OrderFormPage : BasePage
{
	public OrderFormPage(OrderViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }

    private void BtnReview_Clicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        var parent = (StackLayout)btn.Parent;
        var productID = ((Label)parent.FindByName("ProductID"))?.Text;
        var vm = (OrderViewModel)BindingContext;
        vm.RateNavigation(productID);
    }
}