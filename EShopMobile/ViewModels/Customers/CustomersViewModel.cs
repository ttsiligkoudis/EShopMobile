using Client;
using CommunityToolkit.Mvvm.ComponentModel;
using DataModels.Dtos;
using EShopMobile.Helpers;

namespace EShopMobile.ViewModels.Customers
{
    public partial class CustomersViewModel : ObservableObject
    {
        private readonly ClientHelper _client;

        [ObservableProperty]
        private List<CustomerDto> customers;

        [ObservableProperty]
        public bool isLoading;

        public CustomersViewModel(IAlertService alert)
        {
            _client = new ClientHelper();
        }

        public async Task GetCustomers()
        {
            IsLoading = true;
            Customers = (await _client.CustomerClient.GetListAsync("Customers")).ToList();
            IsLoading = false;
        }
    }
}
