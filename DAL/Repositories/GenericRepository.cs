using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL.Repositories
{
    public abstract class GenericRepository<C, T>
        where C : DbContext, new()
        where T : class 

    {
        private C _entities;

        public C _modelOfSalesContainer
        {
            get { return _entities; }
            set { _entities = value; }
        }

        protected GenericRepository(C modelOfSalesContainer)
        {
            _entities = modelOfSalesContainer;
        }

        public virtual IEnumerable<T> Read()
        {
            return _entities.Set<T>();
        }

        public virtual T FindBy(Expression<Func<T, bool>> predicate)
        {
            T saleInfo = _entities.Set<T>().FirstOrDefault(predicate);
            return saleInfo;
        }

        public virtual void Delete(T item)
        {
            if (item != null)
            {
                _entities.Set<T>().Remove(item);
            }
            else
            {
                throw new ArgumentException("This information not found!");
            }
        }

        public virtual void SaveChanges()
        {
            _entities.SaveChanges();
        }
    }
}
