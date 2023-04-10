using DataModels.Dtos;
using EShopMobile.ViewModels;
using EShopMobile.ViewModels.Products;

namespace EShopMobile.Pages.Products;

public partial class ProductsIndexPage : BasePage
{
	public ProductsIndexPage(ProductsViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var vm = (ProductsViewModel)BindingContext;
        if (!(vm.Products?.Any() ?? false))
            Task.Run(vm.GetProducts);
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var vm = (ProductsViewModel)BindingContext;
        vm.Product = e.CurrentSelection[0] as ProductDto;
        vm.ProductNavigation();
    }
}