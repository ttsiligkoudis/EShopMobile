package crc64f47a2224750f28fb;


public class DangerousTrustProvider_DangerousTrustManager
	extends javax.net.ssl.X509ExtendedTrustManager
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_checkClientTrusted:([Ljava/security/cert/X509Certificate;Ljava/lang/String;Ljava/net/Socket;)V:GetCheckClientTrusted_arrayLjava_security_cert_X509Certificate_Ljava_lang_String_Ljava_net_Socket_Handler\n" +
			"n_checkClientTrusted:([Ljava/security/cert/X509Certificate;Ljava/lang/String;Ljavax/net/ssl/SSLEngine;)V:GetCheckClientTrusted_arrayLjava_security_cert_X509Certificate_Ljava_lang_String_Ljavax_net_ssl_SSLEngine_Handler\n" +
			"n_checkClientTrusted:([Ljava/security/cert/X509Certificate;Ljava/lang/String;)V:GetCheckClientTrusted_arrayLjava_security_cert_X509Certificate_Ljava_lang_String_Handler\n" +
			"n_checkServerTrusted:([Ljava/security/cert/X509Certificate;Ljava/lang/String;Ljava/net/Socket;)V:GetCheckServerTrusted_arrayLjava_security_cert_X509Certificate_Ljava_lang_String_Ljava_net_Socket_Handler\n" +
			"n_checkServerTrusted:([Ljava/security/cert/X509Certificate;Ljava/lang/String;Ljavax/net/ssl/SSLEngine;)V:GetCheckServerTrusted_arrayLjava_security_cert_X509Certificate_Ljava_lang_String_Ljavax_net_ssl_SSLEngine_Handler\n" +
			"n_checkServerTrusted:([Ljava/security/cert/X509Certificate;Ljava/lang/String;)V:GetCheckServerTrusted_arrayLjava_security_cert_X509Certificate_Ljava_lang_String_Handler\n" +
			"n_getAcceptedIssuers:()[Ljava/security/cert/X509Certificate;:GetGetAcceptedIssuersHandler\n" +
			"";
		mono.android.Runtime.register ("MyApp.Platforms.Android.DangerousTrustProvider+DangerousTrustManager, EShopMobile", DangerousTrustProvider_DangerousTrustManager.class, __md_methods);
	}


	public DangerousTrustProvider_DangerousTrustManager ()
	{
		super ();
		if (getClass () == DangerousTrustProvider_DangerousTrustManager.class) {
			mono.android.TypeManager.Activate ("MyApp.Platforms.Android.DangerousTrustProvider+DangerousTrustManager, EShopMobile", "", this, new java.lang.Object[] {  });
		}
	}


	public void checkClientTrusted (java.security.cert.X509Certificate[] p0, java.lang.String p1, java.net.Socket p2)
	{
		n_checkClientTrusted (p0, p1, p2);
	}

	private native void n_checkClientTrusted (java.security.cert.X509Certificate[] p0, java.lang.String p1, java.net.Socket p2);


	public void checkClientTrusted (java.security.cert.X509Certificate[] p0, java.lang.String p1, javax.net.ssl.SSLEngine p2)
	{
		n_checkClientTrusted (p0, p1, p2);
	}

	private native void n_checkClientTrusted (java.security.cert.X509Certificate[] p0, java.lang.String p1, javax.net.ssl.SSLEngine p2);


	public void checkClientTrusted (java.security.cert.X509Certificate[] p0, java.lang.String p1)
	{
		n_checkClientTrusted (p0, p1);
	}

	private native void n_checkClientTrusted (java.security.cert.X509Certificate[] p0, java.lang.String p1);


	public void checkServerTrusted (java.security.cert.X509Certificate[] p0, java.lang.String p1, java.net.Socket p2)
	{
		n_checkServerTrusted (p0, p1, p2);
	}

	private native void n_checkServerTrusted (java.security.cert.X509Certificate[] p0, java.lang.String p1, java.net.Socket p2);


	public void checkServerTrusted (java.security.cert.X509Certificate[] p0, java.lang.String p1, javax.net.ssl.SSLEngine p2)
	{
		n_checkServerTrusted (p0, p1, p2);
	}

	private native void n_checkServerTrusted (java.security.cert.X509Certificate[] p0, java.lang.String p1, javax.net.ssl.SSLEngine p2);


	public void checkServerTrusted (java.security.cert.X509Certificate[] p0, java.lang.String p1)
	{
		n_checkServerTrusted (p0, p1);
	}

	private native void n_checkServerTrusted (java.security.cert.X509Certificate[] p0, java.lang.String p1);


	public java.security.cert.X509Certificate[] getAcceptedIssuers ()
	{
		return n_getAcceptedIssuers ();
	}

	private native java.security.cert.X509Certificate[] n_getAcceptedIssuers ();

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
