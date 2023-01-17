using Microsoft.EntityFrameworkCore;
using SchoolServer.Controllers;
using SchoolServer.Data.Entities;

namespace SchoolServer.Data;

public class DataContext : DbContext
{
    // public DataContext(DbContextOptions<DataContext> options) : base(options)
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        // Database.EnsureDeleted();
        // Database.EnsureCreated();
    }

    public DbSet<UserDal> Users { get; set; }
    public DbSet<TaskDal> Tasks { get; set; }
    public DbSet<SubjectDal> Subjects { get; set; }
    public DbSet<DifficultyDal> Difficulties { get; set; }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     // no
    //     base.OnModelCreating(modelBuilder);
    // }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }

    // public DbSet<T> DbSet<T>() where T : class
    // {
    //     return Set<T>();
    // }

    public IQueryable<T> Query<T>() where T : class
    {
        return Set<T>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // тут можно задать подключение к бд, как и в appsettings

        // mysql
        optionsBuilder.UseMySql(
            "Server=1234;Database=1234;Uid=1234;Password=123",
            ServerVersion.AutoDetect(
                "Server=1234;Database=1234;Uid=1234;Password=123"));


        // ниже постгрес
        // optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=jhtnmb32;");
        // POOLING = True; MINPOOLSIZE = 1; MAXPOOLSIZE = 100;
        // optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // modelBuilder.Entity<UserDal>().Property(x => x.Id).
        // base.OnModelCreating(modelBuilder);
    }
}