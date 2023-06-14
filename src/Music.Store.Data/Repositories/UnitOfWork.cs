using Microsoft.EntityFrameworkCore.Storage;
using Music.Store.Domain;
using Music.Store.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Music.Store.Data.Repositories
{
    public interface IUnitOfWork
    {
        ApplicationDbContext Context { get; }
        IRepository<T> Repository<T>() where T : EntityBase;
        IExecutionStrategy CreateExecutionStrategy();
        Task CreateTransaction();
        Task Commit();
        Task Rollback();
        Task<int> Save();
    }

    public class UnitOfWork : IUnitOfWork, IDisposable
    {

        private readonly ApplicationDbContext _context;
        private bool _disposed;
        private IDbContextTransaction _objTran;

        public ApplicationDbContext Context
        {
            get { return _context; }
        }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IExecutionStrategy CreateExecutionStrategy()
        {
            return _context.Database.CreateExecutionStrategy();
        }

        public async Task CreateTransaction()
        {
            if (_objTran == null)
            {
                _objTran = await _context.Database.BeginTransactionAsync();
            }
        }

        public async Task Rollback()
        {
            await _objTran.RollbackAsync();
            await _objTran.DisposeAsync();
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task Commit()
        {
            await _objTran.CommitAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    _context.Dispose();
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IRepository<T> Repository<T>() where T : EntityBase
        {
            return new Repository<T>(_context);
        }
    }
}
