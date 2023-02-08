using CommunityToolkit.Mvvm.ComponentModel;
using EShopMobile.Helpers;
using EShopMobile.Models;

namespace EShopMobile.ViewModels.Customers
{
    public partial class CustomersViewModel : ObservableObject
    {
        private readonly ClientHelper _client;
        private readonly Session _session;
        private readonly IAlertService _alert;

        [ObservableProperty]
        private Customer customer;

        [ObservableProperty]
        private User user;

        public CustomersViewModel(IAlertService alert)
        {
            _client = new ClientHelper();
            _session = new Session();
            _alert = alert;
            Customer = _session.GetCustomer();
            User = _session.GetUser();
        }

        public async Task<List<Customer>> GetCustomers()
        {
            var customers = await _client.CustomerClient.GetListAsync("Customers");
            return customers.ToList();
        }
    }
}
