using DataModels.Dtos;
using Enums;
using EShopMobile.Helpers;
using EShopMobile.Pages.Products;
using EShopMobile.ViewModels;

namespace EShopMobile.Pages;

public partial class HomePage : BasePage
{

    public HomePage(HomeViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        Task.Run(MauiProgram.CheckLocationPermission);
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var vm = (HomeViewModel)BindingContext;
        vm.SavedProducts = Session.GetSavedProducts();
        Wishlist.IsVisible = vm.SavedProducts?.Any() ?? false;
        await vm.GetRandomProducts().ConfigureAwait(false);
        vm.Customer = Session.GetCustomer();
        vm.User = Session.GetUser();
    }

    private void ProductsNavBtn(object sender, EventArgs e)
    {
        var btn = (ImageButton)sender;
        var vm = (HomeViewModel)BindingContext;
        vm.Category = btn.StyleId;
        vm.ProductsNavigation();
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var vm = (HomeViewModel)BindingContext;
        vm.Product = e.CurrentSelection[0] as ProductDto;
        vm.ProductNavigation();
    }
}