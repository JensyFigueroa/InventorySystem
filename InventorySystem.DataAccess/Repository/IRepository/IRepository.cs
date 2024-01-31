using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.DataAccess.Repository.IRepository
{
    public interface IRepository <T> where T : class
    {
        T Get(int id);

        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedEnumerable<T>> orderBy = null,
            string includeProperty = null
            );

        IEnumerable<T> GetFirt(
           Expression<Func<T, bool>> filter = null,
           string includeProperty = null
           );

        void Add(T entities);
        void remove(int id);
        void remove(T entities);
        void removeRange(IEnumerable<T> entities);


    }
}
