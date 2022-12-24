using Microsoft.EntityFrameworkCore;
using SchoolServer.Controllers;
using SchoolServer.Data.Entities;
using SchoolServer.Models;
using Task = System.Threading.Tasks.Task;

namespace SchoolServer.Data.Repositories;

public class DbRepository : IDbRepository
{
    private readonly DataContext context;

    public DbRepository(DataContext context)
    {
        this.context = context;
    }

    public IQueryable<T> Get<T>() where T : class, IEntityDal
    {
        var t = typeof(T).GetConstructors().FirstOrDefault()?.Invoke(null);
        // return context.DbSet<T>().Where(x => x.)AsQueryable();
        // и в чем разница. p.s. метод выше я написал сам
        return context.Set<T>().AsQueryable();
    }


    public IQueryable<T> Get<T>(Func<T, bool> selector) where T : class, IEntityDal
    {
        return context.Set<T>().Where(selector).AsQueryable();
    }

    public IQueryable<T> GetAll<T>() where T : class, IEntityDal
    {
        return Get<T>();
    }

    public async Task Remove<T>(T entity) where T : class, IEntityDal
    {
        await Task.Run(() => context.Remove(entity));
    }

    public async Task Update<T>(T entity) where T : class, IEntityDal
    {
        await Task.Run(() => context.Set<T>().Update(entity));
    }

    public async Task Add<T>(T entity) where T : class, IEntityDal
    {
        await Task.Run((() => context.Set<T>().Add(entity)));
    }

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }

    public UserDal GetUser(LoginModel loginModel)
    {
        return context.Set<UserDal>().AsQueryable()
            .FirstOrDefault(x => x.Login == loginModel.Login && x.Password == loginModel.Password);
    }
}