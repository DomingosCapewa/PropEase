using System.Threading.Tasks;

namespace PropEase.API.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<T> Repository<T>() where T : class;
        Task<int> SaveChangesAsync();
    }
}
