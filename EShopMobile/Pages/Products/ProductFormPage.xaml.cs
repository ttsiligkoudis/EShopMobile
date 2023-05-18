using DataModels.Dtos;
using DataModels;
using EShopMobile.ViewModels.Products;
using EShopMobile.Helpers;

namespace EShopMobile.Pages.Products;

public partial class ProductFormPage : BasePage
{
    public ProductFormPage(ProductsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var vm = (ProductsViewModel)BindingContext;
        if (vm.Product.Quantity == 0)
        {
            StockText.IsVisible = true;
            StockText.Text = "Out of stock";
            QuantityBorder.IsVisible = false;
            minus.IsVisible = false;
            plus.IsVisible = false;
            ProductCartBtn.IsVisible = false;
        } 
        else if (vm.Product.Quantity <= 5)
        {
            StockText.IsVisible = true;
            StockText.Text = "Almost out of stock";
        }

        await vm.GetProductRates();
        Quantity.TextChanged += Quantity_TextChanged;
        var cartProducts = Session.GetCartProducts();
        if (cartProducts != null && vm.Product != null)
            changeAddtoCartBtn(!cartProducts.Any(w => w.Id == vm.Product.Id));

        var savedProducts = Session.GetSavedProducts();
        if (savedProducts != null && vm.Product != null)
            changeSaveProductBtn(savedProducts.Any(w => w.Id == vm.Product.Id));
    }

    private void ProductCartBtn_Clicked(object sender, EventArgs e)
    {
        var vm = (ProductsViewModel)BindingContext;
        if (vm.Product == null)
            return;

        var product = MauiProgram.Clone(vm.Product);
        product.Quantity = vm.Quantity;

        var products = Session.GetCartProducts();
        if (products == null)
        {
            Session.SetCartProducts(new List<ProductDto> { product });
            changeAddtoCartBtn(false);
            return;
        }

        var productInCart = products.FirstOrDefault(w => w.Id == product.Id);
        if (productInCart != null)
        {
            products.Remove(productInCart);
            Session.SetCartProducts(products.Any() ? products : null);
            changeAddtoCartBtn(true);
        }
        else
        {
            products.Add(product);
            Session.SetCartProducts(products);
            changeAddtoCartBtn(false);
        }
    }

    private void QuantityBtn_Clicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        var vm = (ProductsViewModel)BindingContext;

        if (btn.StyleId == "minus" && vm.Quantity > 1)
            vm.Quantity--;
        else if (btn.StyleId == "plus" && (!vm.Product.Quantity.HasValue || vm.Quantity < vm.Product.Quantity))
            vm.Quantity++;
    }

    private void Quantity_TextChanged(object sender, TextChangedEventArgs e)
    {
        var vm = (ProductsViewModel)BindingContext;

        if (vm.Product.Quantity.HasValue && vm.Quantity > vm.Product.Quantity.Value)
            vm.Quantity = vm.Product.Quantity.Value;

        if (vm.Quantity < 1)
            vm.Quantity = 1;
    }

    private void changeAddtoCartBtn(bool add)
    {
        ProductCartBtn.Text = add ? "Add to Cart" : "Remove from Cart";
        ProductCartBtn.BackgroundColor = add ? Color.FromHex("0d6efd") : Color.FromHex("8b0000");
        Quantity.BackgroundColor = add ? Colors.White : Color.FromHex("#e9ecef");
        Quantity.IsEnabled = add;
        minus.IsEnabled = add;
        plus.IsEnabled = add;
        SetCartCounter(true);
    }

    private void HeartBtn_Clicked(object sender, EventArgs e)
    {
        var vm = (ProductsViewModel)BindingContext;
        var btn = sender as Button;
        var add = btn.StyleId == "NotSaved";
        changeSaveProductBtn(add);
        var products = Session.GetSavedProducts();
        if (products == null)
        {
            Session.SetSavedProducts(new List<ProductDto> { vm.Product });
            return;
        }

        var productInSaved = products.FirstOrDefault(w => w.Id == vm.Product.Id);
        if (productInSaved != null)
        {
            products.Remove(productInSaved);
            Session.SetSavedProducts(products.Any() ? products : null);
        }
        else
        {
            products.Add(vm.Product);
            Session.SetSavedProducts(products);
        }
    }

    private void changeSaveProductBtn(bool add)
    {
        Saved.IsVisible = add;
        NotSaved.IsVisible = !add;
    }
}