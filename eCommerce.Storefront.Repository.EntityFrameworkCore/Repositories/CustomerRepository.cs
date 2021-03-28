using System.Linq;
using Infrastructure.EntityFrameworkCore;
using Infrastructure.UnitOfWork;
using eCommerce.Storefront.Model.Customers;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Repositories
{
    public class CustomerRepository : Repository<Customer, long>, ICustomerRepository
    {
        public CustomerRepository(IUnitOfWork uow, DataContext dataContext) : base(uow, dataContext)
        {
        }

        public Customer FindBy(string email)
        {
            var user = _dataContext.Users.FirstOrDefault(u => u.Email.Equals(email));

            if (user != null)
            {
                return FindBy(c => c.UserId.Equals(user.Id)).FirstOrDefault();
            }
            else 
            {
                return null;
            }
        }

        public override IQueryable<Customer> AppendCriteria(IQueryable<Customer> criteria)
        {
            return criteria.Include(c => c.DeliveryAddressBook)
                           .Include(c => c.Orders)
                           .Include(c => c.Basket)
                           .ThenInclude(b => b.Items)
                           .ThenInclude(i => i.Product)
                           .ThenInclude(p => p.Title)
                           .Include(c => c.Basket)
                           .ThenInclude(b => b.DeliveryOption);
        }

        public void SaveEmail(string userId, string email)
        {
            var user = _dataContext.Users.FirstOrDefault(u => u.Id.Equals(userId));

            if (user != null)
            {
                user.UserName = user.Email = email;
                user.NormalizedUserName = user.NormalizedEmail = email.ToUpper();
                
                _dataContext.Users.Update(user);
            }
        }
    }
}