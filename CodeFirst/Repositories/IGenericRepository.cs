using System;
using System.Collections.Generic;

namespace CodeFirst.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity Get(Guid id);
        List<TEntity> List();
        void Add(TEntity item);
        void Remove(TEntity item);
        void Update(TEntity item);
    }
}
