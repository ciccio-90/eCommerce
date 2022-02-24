using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using eCommerce.Backoffice.Shared.Services.Interfaces;
using eCommerce.Storefront.Model;
using eCommerce.Storefront.Repository.EntityFrameworkCore;
using eCommerce.Storefront.Repository.EntityFrameworkCore.Repositories.Interfaces;

namespace eCommerce.Backoffice.Shared.Services.Implementations
{
    public class EntityService<T, TId> : IEntityService<T, TId> where T : EntityBase<TId>
    {
        private readonly IRepository<T, TId> _repository;
        private readonly IUnitOfWork _uow;

        public EntityService(IRepository<T, TId> repository, IUnitOfWork uow)
        {
            _repository = repository;
            _uow = uow;
        }

        public T Get(TId id)
        {
            return _repository.FindBy(id);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _repository.FindBy(predicate);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate, int index, int count)
        {
            return _repository.FindBy(predicate, index, count);
        }

        public IEnumerable<T> Get()
        {
            return _repository.FindAll();
        }

        public T Create(T entity)
        {
            entity.ThrowExceptionIfInvalid();
            _repository.Add(entity);
            _uow.Commit();

            return entity;
        }

        public T Modify(T entity)
        {            
            entity.ThrowExceptionIfInvalid();
            _repository.Save(entity);
            _uow.Commit();

            return entity;
        }

        public void Delete(TId id)
        {
            T entity = _repository.FindBy(id);

            _repository.Remove(entity);
            _uow.Commit();
        }
    }
}