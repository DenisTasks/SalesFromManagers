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
    public class SaleInfoRepository : GenericRepository<ModelOfSalesContainer, SaleInfo>, ICreateRepository<DAL.Models.SaleInfo, SaleInfo>
    {
        public SaleInfoRepository(ModelOfSalesContainer modelOfSalesContainer) : base(modelOfSalesContainer)
        {
        }

        public void Create(DAL.Models.SaleInfo itemSaleInfo)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DAL.Models.SaleInfo, SaleInfo>()
                .ForMember("ProductId", opt => opt.MapFrom(s => s.ProductId))
                .ForMember("ClientId", opt => opt.MapFrom(s => s.ClientId))
                .ForMember("ManagerId", opt => opt.MapFrom(s => s.ManagerId))
                .ForMember("DateOfSale", opt => opt.MapFrom(s => s.DateOfSale))).CreateMapper();
            SaleInfo saleInfo = mapper.Map<DAL.Models.SaleInfo, SaleInfo>(itemSaleInfo);
            _modelOfSalesContainer.SaleInfoSet.Add(saleInfo);
        }

        public void Update(SaleInfo itemSaleInfo)
        {
            SaleInfo saleInfo = FindBy(s => s.SaleInfoId == itemSaleInfo.SaleInfoId);
            if (saleInfo != null)
            {
                saleInfo.DateOfSale = itemSaleInfo.DateOfSale;
            }
            else
            {
                throw new ArgumentException("This information of sale ID not found!");
            }
        }
    }
}

