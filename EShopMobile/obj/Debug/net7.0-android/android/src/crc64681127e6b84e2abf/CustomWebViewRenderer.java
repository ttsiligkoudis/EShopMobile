package crc64681127e6b84e2abf;


public class CustomWebViewRenderer
	extends crc6477f0d89a9cfd64b1.WebViewRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("EShopMobile.Controls.CustomWebViewRenderer, EShopMobile", CustomWebViewRenderer.class, __md_methods);
	}


	public CustomWebViewRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == CustomWebViewRenderer.class) {
			mono.android.TypeManager.Activate ("EShopMobile.Controls.CustomWebViewRenderer, EShopMobile", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
		}
	}


	public CustomWebViewRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == CustomWebViewRenderer.class) {
			mono.android.TypeManager.Activate ("EShopMobile.Controls.CustomWebViewRenderer, EShopMobile", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
		}
	}


	public CustomWebViewRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == CustomWebViewRenderer.class) {
			mono.android.TypeManager.Activate ("EShopMobile.Controls.CustomWebViewRenderer, EShopMobile", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1, p2 });
		}
	}

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
