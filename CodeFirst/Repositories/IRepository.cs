using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;

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

    public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly HeroContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public EFGenericRepository(HeroContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public List<TEntity> List()
        {
            return _dbSet.ToList();
        }

        public TEntity Get(Guid id)
        {
            return _dbSet.Find(id);
        }

        public void Add(TEntity item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public void Remove(TEntity item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }

        public void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
