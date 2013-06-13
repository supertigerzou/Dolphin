using Dolphin.Core.Data;
using System.Linq;

namespace Dolphin.Data
{
    public class EntityFrameworkRepository<T> : IRepository<T> where T : class
    {
        private readonly IDbContext _dbContext;

        public EntityFrameworkRepository(IDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public T GetByPrimaryKey(object key)
        {
            return this._dbContext.Set<T>().Find(key);
        }

        public void Insert(T record)
        {
            this._dbContext.Set<T>().Add(record);
        }

        public IQueryable<T> Table { get { return this._dbContext.Set<T>(); } }
    }
}
