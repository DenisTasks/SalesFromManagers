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
    public class ProductRepository : GenericRepository<ModelOfSalesContainer, Product>, ICreateRepository<DAL.Models.Product, Product>
    {
        public ProductRepository(ModelOfSalesContainer modelOfSalesContainer) : base(modelOfSalesContainer)
        {
        }

        public void Create(DAL.Models.Product itemProduct)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DAL.Models.Product, Product>()
                .ForMember("Name", opt => opt.MapFrom(p => p.Name))
                .ForMember("Price", opt => opt.MapFrom(p => p.Price))).CreateMapper();
            Product product = mapper.Map<DAL.Models.Product, Product>(itemProduct);
            _modelOfSalesContainer.ProductSet.Add(product);
        }

        public void Update(Product itemProduct)
        {
            Product product = FindBy(p => p.ProductId == itemProduct.ProductId);
            if (product != null)
            {
                product.Name = itemProduct.Name;
                product.Price = itemProduct.Price;
            }
            else
            {
                throw new ArgumentException("This product ID not found!");
            }
        }
    }
}
