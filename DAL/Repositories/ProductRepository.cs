using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using AutoMapper;

namespace DAL.Repositories
{
    public class ProductRepository : IDisposable
    {
        private readonly ModelOfSalesContainer _modelOfSalesContainer;
        public ProductRepository()
        {
            _modelOfSalesContainer = new ModelOfSalesContainer();
        }

        public void Create(DAL.Models.Product itemProduct)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DAL.Models.Product, Model.Product>()
                .ForMember("ProductId", opt => opt.MapFrom(p => p.ProductId))
                .ForMember("Name", opt => opt.MapFrom(p => p.Name))
                .ForMember("Price", opt => opt.MapFrom(p => p.Price)));
            Model.Product product = Mapper.Map<DAL.Models.Product, Model.Product>(itemProduct);
            _modelOfSalesContainer.ProductSet.Add(product);
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
