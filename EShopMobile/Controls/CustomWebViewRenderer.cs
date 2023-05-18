using Android.Content;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Platform;
using EShopMobile.Controls;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(WebView), typeof(CustomWebViewRenderer))]
namespace EShopMobile.Controls
{
    public class CustomWebViewRenderer : WebViewRenderer
    {
        private readonly Context _context;

        public CustomWebViewRenderer(Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                Control.SetWebViewClient(GetWebViewClient());
                Control.Settings.UserAgentString = "Mozilla/5.0 (Linux; Android 8.0; Pixel 2 Build/OPD3.170816.012) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.82 Mobile Safari/537.36";
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }
    }
}