using System.Linq;
using Infrastructure.EntityFrameworkCore;
using Infrastructure.UnitOfWork;
using eCommerce.Storefront.Model.Customers;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Repositories
{
    public class CustomerRepository : Repository<Customer, int>, ICustomerRepository
    {
        public CustomerRepository(IUnitOfWork uow, DataContext dataContext) : base(uow, dataContext)
        {
        }

        public Customer FindBy(string identityToken)
        {
            return FindBy(c => c.IdentityToken.Equals(identityToken)).FirstOrDefault();
        }

        public override IQueryable<Customer> AppendCriteria(IQueryable<Customer> criteria)
        {
            return criteria.Include(c => c.DeliveryAddressBook)
                           .Include(c => c.Orders);
        }
    }
}