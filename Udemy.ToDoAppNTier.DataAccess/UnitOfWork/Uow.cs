using System.Threading.Tasks;
using Udemy.ToDoAppNTier.DataAccess.Contexts;
using Udemy.ToDoAppNTier.DataAccess.Interfaces;
using Udemy.ToDoAppNTier.DataAccess.Repositories;
using Udemy.ToDoAppNTier.Entities.Domains;

namespace Udemy.ToDoAppNTier.DataAccess.UnitOfWork
{
    public class Uow : IUow
    {
        private readonly ToDoContext _context;

        public Uow(ToDoContext context)
        {
            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            return new Repository<T>(_context);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
