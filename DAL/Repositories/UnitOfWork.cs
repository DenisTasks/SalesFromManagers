using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Model;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ModelOfSalesContainer _db = new ModelOfSalesContainer();

        private ICreateRepository<DAL.Models.Product, Model.Product> _productRepository;
        private ICreateRepository<DAL.Models.Client, Model.Client> _clientRepository;
        private ICreateRepository<DAL.Models.Manager, Model.Manager> _managerRepository;
        private ICreateRepository<DAL.Models.SaleInfo, Model.SaleInfo> _saleInfoRepository;

        public DbContextTransaction BeginTransaction()
        {
            return _db.Database.BeginTransaction();
        }
        public ICreateRepository<DAL.Models.Product, Model.Product> ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_db);
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
                    _clientRepository = new ClientRepository(_db);
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
                    _managerRepository = new ManagerRepository(_db);
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
                    _saleInfoRepository = new SaleInfoRepository(_db);
                }
                return _saleInfoRepository;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
