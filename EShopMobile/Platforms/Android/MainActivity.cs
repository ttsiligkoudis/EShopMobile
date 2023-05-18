using Android.App;
using Android.Content.PM;
using Android.Content;
using EShopMobile.Pages;
using EShopMobile.Pages.Users;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Auth.Api;
//using Firebase.Auth;

namespace EShopMobile;

[Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop, Exported = true)]
[IntentFilter(new[] { Intent.ActionView },
              Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
              DataScheme = CALLBACK_SCHEME)]
public class WebAuthenticationCallbackActivity : WebAuthenticatorCallbackActivity
{
    const string CALLBACK_SCHEME = "myapp";
}

[Activity(
    Theme = "@style/Maui.SplashTheme", 
    LaunchMode = LaunchMode.SingleTop,
    MainLauncher = true, 
    ConfigurationChanges = ConfigChanges.ScreenSize | 
        ConfigChanges.Orientation | 
        ConfigChanges.UiMode | 
        ConfigChanges.ScreenLayout | 
        ConfigChanges.SmallestScreenSize | 
        ConfigChanges.Density
)]
[IntentFilter(
    new[] { Intent.ActionView },
    Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
    DataSchemes = new[] { "EShopMobile", "http", "https" },
    DataHosts = new[] { "eshop.myportofolio.eu" },
    DataPaths = new[] { "", "/Users/Login", "/Users/ResetPassword" },
    AutoVerify = true
)]
[IntentFilter(
    new[] { Intent.ActionSend },
    Categories = new[] { Intent.CategoryDefault },
    DataMimeType = "*/*")]
[IntentFilter(new[] { Platform.Intent.ActionAppAction },
              Categories = new[] { Intent.CategoryDefault })]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnResume()
    {
        base.OnResume();

        Platform.OnResume(this);
    }

    protected async override void OnNewIntent(Intent intent)
    {
        base.OnNewIntent(intent);

        if (intent.Action == Intent.ActionView && intent.Data != null)
        {
            string path;
            List<string> queryList;
            var parameters = new Dictionary<string, object>();

            path = intent.Data.LastPathSegment switch
            {
                "Login" => nameof(LoginPage),
                "ResetPassword" => nameof(ResetPasswordPage),
                _ => nameof(HomePage)
            };

            if (!string.IsNullOrEmpty(intent.Data.Query))
            {
                queryList = intent.Data.Query.Split("&").ToList();
                foreach (var item in queryList)
                {
                    var temp = item.Split("=");
                    parameters.Add(string.Concat(temp[0][0].ToString().ToUpper(), temp[0].AsSpan(1)), temp[1]);
                }
            }
            await Shell.Current.GoToAsync(path,parameters);
        }

        Platform.OnNewIntent(intent);
    }

    protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
    {
        base.OnActivityResult(requestCode, resultCode, data);

        if (requestCode == 1)
        {
            GoogleSignInResult result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
            if (result.IsSuccess)
            {
                LoginWithFireBase(result.SignInAccount);
            }
        }
    }

    private void LoginWithFireBase(GoogleSignInAccount account)
    {
        //var credentials = GoogleAuthProvider.GetCredential(account.IdToken, null);
    }
}
