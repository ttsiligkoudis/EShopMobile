
using EShopMobile.Helpers;
using EShopMobile.Pages.Orders;

namespace EShopMobile.Pages
{
    public class BasePage : ContentPage
    {
        public int CartCounter { get; set; }
        public Label CartCounterLabel { get; set; }

        public BasePage()
        {
            CartCounterLabel = new();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            SetCartCounter(false);

            //BackgroundColor = App.Current.PlatformAppTheme.HasFlag(AppTheme.Dark) ? Color.FromHex("2B2B2B") : Color.FromHex("f1f1f1");
            BackgroundColor = Color.FromHex("f1f1f1");

            var stackLayout = new StackLayout 
            { 
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Label
                    {
                        StyleId = "ShellTitle",
                        Text = Title,
                        FontSize = 20,
                        FontFamily = "OpenSansRegular",
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Colors.White,
                        VerticalOptions = LayoutOptions.Center,
                        Padding = 0,
                        Margin = 0
                    },
                }
            };

            var savedBtn = new Button
            {
                StyleId = "ShellSavedBtn",
                ImageSource = new FontImageSource
                {
                    FontFamily = "FontSolid",
                    Glyph = "\uf004",
                    Size = 20
                },
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Colors.Transparent,
                Padding = 5
            };
            savedBtn.Clicked += SavedBtn_Click;

            var cartBtn = new Button
            {
                StyleId = "ShellCartBtn",
                ImageSource = new FontImageSource
                {
                    FontFamily = "FontSolid",
                    Glyph = "\uf07a",
                    Size = 20
                },
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Colors.Transparent,
                Padding = 5,
                Margin = new Thickness(0,0,5,0)
            };
            cartBtn.Clicked += CartBtn_Clicked;

            CartCounterLabel = new Label
            {
                StyleId = "CartCount",
                Text = CartCounter.ToString(),
                TextColor = Colors.White,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = 12,
                Padding = 0,
            };

            var counterFrame = new Frame
            {
                HeightRequest = 20,
                CornerRadius = 20,
                BackgroundColor = Color.FromHex("418deb"),
                VerticalOptions = LayoutOptions.Center,
                Padding = 0,
                Margin = new Thickness(-10,-15,0,0),
                Content = CartCounterLabel
            };

            var childStackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                Children = { savedBtn, cartBtn, counterFrame }
            };

            stackLayout.Children.Add(childStackLayout);

            Shell.SetTitleView(this, stackLayout);
        }

        private void SavedBtn_Click(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(SavedPage));
        }

        private void CartBtn_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(CartPage));
            //await Navigation.PushModalAsync(new CartPage(new ViewModels.Products.ProductsViewModel()));
        }

        protected void SetCartCounter(bool changeLabel)
        {
            var cartProducts = Session.GetCartProducts();
            CartCounter = cartProducts?.Count ?? 0;
            if (changeLabel)
                CartCounterLabel.Text = CartCounter.ToString();
        }
    }
}
