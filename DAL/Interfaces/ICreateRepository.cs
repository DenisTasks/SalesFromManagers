using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICreateRepository<TIn, TOut>
        where TIn : class
        where TOut : class
    {
        void Create(TIn item);
        IEnumerable<TOut> Distinct(Expression<Func<TOut, string>> predicate);
        IEnumerable<TOut> Read();
        IEnumerable<TOut> Read(Expression<Func<TOut, int>> predicate, int skipItems, int takeItems);
        void Update(TOut item);
        void Delete(TOut item);
        TOut FindBy(Expression<Func<TOut, bool>> predicate);
    }
}
