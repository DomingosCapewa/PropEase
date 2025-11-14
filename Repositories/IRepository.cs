using System.Linq;
using System.Threading.Tasks;

namespace PropEase.API.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Query();
        Task<T?> GetByIdAsync(object id);
        Task AddAsync(T entity);
        void Remove(T entity);
    }
}
