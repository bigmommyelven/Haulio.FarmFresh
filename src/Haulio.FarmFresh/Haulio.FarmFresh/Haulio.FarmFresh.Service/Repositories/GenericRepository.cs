using Haulio.FarmFresh.Domain.Common;
using Haulio.FarmFresh.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Haulio.FarmFresh.Service.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
    {
        private bool disposed = false;
        protected readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<T> Add(T entity)
        {
            var ent = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return ent.Entity;
        }

        public virtual async Task<PagedResponse> GetAll(Pagination pagination = null)
        {
            // Pakai default value kalo null
            pagination ??= new Pagination();
            IQueryable<T> query = _context.Set<T>();

            var totalRecords = query.Count();
            var data = await query
                .Skip((pagination.Page - 1) * pagination.Limit)
                .Take(pagination.Limit)
                .ToListAsync();

            return new PagedResponse(data, pagination, totalRecords);
        }

        public virtual async Task<T> GetById(object id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<T> Update(T entity)
        {
            var res = _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
