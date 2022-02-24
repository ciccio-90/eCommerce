using eCommerce.Storefront.Model;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Repositories.Interfaces
{
    public interface IRepository<T, TId> : IReadOnlyRepository<T, TId> where T : EntityBase<TId>
    {
        void Add(T entity);
        void Save(T entity);
        void Remove(T entity);
    }
}