using DataModels.Dtos;
using EShopMobile.Helpers;
using EShopMobile.ViewModels;

namespace EShopMobile.Pages;

public partial class SavedPage : ContentPage
{
	private readonly Session _session;

	public SavedPage(HomeViewModel vm)
	{
		InitializeComponent();
		_session = new Session();
		BindingContext = vm;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
		var vm = (HomeViewModel)BindingContext;
		vm.SavedProducts = _session.GetSavedProducts();
        Wishlist.IsVisible = vm.SavedProducts?.Any() ?? false;
        Task.Run(vm.GetRandomProducts);
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var vm = (HomeViewModel)BindingContext;
        vm.Product = e.CurrentSelection[0] as ProductDto;
        vm.ProductNavigation();
    }
}