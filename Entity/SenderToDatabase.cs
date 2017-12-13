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
        private IUnitOfWork _unitOfWork;
        private readonly object _lockObj = new object();

        public void Send(string managerLastName, SaleInfoRecord item)
        {
            lock (_lockObj)
            {
                using (_unitOfWork = new UnitOfWork())
                {
                    using (var transaction = _unitOfWork.BeginTransaction())
                    {
                        try
                        {
                            DAL.Models.Manager newManager = new DAL.Models.Manager { LastName = managerLastName };
                            Model.Manager manager = _unitOfWork.ManagerRepository.FindByEntity(newManager);
                            if (manager == null)
                            {
                                _unitOfWork.ManagerRepository.Create(newManager);
                                _unitOfWork.Save();
                                manager = _unitOfWork.ManagerRepository.FindByEntity(newManager);
                            }

                            DAL.Models.Product newProduct = new DAL.Models.Product { Name = item.Product, Price = item.Price };
                            _unitOfWork.ProductRepository.Create(newProduct);

                            DAL.Models.Client newClient = new DAL.Models.Client { Name = item.Client };
                            _unitOfWork.ClientRepository.Create(newClient);

                            _unitOfWork.Save();

                            Model.Product product = _unitOfWork.ProductRepository.FindByEntity(newProduct);
                            Model.Client client = _unitOfWork.ClientRepository.FindByEntity(newClient);

                            DAL.Models.SaleInfo newSaleInfo = new DAL.Models.SaleInfo
                            {
                                ProductId = product.ProductId,
                                ClientId = client.ClientId,
                                ManagerId = manager.ManagerId,
                                DateOfSale = item.DateOfSale
                            };
                            _unitOfWork.SaleInfoRepository.Create(newSaleInfo);
                            _unitOfWork.Save();
                            transaction.Commit();
                        }
                        catch (Exception e)
                        {
                            transaction.Rollback();
                            throw new Exception(e.Source + " : Crash in sender from " + managerLastName);
                        }
                    }
                }
            }
        }
    }
}
