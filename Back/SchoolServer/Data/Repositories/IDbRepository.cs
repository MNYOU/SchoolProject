using SchoolServer.Data.Entities;
using Task = System.Threading.Tasks.Task;

namespace SchoolServer.Data.Repositories;

public interface IDbRepository
{
    IQueryable<T> Get<T>() where T : class, IEntityDal;

    IQueryable<T> Get<T>(Func<T, bool> selector) where T : class, IEntityDal;

    IQueryable<T> GetAll<T>() where T : class, IEntityDal;

    Task Remove<T>(T entity) where T : class, IEntityDal;

    Task Update<T>(T entity) where T : class, IEntityDal;
    
    Task Add<T>(T entity) where T : class, IEntityDal;

    Task<int> SaveChangesAsync();
}