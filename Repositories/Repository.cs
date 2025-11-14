using Microsoft.EntityFrameworkCore;
using PropEase.API.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PropEase.API.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _db;
        public Repository(AppDbContext db) { _db = db; }

        public IQueryable<T> Query() => _db.Set<T>().AsQueryable();

        public Task<T?> GetByIdAsync(object id)
        {
            return _db.Set<T>().FindAsync(id).AsTask();
        }

        public async Task AddAsync(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
        }

        public void Remove(T entity)
        {
            _db.Set<T>().Remove(entity);
        }
    }
}
