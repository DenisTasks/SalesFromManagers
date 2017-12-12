using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICreateRepository<DAL.Models.Product, Model.Product> ProductRepository { get; }
        ICreateRepository<DAL.Models.Client, Model.Client> ClientRepository { get; }
        ICreateRepository<DAL.Models.Manager, Model.Manager> ManagerRepository { get; }
        ICreateRepository<DAL.Models.SaleInfo, Model.SaleInfo> SaleInfoRepository { get; }
        void Save();
    }
}
