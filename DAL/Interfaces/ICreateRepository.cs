using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICreateRepository<TIn, TOut> : IDisposable
    {
        void Create(TIn item);
        TOut FindById(int id);
        TOut FindByEntity(TIn item);
        IEnumerable<TOut> Read();
        void Update(TIn item);
        void Delete(TIn item);
        void SaveChanges();
    }
}
