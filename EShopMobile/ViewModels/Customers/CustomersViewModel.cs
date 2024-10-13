using Client;
using CommunityToolkit.Mvvm.ComponentModel;
using DataModels.Dtos;
using Enums;

namespace EShopMobile.ViewModels.Customers
{
    public partial class CustomersViewModel : ObservableObject
    {
        private readonly IClient _client;

        [ObservableProperty]
        private List<CustomerDto> customers;

        [ObservableProperty]
        public bool isLoading;

        [ObservableProperty]
        private StackLayout pageNumberStack;

        [ObservableProperty]
        private ScrollView scrollView;

        public CustomersViewModel(IClient client)
        {
            _client = client;
        }

        public async void GetCustomers(int pageNumber = 1)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                IsLoading = true;
            });

            var result = (await _client.GetAsync<List<CustomerDto>>("Customers")).ToList();

            pageNumber = pageNumber > 0 ? pageNumber - 1 : 0;
            var pageSize = PageSize.Ten;
            var skip = pageNumber * (short)pageSize;
            var take = (short)pageSize;

            var pages = (result.Count / (short)pageSize) + (result.Count % (short)pageSize > 0 ? 1 : 0);
            Customers = result.Skip(skip).Take(take).ToList();

            PageNumberStack.Clear();
            for (int i = 1; i <= pages; i++)
            {
                var btn = new Button()
                {
                    Text = i.ToString(),
                    TextColor = Colors.White,
                    BackgroundColor = Color.FromHex("6c757d"),
                    Padding = 10,
                    HorizontalOptions = LayoutOptions.Center
                };
                btn.Clicked += PageChanged;
                PageNumberStack.Children.Add(btn);
            }

            MainThread.BeginInvokeOnMainThread(() =>
            {
                IsLoading = false;
            });
        }

        private async void PageChanged(object sender, EventArgs e)
        {
            var btn = sender as Button;
            await ScrollView.ScrollToAsync(0, 0, true);
            GetCustomers(Convert.ToInt32(btn.Text));
        }
    }
}
