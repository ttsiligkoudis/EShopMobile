using DataModels.Dtos;
using static Newtonsoft.Json.JsonConvert;

namespace EShopMobile.Helpers
{
    public class Session
    {
        public CustomerDto GetCustomer()
        {
            var customerStr = Preferences.Get(nameof(CustomerDto), null);
            if (!string.IsNullOrEmpty(customerStr))
            {
                return DeserializeObject<CustomerDto>(customerStr);
            }
            return null;

        }

        public void SetCustomer(CustomerDto customer)
        {
            if (Preferences.ContainsKey(nameof(CustomerDto)))
            {
                Preferences.Remove(nameof(CustomerDto));
            }
            Preferences.Set(nameof(CustomerDto), SerializeObject(customer));
        }

        public UserDto GetUser()
        {
            var userStr = Preferences.Get(nameof(UserDto), null);

            if (!string.IsNullOrEmpty(userStr))
            {
                return DeserializeObject<UserDto>(userStr);
            }
            return null;
        }

        public void SetUser(UserDto user)
        {
            if (Preferences.ContainsKey(nameof(UserDto)))
            {
                Preferences.Remove(nameof(UserDto));
            }
            Preferences.Set(nameof(UserDto), SerializeObject(user));
        }

        public List<ProductDto> GetCartProducts()
        {
            var productsStr = Preferences.Get("CartProducts", null);

            if (!string.IsNullOrEmpty(productsStr))
            {
                return DeserializeObject<List<ProductDto>>(productsStr);
            }
            return null;
        }

        public void SetCartProducts(List<ProductDto> products)
        {
            if (Preferences.ContainsKey("CartProducts"))
            {
                Preferences.Remove("CartProducts");
            }
            Preferences.Set("CartProducts", SerializeObject(products));
        }

        public List<ProductDto> GetSavedProducts()
        {
            var productsStr = Preferences.Get("SavedProducts", null);

            if (!string.IsNullOrEmpty(productsStr))
            {
                return DeserializeObject<List<ProductDto>>(productsStr);
            }
            return null;
        }

        public void SetSavedProducts(List<ProductDto> products)
        {
            if (Preferences.ContainsKey("SavedProducts"))
            {
                Preferences.Remove("SavedProducts");
            }
            Preferences.Set("SavedProducts", SerializeObject(products));
        }
    }
}
