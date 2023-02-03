using EShopMobile.Models;
using Newtonsoft.Json;
using static Newtonsoft.Json.JsonConvert;

namespace EShopMobile.Helpers
{
    public class Session
    {
        public Customer GetCustomer()
        {
            var customerStr = Preferences.Get(nameof(Customer), null);
            if (!string.IsNullOrEmpty(customerStr))
            {
                return DeserializeObject<Customer>(customerStr);
            }
            return null;

        }

        public void SetCustomer(Customer customer)
        {
            if (Preferences.ContainsKey(nameof(Customer)))
            {
                Preferences.Remove(nameof(Customer));
            }
            Preferences.Set(nameof(Customer), SerializeObject(customer));
        }

        public User GetUser()
        {
            var userStr = Preferences.Get(nameof(User), null);

            if (!string.IsNullOrEmpty(userStr))
            {
                return DeserializeObject<User>(userStr);
            }
            return null;
        }

        public void SetUser(User user)
        {
            if (Preferences.ContainsKey(nameof(User)))
            {
                Preferences.Remove(nameof(User));
            }
            Preferences.Set(nameof(User), SerializeObject(user));
        }
    }
}
