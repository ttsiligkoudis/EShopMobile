using EShopMobile.Models;

namespace EShopMobile.Helpers
{
    public interface IUserAccess
    {
        bool IsAdmin(Customer customer);
        bool IsCustomer(Customer customer);
    }
}
