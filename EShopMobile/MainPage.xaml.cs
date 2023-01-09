namespace EShopMobile;

using EShopMobile.Helpers;
using EShopMobile.Models;

public partial class MainPage : ContentPage
{
    private ClientHelper _client;

    public MainPage()
	{
		InitializeComponent();
		_client = new ClientHelper();
	}

    private async void Login_Clicked(object sender, EventArgs e)
    {
        var userInDb =
        await _client.UserClient.GetAsync("Users/GetUserByEmailAndPassword/?email=" + Email.Text.Trim() + "&password=" + Password.Text.Trim());

        if (userInDb == null) 
        {
            Validation.Text = "Invalid email or password";
        }
        else
        {
            var customerDto = await _client.CustomerClient.GetAsync("Customers/User/" + userInDb.Id);
            if (customerDto != null)
            {
                //HttpContext.Session.SetString("Customer", JsonConvert.SerializeObject(_mapper.Map<Customer>(customer)));
            }
            userInDb.LoginDate = DateTime.Now;
            await _client.UserClient.PutAsync(userInDb, "Users/" + userInDb.Id);
        }

    }
    private void Register_Clicked(object sender, EventArgs e)
    {
    }

    private void Login_IsEnabled(object sender, TextChangedEventArgs e)
    {
        Login.IsEnabled = !(string.IsNullOrEmpty(Email.Text) || string.IsNullOrEmpty(Password.Text));
    }
}

