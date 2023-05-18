using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.OS;

namespace EShopMobile.Helpers
{
    public class MyConnectionCallbacks : Java.Lang.Object, GoogleApiClient.IConnectionCallbacks
    {
        public void OnConnected(Bundle connectionHint)
        {
            // Your code here
        }

        public void OnConnectionSuspended(int cause)
        {
            // Your code here
        }

        public void OnConnectionFailed(ConnectionResult connection)
        {

        }
    }

}
