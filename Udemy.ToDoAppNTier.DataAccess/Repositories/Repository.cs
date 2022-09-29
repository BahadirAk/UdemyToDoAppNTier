using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.ToDoAppNTier.DataAccess.Contexts;
using Udemy.ToDoAppNTier.DataAccess.Interfaces;
using Udemy.ToDoAppNTier.Entities.Domains;

namespace Udemy.ToDoAppNTier.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ToDoContext _context;

        public Repository(ToDoContext context)
        {
            _context = context;
        }

        public async Task Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByFilter(System.Linq.Expressions.Expression<Func<T, bool>> filter, bool asNoTracking = false)
        {
            return asNoTracking? await _context.Set<T>().SingleOrDefaultAsync(filter) : await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(filter);
        }

        public async Task<T> Find(object id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public IQueryable<T> GetQuery()
        {
            return _context.Set<T>().AsQueryable();
        }

        public void Remove(T entity)
        {
            //var deletedEntity = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(entity);
        }

        public void Update(T entity, T unchangedEntity)
        {
            //_context.Set<T>().Update(entity);

            //var updatedEntity = _context.Set<T>().Find(entity.Id);
            //_context.Entry(updatedEntity).CurrentValues.SetValues(entity);

            _context.Entry(unchangedEntity).CurrentValues.SetValues(entity);
        }
    }
}
