using DataModels.Dtos;
using EShopMobile.Helpers;
using EShopMobile.ViewModels;

namespace EShopMobile.Pages;

public partial class SavedPage : ContentPage
{
	public SavedPage(HomeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
		var vm = (HomeViewModel)BindingContext;
		vm.SavedProducts = Session.GetSavedProducts();
        Wishlist.IsVisible = vm.SavedProducts?.Any() ?? false;
        await vm.GetRandomProducts().ConfigureAwait(false);
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var vm = (HomeViewModel)BindingContext;
        vm.Product = e.CurrentSelection[0] as ProductDto;
        vm.ProductNavigation();
    }
}