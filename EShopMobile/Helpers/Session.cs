using DataModels.Dtos;
using static Newtonsoft.Json.JsonConvert;

namespace EShopMobile.Helpers
{
    public static class Session
    {
        public static CustomerDto GetCustomer()
        {
            var objectStr = Preferences.Get("Customer", null);

            return !string.IsNullOrEmpty(objectStr) ? DeserializeObject<CustomerDto>(objectStr) : null;

        }

        public static void SetCustomer(CustomerDto obj)
        {
            Preferences.Set("Customer", SerializeObject(obj));
        }

        public static UserDto GetUser()
        {
            var objectStr = Preferences.Get("User", null);

            return !string.IsNullOrEmpty(objectStr) ? DeserializeObject<UserDto>(objectStr) : null;
        }

        public static void SetUser(UserDto obj)
        {
            Preferences.Set("User", SerializeObject(obj));
        }

        public static List<ProductDto> GetCartProducts()
        {
            var objectStr = Preferences.Get("CartProducts", null);

            return !string.IsNullOrEmpty(objectStr) ? DeserializeObject<List<ProductDto>>(objectStr) : null;
        }

        public static void SetCartProducts(List<ProductDto> obj)
        {
            Preferences.Set("CartProducts", SerializeObject(obj));
        }

        public static List<ProductDto> GetSavedProducts()
        {
            var objectStr = Preferences.Get("SavedProducts", null);

            return !string.IsNullOrEmpty(objectStr) ? DeserializeObject<List<ProductDto>>(objectStr) : null;
        }

        public static void SetSavedProducts(List<ProductDto> obj)
        {
            Preferences.Set("SavedProducts", SerializeObject(obj));
        }
    }
}
