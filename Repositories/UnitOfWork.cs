using PropEase.API.Data;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace PropEase.API.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _db;
        private readonly ConcurrentDictionary<Type, object> _repos = new();

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T);
            if (_repos.TryGetValue(type, out var existing))
                return (IRepository<T>)existing;

            var repo = new Repository<T>(_db);
            _repos[type] = repo;
            return repo;
        }

        public Task<int> SaveChangesAsync() => _db.SaveChangesAsync();

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}
