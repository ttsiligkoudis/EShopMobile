using DataModels.Dtos;
using Enums;
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

        vm.PageNumberStack = PageNumberStack;
        vm.ScrollView = ScrollView;

        if (vm.CategoryPicker == null)
        {
            CategoryPicker.ItemsSource = (from d in Enum.GetValues(typeof(Category)).Cast<Category>() select d).ToList();
            CategoryPicker.SelectedItem = string.IsNullOrEmpty(vm.Category) ? default(Category) : Enum.Parse<Category>(vm.Category);
            CategoryPicker.SelectedIndex = string.IsNullOrEmpty(vm.Category) ? 0 : ((short)Enum.Parse<Category>(vm.Category) - 1);
            vm.CategoryPicker = CategoryPicker;
        }

        if (vm.PageSizePicker == null)
        {
            PageSizePicker.ItemsSource = (from d in Enum.GetValues(typeof(PageSize)).Cast<PageSize>() select d).ToList();
            PageSizePicker.SelectedItem = default(PageSize);
            PageSizePicker.SelectedIndex = 0;
            vm.PageSizePicker = PageSizePicker;
        }

        if (vm.OrderByPicker == null)
        {
            OrderByPicker.ItemsSource = (from d in Enum.GetValues(typeof(OrderBy)).Cast<OrderBy>() select d).ToList();
            OrderByPicker.SelectedItem = default(OrderBy);
            OrderByPicker.SelectedIndex = 0;
            vm.OrderByPicker = OrderByPicker;
        }

        if (vm.OrderTypePicker == null)
        {
            OrderTypePicker.ItemsSource = (from d in Enum.GetValues(typeof(OrderType)).Cast<OrderType>() select d).ToList();
            OrderTypePicker.SelectedItem = default(OrderType);
            OrderTypePicker.SelectedIndex = 0;
            vm.OrderTypePicker = OrderTypePicker;
        }

        if (!(vm.Products?.Any() ?? false))
        {
            vm.GetProducts();
        }
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var vm = (ProductsViewModel)BindingContext;
        vm.Product = e.CurrentSelection[0] as ProductDto;
        vm.ProductNavigation();
    }
}