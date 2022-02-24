namespace eCommerce.Storefront.Repository.EntityFrameworkCore
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}