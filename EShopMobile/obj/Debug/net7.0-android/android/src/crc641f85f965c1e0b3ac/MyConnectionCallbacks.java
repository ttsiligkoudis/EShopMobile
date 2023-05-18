package crc641f85f965c1e0b3ac;


public class MyConnectionCallbacks
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks,
		com.google.android.gms.common.api.internal.ConnectionCallbacks
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onConnected:(Landroid/os/Bundle;)V:GetOnConnected_Landroid_os_Bundle_Handler:Android.Gms.Common.Api.Internal.IConnectionCallbacksInvoker, Xamarin.GooglePlayServices.Base\n" +
			"n_onConnectionSuspended:(I)V:GetOnConnectionSuspended_IHandler:Android.Gms.Common.Api.Internal.IConnectionCallbacksInvoker, Xamarin.GooglePlayServices.Base\n" +
			"";
		mono.android.Runtime.register ("EShopMobile.Helpers.MyConnectionCallbacks, EShopMobile", MyConnectionCallbacks.class, __md_methods);
	}


	public MyConnectionCallbacks ()
	{
		super ();
		if (getClass () == MyConnectionCallbacks.class) {
			mono.android.TypeManager.Activate ("EShopMobile.Helpers.MyConnectionCallbacks, EShopMobile", "", this, new java.lang.Object[] {  });
		}
	}


	public void onConnected (android.os.Bundle p0)
	{
		n_onConnected (p0);
	}

	private native void n_onConnected (android.os.Bundle p0);


	public void onConnectionSuspended (int p0)
	{
		n_onConnectionSuspended (p0);
	}

	private native void n_onConnectionSuspended (int p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
