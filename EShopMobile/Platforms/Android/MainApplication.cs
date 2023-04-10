using Android.App;
using Android.Runtime;
using AndroidX.AppCompat.App;

namespace EShopMobile;

#if DEBUG
[Application(AllowBackup = false, Debuggable = true, UsesCleartextTraffic = true)]
#else
[Application]
#endif
public class MainApplication : MauiApplication
{
	public MainApplication(IntPtr handle, JniHandleOwnership ownership)
		: base(handle, ownership)
	{
        AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;
    }

	protected override MauiApp CreateMauiApp()
	{
        AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;
        return MauiProgram.CreateMauiApp();
    }
}
