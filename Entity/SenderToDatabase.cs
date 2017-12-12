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
        private UnitOfWork _unitOfWork;
        private readonly ICreateRepository<DAL.Models.Manager, Model.Manager> _managerRepository;
        private readonly ICreateRepository<DAL.Models.Product, Model.Product> _productRepository;
        private readonly ICreateRepository<DAL.Models.Client, Model.Client> _clientRepository;
        private readonly ICreateRepository<DAL.Models.SaleInfo, Model.SaleInfo> _saleInfoRepository;

        public SenderToDatabase()
        {
            _unitOfWork = new UnitOfWork();
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
                Model.Manager manager = _unitOfWork.ManagerRepository.FindByEntity(newManager);
                if (manager == null)
                {
                    _unitOfWork.ManagerRepository.Create(newManager);
                    _unitOfWork.ManagerRepository.SaveChanges();
                    manager = _unitOfWork.ManagerRepository.FindByEntity(newManager);
                }

                DAL.Models.Product newProduct = new DAL.Models.Product { Name = item.Product, Price = item.Price };
                _unitOfWork.ProductRepository.Create(newProduct);
                _unitOfWork.ProductRepository.SaveChanges();
                Model.Product product = _unitOfWork.ProductRepository.FindByEntity(newProduct);

                DAL.Models.Client newClient = new DAL.Models.Client { Name = item.Client };
                _unitOfWork.ClientRepository.Create(newClient);
                _unitOfWork.ClientRepository.SaveChanges();
                Model.Client client = _unitOfWork.ClientRepository.FindByEntity(newClient);

                try
                {
                    DAL.Models.SaleInfo newSaleInfo = new DAL.Models.SaleInfo
                    {
                        ProductId = product.ProductId,
                        ClientId = client.ClientId,
                        ManagerId = manager.ManagerId,
                        DateOfSale = item.DateOfSale
                    };
                    _unitOfWork.SaleInfoRepository.Create(newSaleInfo);
                    _unitOfWork.SaleInfoRepository.SaveChanges();
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
