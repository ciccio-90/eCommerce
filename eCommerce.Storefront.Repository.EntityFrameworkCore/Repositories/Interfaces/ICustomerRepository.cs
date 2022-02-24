using eCommerce.Storefront.Model.Customers;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer, long>
    {
        Customer FindBy(string email);
        void SaveEmail(string userId, string email);
    }
}