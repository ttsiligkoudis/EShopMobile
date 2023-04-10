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
}