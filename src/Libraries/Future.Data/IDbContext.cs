using System.Data.Entity;

namespace Future.Data
{
    public interface IDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        Database Database { get; }
    }
}
