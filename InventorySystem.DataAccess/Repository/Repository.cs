using InventorySystem.DataAccess.Data;
using InventorySystem.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        public readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public void Add(T entities)
        {
            dbSet.Add(entities); // insert into table
        }

        public T Get(int id)
        {
            return dbSet.Find(id); // select * from
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedEnumerable<T>> orderBy = null, string includeProperty = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter); //select * from where ...
            }

            if (includeProperty != null)
            {
                foreach(var includeProp in includeProperty.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries)) 
                {
                    query = query.Include(includeProp);
                }

            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        public IEnumerable<T> GetFirst(Expression<Func<T, bool>> filter = null, string includeProperty = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter); //select * from where ...
            }

            if (includeProperty != null)
            {
                foreach (var includeProp in includeProperty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }

            }

            yield return query.FirstOrDefault();
        }

        public void remove(int id)
        {
            T entities = dbSet.Find(id);
            remove(entities);
        }

        public void remove(T entities)
        {
            dbSet.Remove(entities); //delete from table
        }

        public void removeRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
