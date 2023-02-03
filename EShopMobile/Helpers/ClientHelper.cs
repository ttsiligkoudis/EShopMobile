using EShopMobile.Models;

namespace EShopMobile.Helpers
{
    public class ClientHelper
    {
        public Client<Customer> CustomerClient { get; set; }
        public Client<Order> OrderClient { get; set; }
        public Client<Product> ProductClient { get; set; }
        public Client<User> UserClient { get; set; }
        public Client<OrderProducts> OrderProductClient { get; set; }

        public ClientHelper()
        {
            CustomerClient = new Client<Customer>();
            OrderClient = new Client<Order>();
            ProductClient = new Client<Product>();
            UserClient = new Client<User>();
            OrderProductClient = new Client<OrderProducts>();
        }

        public void Dispose()
        {
            CustomerClient.Dispose();
            OrderClient.Dispose();
            ProductClient.Dispose();
            UserClient.Dispose();
            OrderProductClient.Dispose();
        }
    }
}