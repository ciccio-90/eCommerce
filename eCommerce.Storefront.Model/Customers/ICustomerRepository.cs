using Infrastructure.Domain;

namespace eCommerce.Storefront.Model.Customers
{
    public interface ICustomerRepository : IRepository<Customer, long>
    {
        Customer FindBy(string email);
        void SaveEmail(string userId, string email);
    }
}