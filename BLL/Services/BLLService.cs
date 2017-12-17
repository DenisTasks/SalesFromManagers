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

        public IEnumerable<ManagerDTO> GetManagers()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Manager, ManagerDTO>());
            return Mapper.Map<IEnumerable<Manager>, List<ManagerDTO>>(Database.ManagerRepository.Read());
        }

        public IEnumerable<ClientDTO> GetClients()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Client, ClientDTO>());
            return Mapper.Map<IEnumerable<Client>, List<ClientDTO>>(Database.ClientRepository.Read());
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductDTO>());
            return Mapper.Map<IEnumerable<Product>, List<ProductDTO>>(Database.ProductRepository.Read());
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
            return saleInfoMapper.Map<SaleInfo, SaleInfoDTO>(Database.SaleInfoRepository.FindById(id));
        }

        public void DeleteSaleInfoById(int id)
        {
            using (var transaction = Database.BeginTransaction())
            {
                try
                {
                    var deleteItem = Database.SaleInfoRepository.FindById(id);
                    Database.SaleInfoRepository.Delete2(deleteItem);
                    Database.Save();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Source + " : Crash in BLLService from SaleInfo id: " + id);
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
                    Database.SaleInfoRepository.Update2(updateItemSaleInfo);

                    var updateMapperClient = new MapperConfiguration(cfg => cfg.CreateMap<SaleInfoDTO, Client>()
                        .ForMember("ClientId", opt => opt.MapFrom(s => s.ClientId))
                        .ForMember("Name", opt => opt.MapFrom(s => s.ClientName))).CreateMapper();
                    var updateItemClient = updateMapperClient.Map<SaleInfoDTO, Client>(item);
                    Database.ClientRepository.Update2(updateItemClient);

                    var updateMapperProduct = new MapperConfiguration(cfg => cfg.CreateMap<SaleInfoDTO, Product>()
                        .ForMember("ProductId", opt => opt.MapFrom(s => s.ProductId))
                        .ForMember("Name", opt => opt.MapFrom(s => s.ProductName))
                        .ForMember("Price", opt => opt.MapFrom(s => s.ProductPrice))).CreateMapper();
                    var updateItemProduct = updateMapperProduct.Map<SaleInfoDTO, Product>(item);
                    Database.ProductRepository.Update2(updateItemProduct);

                    Database.Save();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Source + " : Crash in BLLService from SaleInfo: ");
                }
            }
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
