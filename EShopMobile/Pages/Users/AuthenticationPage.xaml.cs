namespace EShopMobile.Pages.Users;

public partial class AuthenticationPage : ContentPage
{
	public AuthenticationPage()
	{
		InitializeComponent();
        var client = new HttpClient();
        var uri = new Uri("https://eshop.myportofolio.eu/Users/LoginWithGoogle?isMobile=true");

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            // Set the User-Agent header
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Linux; Android 8.0; Pixel 2 Build/OPD3.170816.012) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.82 Mobile Safari/537.36");

            // Make the request
            var response = await client.GetAsync(uri);

            // Get the content as a string
            var content = await response.Content.ReadAsStringAsync();

            // Display the content in a WebView
            myWebView.Source = new HtmlWebViewSource { Html = content };
        });
        
    }
}