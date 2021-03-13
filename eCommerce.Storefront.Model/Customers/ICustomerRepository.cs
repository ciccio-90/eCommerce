using Infrastructure.Domain;

namespace eCommerce.Storefront.Model.Customers
{
    public interface ICustomerRepository : IRepository<Customer, int>
    {
        Customer FindBy(string identityToken);
    }
}