using DataModels;
using EShopMobile.Helpers;
using EShopMobile.ViewModels.Products;

namespace EShopMobile.Pages.Orders;

public partial class CartPage : ContentPage
{
    private readonly Session _session;

    public CartPage(ProductsViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
        _session = new Session();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var vm = (ProductsViewModel)BindingContext;
        vm.Customer = _session.GetCustomer();
        vm.Navigation = Navigation;
        if (vm.Customer != null)
        {
            OrderPagesBtns.IsVisible = false;
            OrderDetailsBtn2.Text = "Complete Order";
            OrderDetailsBtn2.Clicked -= ChangePageBtn_Clicked;
            OrderDetailsBtn2.Command = vm.CompleteOrderCommand;
            CustomerName.IsEnabled = false;
            CustomerCity.IsEnabled = false;
            CustomerAddress.IsEnabled = false;
            CustomerEmail.IsEnabled = false;
            CreateAccountSwitch.IsVisible = false;
        }
        vm.Customer ??= new();
        vm.Products = _session.GetCartProducts();
        if (vm.Products?.Any() ?? false)
            vm.FinalPrice = vm.Products.Select(s => s.Price * s.Quantity ?? 0).Sum();
        else
        {
            NoProductsLabel.IsVisible = true;
            CartCollection.IsVisible = false;
            FinalPriceBorder.IsVisible = false;
            CompleteOrderBtn.IsEnabled = false;
            CompleteOrderBtn.BackgroundColor = Colors.Grey;
        }
    }

    private async void DeleteBtn_Clicked(object sender, EventArgs e)
    {
        var vm = (ProductsViewModel)BindingContext;
        var btn = sender as Button;
        var parent = (StackLayout)btn.Parent;
        var productID = ((Label)parent.FindByName("ProductID"))?.Text;
        var product = vm.Products.FirstOrDefault(w => w.Id.ToString() == productID);
        if (product != null)
        {
            vm.Products.Remove(product);
            _session.SetCartProducts(vm.Products);
            var page = Navigation.NavigationStack.LastOrDefault();
            await Shell.Current.GoToAsync(nameof(CartPage));
            Navigation.RemovePage(page);
        }
    }

    private void ChangePageBtn_Clicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        var proceed = btn.StyleId != "CartProductsBtn";
        CartProducts.IsVisible = !proceed;
        OrderDetails.IsVisible = proceed;
        CartProductsBtn.TextColor = !proceed ? Colors.White : Colors.Grey;
        OrderDetailsBtn.TextColor = proceed ? Colors.White : Colors.Grey;
    }

    private void CreateAccountSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        var toggle = sender as Switch;
        Passwords.IsVisible = toggle.IsToggled;
    }
}