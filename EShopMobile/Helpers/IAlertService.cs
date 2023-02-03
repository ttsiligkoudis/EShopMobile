namespace EShopMobile.Helpers
{
    public interface IAlertService
    {
        Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons);

        Task<bool> DisplayAlert(string title, string message, string accept, string cancel);
    }
}
