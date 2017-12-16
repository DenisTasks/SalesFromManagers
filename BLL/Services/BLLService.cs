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
            var allSaleInfoMapper = new MapperConfiguration(cfg => cfg.CreateMap<SaleInfo, SaleInfoDTO>()
                .ForMember("SaleInfoId", opt => opt.MapFrom(s => s.SaleInfoId))
                .ForMember("DateOfSale", opt => opt.MapFrom(s => s.DateOfSale))
                .ForMember("ProductName", opt => opt.MapFrom(s => s.Product.Name))
                .ForMember("ProductPrice", opt => opt.MapFrom(s => s.Product.Price))
                .ForMember("ClientName", opt => opt.MapFrom(s => s.Client.Name))
                .ForMember("ManagerName", opt => opt.MapFrom(s => s.Manager.LastName))).CreateMapper();
            return allSaleInfoMapper.Map<IEnumerable<SaleInfo>, List<SaleInfoDTO>>(Database.SaleInfoRepository.Read());
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
