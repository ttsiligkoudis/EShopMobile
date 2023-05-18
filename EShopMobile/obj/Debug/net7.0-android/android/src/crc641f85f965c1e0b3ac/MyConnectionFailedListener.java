package crc641f85f965c1e0b3ac;


public class MyConnectionFailedListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener,
		com.google.android.gms.common.api.internal.OnConnectionFailedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onConnectionFailed:(Lcom/google/android/gms/common/ConnectionResult;)V:GetOnConnectionFailed_Lcom_google_android_gms_common_ConnectionResult_Handler:Android.Gms.Common.Api.Internal.IOnConnectionFailedListenerInvoker, Xamarin.GooglePlayServices.Base\n" +
			"";
		mono.android.Runtime.register ("EShopMobile.Helpers.MyConnectionFailedListener, EShopMobile", MyConnectionFailedListener.class, __md_methods);
	}


	public MyConnectionFailedListener ()
	{
		super ();
		if (getClass () == MyConnectionFailedListener.class) {
			mono.android.TypeManager.Activate ("EShopMobile.Helpers.MyConnectionFailedListener, EShopMobile", "", this, new java.lang.Object[] {  });
		}
	}


	public void onConnectionFailed (com.google.android.gms.common.ConnectionResult p0)
	{
		n_onConnectionFailed (p0);
	}

	private native void n_onConnectionFailed (com.google.android.gms.common.ConnectionResult p0);

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
