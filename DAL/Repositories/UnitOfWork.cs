using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Model;

namespace DAL.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private readonly ModelOfSalesContainer _db = new ModelOfSalesContainer();

        private ProductRepository _productRepository;
        private ClientRepository _clientRepository;
        private ManagerRepository _managerRepository;
        private SaleInfoRepository _saleInfoRepository;

        public ICreateRepository<DAL.Models.Product, Model.Product> ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository();
                }
                return _productRepository;
            }
        }

        public ICreateRepository<DAL.Models.Client, Model.Client> ClientRepository
        {
            get
            {
                if (_clientRepository == null)
                {
                    _clientRepository = new ClientRepository();
                }
                return _clientRepository;
            }
        }

        public ICreateRepository<DAL.Models.Manager, Model.Manager> ManagerRepository
        {
            get
            {
                if (_managerRepository == null)
                {
                    _managerRepository = new ManagerRepository();
                }
                return _managerRepository;
            }
        }

        public ICreateRepository<DAL.Models.SaleInfo, Model.SaleInfo> SaleInfoRepository
        {
            get
            {
                if (_saleInfoRepository == null)
                {
                    _saleInfoRepository = new SaleInfoRepository();
                }
                return _saleInfoRepository;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
