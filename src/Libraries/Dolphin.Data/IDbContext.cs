using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Dolphin.Data
{
    public interface IDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry Entry(object entity);
        int SaveChanges();
        Database Database { get; }
    }
}
