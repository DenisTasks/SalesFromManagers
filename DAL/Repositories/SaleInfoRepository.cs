using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using AutoMapper;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class SaleInfoRepository : ICreateRepository<DAL.Models.SaleInfo, Model.SaleInfo>
    {
        private readonly ModelOfSalesContainer _modelOfSalesContainer;
        public SaleInfoRepository()
        {
            _modelOfSalesContainer = new ModelOfSalesContainer();
        }

        public void Create(DAL.Models.SaleInfo itemSaleInfo)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DAL.Models.SaleInfo, Model.SaleInfo>()
                .ForMember("ProductId", opt => opt.MapFrom(s => s.ProductId))
                .ForMember("ClientId", opt => opt.MapFrom(s => s.ClientId))
                .ForMember("ManagerId", opt => opt.MapFrom(s => s.ManagerId))
                .ForMember("DateOfSale", opt => opt.MapFrom(s => s.DateOfSale)));
            Model.SaleInfo saleInfo = Mapper.Map<DAL.Models.SaleInfo, Model.SaleInfo>(itemSaleInfo);
            _modelOfSalesContainer.SaleInfoSet.Add(saleInfo);
        }

        public Model.SaleInfo FindById(int id)
        {
            Model.SaleInfo saleInfo = _modelOfSalesContainer.SaleInfoSet.FirstOrDefault(x => x.SaleInfoId == id);
            return saleInfo;
        }

        public Model.SaleInfo FindByEntity(DAL.Models.SaleInfo itemSaleInfo)
        {
            return _modelOfSalesContainer.SaleInfoSet.FirstOrDefault(x => x.SaleInfoId == itemSaleInfo.SaleInfoId);
        }

        public IEnumerable<Model.SaleInfo> Read()
        {
            return _modelOfSalesContainer.SaleInfoSet.AsNoTracking();
        }
        public void Update(DAL.Models.SaleInfo itemSaleInfo)
        {
            Model.SaleInfo saleInfo =
                this._modelOfSalesContainer.SaleInfoSet.FirstOrDefault(s => s.SaleInfoId == itemSaleInfo.SaleInfoId);
            if (saleInfo != null)
            {
                saleInfo.ProductId = itemSaleInfo.ProductId;
                saleInfo.ClientId = itemSaleInfo.ClientId;
                saleInfo.ManagerId = itemSaleInfo.ManagerId;
                saleInfo.DateOfSale = itemSaleInfo.DateOfSale;
            }
            else
            {
                throw new ArgumentException("This information of sale ID not found!");
            }
        }
        public void Delete(DAL.Models.SaleInfo itemSaleInfo)
        {
            Model.SaleInfo saleInfo =
                _modelOfSalesContainer.SaleInfoSet.FirstOrDefault(s => s.SaleInfoId == itemSaleInfo.SaleInfoId);
            if (saleInfo != null)
            {
                _modelOfSalesContainer.SaleInfoSet.Remove(saleInfo);
            }
            else
            {
                throw new ArgumentException("This information of sale ID not found!");
            }
        }
        public void SaveChanges()
        {
            _modelOfSalesContainer.SaveChanges();
        }
        public void Dispose()
        {
            _modelOfSalesContainer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

