namespace eCommerce.Storefront.Repository.EntityFrameworkCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShopDataContext _dataContext;

        public UnitOfWork(ShopDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Commit()
        {
            _dataContext.SaveChanges();
        }
    }
}