using EShopMobile.ViewModels.Orders;

namespace EShopMobile.Pages.Orders;

public partial class RatePage : BasePage
{
	public RatePage(OrderViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}