using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Repositories;
using Model;

namespace BLL.Services
{
    public class BLLService : IBLLService
    {
        IUnitOfWork Database { get; set; }

        public BLLService(IUnitOfWork uOw)
        {
            Database = uOw;
        }

        public IEnumerable<SaleInfoDTO> GetSaleInfo()
        {
            var saleInfoMapper = new MapperConfiguration(cfg => cfg.CreateMap<SaleInfo, SaleInfoDTO>()
                .ForMember("SaleInfoId", opt => opt.MapFrom(s => s.SaleInfoId))
                .ForMember("DateOfSale", opt => opt.MapFrom(s => s.DateOfSale))
                .ForMember("ProductName", opt => opt.MapFrom(s => s.Product.Name))
                .ForMember("ProductPrice", opt => opt.MapFrom(s => s.Product.Price))
                .ForMember("ClientName", opt => opt.MapFrom(s => s.Client.Name))
                .ForMember("ManagerName", opt => opt.MapFrom(s => s.Manager.LastName))).CreateMapper();
            return saleInfoMapper.Map<IEnumerable<SaleInfo>, List<SaleInfoDTO>>(Database.SaleInfoRepository.Read());
        }

        public SaleInfoDTO FindSaleInfoById(int id)
        {
            var saleInfoMapper = new MapperConfiguration(cfg => cfg.CreateMap<SaleInfo, SaleInfoDTO>()
                .ForMember("SaleInfoId", opt => opt.MapFrom(s => s.SaleInfoId))
                .ForMember("DateOfSale", opt => opt.MapFrom(s => s.DateOfSale))
                .ForMember("ProductName", opt => opt.MapFrom(s => s.Product.Name))
                .ForMember("ProductPrice", opt => opt.MapFrom(s => s.Product.Price))
                .ForMember("ClientName", opt => opt.MapFrom(s => s.Client.Name))
                .ForMember("ManagerName", opt => opt.MapFrom(s => s.Manager.LastName))).CreateMapper();
            return saleInfoMapper.Map<SaleInfo, SaleInfoDTO>(Database.SaleInfoRepository.FindBy(x => x.SaleInfoId == id));
        }

        public void DeleteSaleInfoById(int id)
        {
            using (var transaction = Database.BeginTransaction())
            {
                try
                {
                    var deleteItem = Database.SaleInfoRepository.FindBy(x => x.SaleInfoId == id);
                    Database.SaleInfoRepository.Delete(deleteItem);
                    Database.Save();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Source + " : Crash in BLLService from DeleteSaleInfoById");
                }
            }
        }

        public void UpdateSaleInfo(SaleInfoDTO item)
        {
            using (var transaction = Database.BeginTransaction())
            {
                try
                {
                    var updateMapperSaleInfo = new MapperConfiguration(cfg => cfg.CreateMap<SaleInfoDTO, SaleInfo>()
                        .ForMember("SaleInfoId", opt => opt.MapFrom(s => s.SaleInfoId))
                        .ForMember("ProductId", opt => opt.MapFrom(s => s.ProductId))
                        .ForMember("ClientId", opt => opt.MapFrom(s => s.ClientId))
                        .ForMember("ManagerId", opt => opt.MapFrom(s => s.ManagerId))
                        .ForMember("DateOfSale", opt => opt.MapFrom(s => s.DateOfSale))).CreateMapper();
                    var updateItemSaleInfo = updateMapperSaleInfo.Map<SaleInfoDTO, SaleInfo>(item);
                    Database.SaleInfoRepository.Update(updateItemSaleInfo);

                    var updateMapperClient = new MapperConfiguration(cfg => cfg.CreateMap<SaleInfoDTO, Client>()
                        .ForMember("ClientId", opt => opt.MapFrom(s => s.ClientId))
                        .ForMember("Name", opt => opt.MapFrom(s => s.ClientName))).CreateMapper();
                    var updateItemClient = updateMapperClient.Map<SaleInfoDTO, Client>(item);
                    Database.ClientRepository.Update(updateItemClient);

                    var updateMapperProduct = new MapperConfiguration(cfg => cfg.CreateMap<SaleInfoDTO, Product>()
                        .ForMember("ProductId", opt => opt.MapFrom(s => s.ProductId))
                        .ForMember("Name", opt => opt.MapFrom(s => s.ProductName))
                        .ForMember("Price", opt => opt.MapFrom(s => s.ProductPrice))).CreateMapper();
                    var updateItemProduct = updateMapperProduct.Map<SaleInfoDTO, Product>(item);
                    Database.ProductRepository.Update(updateItemProduct);

                    Database.Save();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Source + " : Crash in BLLService from UpdateSaleInfo");
                }
            }
        }

        public ChartInfo GetChartInfo(SaleInfoDTO saleInfo)
        {
            var specificSaleInfo = GetSaleInfo().Where(x => x.ManagerName == saleInfo.ManagerName).ToList();
            ChartInfo chartInfo = new ChartInfo();
            foreach (var item in specificSaleInfo)
            {
                if (!chartInfo.Products.Contains(item.ProductName))
                {
                    chartInfo.Products.Add(item.ProductName);
                    chartInfo.Count.Add(specificSaleInfo.Count(x => x.ProductName == item.ProductName) * item.ProductPrice);
                    chartInfo.Summ += (specificSaleInfo.Count(x => x.ProductName == item.ProductName) * item.ProductPrice);
                }
            }
            return chartInfo;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Database.Dispose();
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
