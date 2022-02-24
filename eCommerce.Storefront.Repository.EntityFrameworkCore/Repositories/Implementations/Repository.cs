using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eCommerce.Storefront.Model;
using eCommerce.Storefront.Repository.EntityFrameworkCore.Repositories.Interfaces;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Repositories.Implementations
{
    public class Repository<T, TId> : IRepository<T, TId> where T : EntityBase<TId>
    {
        protected readonly IUnitOfWork _uow;
        protected readonly ShopDataContext _dataContext;

        public Repository(IUnitOfWork uow, ShopDataContext dataContext)
        {
            _uow = uow;
            _dataContext = dataContext;
        }

        public T FindBy(TId id)
        {
            return AppendCriteria(_dataContext.Set<T>()).OrderBy(e => e.Id).FirstOrDefault(e => e.Id.Equals(id));
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return AppendCriteria(_dataContext.Set<T>()).Where(predicate);
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate, int index, int count)
        {
            return AppendCriteria(_dataContext.Set<T>()).Where(predicate).OrderBy(e => e.Id).Skip(index).Take(count);
        }

        public IEnumerable<T> FindAll()
        {
            return AppendCriteria(_dataContext.Set<T>());
        }

        public virtual IQueryable<T> AppendCriteria(IQueryable<T> criteria)
        {
            return criteria;
        }

        public void Add(T entity)
        {
            _dataContext.Add(entity);
        }

        public void Save(T entity)
        {
            _dataContext.Update(entity);
        }

        public void Remove(T entity)
        {
            _dataContext.Remove(entity);
        }
    }
}
