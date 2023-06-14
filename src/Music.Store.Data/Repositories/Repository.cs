using Microsoft.EntityFrameworkCore;
using Music.Store.Domain;
using Music.Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Music.Store.Data.Repositories
{
    public interface IRepository<T> where T : EntityBase
    {
        Task Add(T entity);

        Task AddRange(List<T> entity);

        IQueryable<T> GetAll();

        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null);

        IQueryable<T> GetAllNoTracking();

        Task<T> Get(Expression<Func<T, bool>> predicate = null);

        Task<T> Find(int id);

        void Update(T entity);

        void Remove(T entity);

        Task Remove(int id);

        void RemoveAll(List<T> list);

        Task<bool> Any(Expression<Func<T, bool>> predicate);
    }


    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;
        private bool _isDisposed;

        public Repository(IUnitOfWork unitOfWork) : this(unitOfWork.Context) { }

        public Repository(ApplicationDbContext context)
        {
            _isDisposed = false;
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRange(List<T> entity)
        {
            await _dbSet.AddRangeAsync(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task Remove(int id)
        {
            var entity = await Get(x => x.Id == id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.OrderByDescending(x => x.Id).AsQueryable();
        }

        public IQueryable<T> GetAllNoTracking()
        {
            return _dbSet.AsQueryable().AsNoTracking();
        }

        public async Task<T> Get(Expression<Func<T, bool>> predicate = null)
        {
            IQueryable<T> query = _dbSet;
            if (predicate != null)
            {
                return await query.FirstOrDefaultAsync(predicate);
            }
            return await query.FirstOrDefaultAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            IQueryable<T> query = _dbSet;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query;
        }

        public async Task<bool> Any(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<T> Find(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void RemoveAll(List<T> list)
        {
            _dbSet.RemoveRange(list);
        }


        #region disposed
        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
            _isDisposed = true;
        }
        #endregion
    }
}
