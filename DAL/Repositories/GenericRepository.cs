using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Model;

namespace DAL.Repositories
{
    public class GenericRepository : IDisposable
    {
        protected ModelOfSalesContainer _modelOfSalesContainer;

        public GenericRepository(ModelOfSalesContainer modelOfSalesContainer)
        {
            _modelOfSalesContainer = modelOfSalesContainer;
        }
        public void Dispose()
        {
            _modelOfSalesContainer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
