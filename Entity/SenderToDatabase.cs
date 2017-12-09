using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories;
using Entity.MappingClass;
using Entity.Interfaces;

namespace Entity
{
    public class SenderToDatabase : ISender<SaleInfoRecord>
    {
        private readonly ICreateRepository<DAL.Models.Manager, Model.Manager> _managerRepository;
        private readonly ICreateRepository<DAL.Models.Product, Model.Product> _productRepository;
        private readonly ICreateRepository<DAL.Models.Client, Model.Client> _clientRepository;
        private readonly ICreateRepository<DAL.Models.SaleInfo, Model.SaleInfo> _saleInfoRepository;

        public SenderToDatabase()
        {
            _managerRepository = new ManagerRepository();
            _productRepository = new ProductRepository();
            _clientRepository = new ClientRepository();
            _saleInfoRepository = new SaleInfoRepository();
        }

        public void Send(string managerLastName, SaleInfoRecord item)
        {
            lock (this)
            {
                DAL.Models.Manager newManager = new DAL.Models.Manager { LastName = managerLastName };
                Model.Manager manager = _managerRepository.FindByEntity(newManager);
                // if not found
                if (manager == null)
                {
                    _managerRepository.Create(newManager);
                    _managerRepository.SaveChanges();
                    manager = _managerRepository.FindByEntity(newManager);
                }

                DAL.Models.Product newProduct = new DAL.Models.Product { Name = item.Product, Price = item.Price };
                _productRepository.Create(newProduct);
                _productRepository.SaveChanges();
                Model.Product product = _productRepository.FindByEntity(newProduct);

                DAL.Models.Client newClient = new DAL.Models.Client { Name = item.Client };
                _clientRepository.Create(newClient);
                _clientRepository.SaveChanges();
                Model.Client client = _clientRepository.FindByEntity(newClient);

                try
                {
                    DAL.Models.SaleInfo newSaleInfo = new DAL.Models.SaleInfo
                    {
                        ProductId = product.ProductId,
                        ClientId = client.ClientId,
                        ManagerId = manager.ManagerId,
                        DateOfSale = item.DateOfSale
                    };
                    _saleInfoRepository.Create(newSaleInfo);
                    _saleInfoRepository.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                    var fullErrorMessage = string.Join("; ", errorMessages);
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                }
            }
        }
    }
}
