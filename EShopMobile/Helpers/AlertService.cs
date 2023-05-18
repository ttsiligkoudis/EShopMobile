
namespace EShopMobile.Helpers
{
    public static class AlertService
    {
        public static async Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
        {
            return await Application.Current.MainPage.DisplayActionSheet(title, cancel, destruction, buttons);
        }

        public static async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        public static async Task DisplayAlert(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }
    }
}
