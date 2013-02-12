
using System.Linq;

namespace Future.Core.Data
{
    public interface IRepository<T> where T : class
    {
        T GetByPrimaryKey(object key);
        void Insert(T record);
        IQueryable<T> Table { get; }
    }
}
