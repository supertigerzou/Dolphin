
using System.Linq;

namespace Dolphin.Core.Data
{
    public interface IRepository<T> where T : class
    {
        T GetByPrimaryKey(object key);
        void Insert(T record);
        IQueryable<T> Table { get; }
    }
}
