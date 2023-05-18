using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using EShopMobile.Helpers;
using EShopMobile.ViewModels;
//using Firebase;
//using Firebase.Auth;
using static Android.Gms.Common.Apis.GoogleApiClient;

namespace EShopMobile.Pages;

public partial class LoginPage : ContentPage
{
    //private FirebaseAuth _firebaseAuth;

    public LoginPage(LoginViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var vm = (LoginViewModel)BindingContext;
        vm.Customer = Session.GetCustomer();
        vm.User = Session.GetUser();
        (Shell.Current as AppShell).ChangeMenu(vm.User?.UserType);
        if (vm.Customer != null)
            await Shell.Current.GoToAsync("..");
    }

    private async void GoogleBtn_Clicked(object sender, EventArgs e)
    {
        //await Shell.Current.GoToAsync(nameof(AuthenticationPage));
        //await Launcher.OpenAsync("https://eshop.myportofolio.eu/Users/LoginWithGoogle?isMobile=true");

        try
        {
            var googleSignInOptions = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                .RequestIdToken("1026619004273-s5ahsma1t956lh92stiegc6eejs720ae.apps.googleusercontent.com")
                .RequestEmail()
                .Build();

            var callbacks = new MyConnectionCallbacks();
            var listener = new MyConnectionFailedListener();

            var googleApiClient = new Builder(MauiApplication.Current.ApplicationContext)
                .AddConnectionCallbacks(callbacks)
                .AddOnConnectionFailedListener(listener)
                .AddApi(Auth.GOOGLE_SIGN_IN_API, googleSignInOptions)
                .AddScope(new Scope(Scopes.Profile))
                .Build();

            var signInIntent = Auth.GoogleSignInApi.GetSignInIntent(googleApiClient);

            Platform.CurrentActivity.StartActivityForResult(signInIntent, 1);

//            googleApiClient.RegisterConnectionCallbacks(new ConnectionCallbacks(
//    onConnected: () =>
//    {
//        if (googleApiClient.IsConnected)
//        {
//            GoogleSignInResult result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
//        }
//    },
//    onConnectionSuspended: () => { }
//));
            googleApiClient.Connect();

            //_firebaseAuth = GetFirebaseAuth();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    //private FirebaseAuth GetFirebaseAuth()
    //{
    //    var app = FirebaseApp.InitializeApp(MauiApplication.Current.ApplicationContext);

    //    if (app == null)
    //    {
    //        var options = new FirebaseOptions.Builder()
    //            .SetProjectId("eshopsso")
    //            .SetApplicationId("eshopsso")
    //            .SetApiKey("AIzaSyCQP4v5PN9Y9kIJcDlBPAnoWYqHKcmb8C8")
    //            .SetStorageBucket("eshopsso.appspot.com")
    //            .Build();

    //        app = FirebaseApp.InitializeApp(MauiApplication.Current.ApplicationContext, options);
    //    }

    //    return FirebaseAuth.Instance;
    //}
}