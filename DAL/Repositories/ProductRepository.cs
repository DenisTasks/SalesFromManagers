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
    public class ProductRepository : GenericRepository, ICreateRepository<DAL.Models.Product, Model.Product>
    {
        public void Create(DAL.Models.Product itemProduct)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DAL.Models.Product, Model.Product>()
                .ForMember("Name", opt => opt.MapFrom(p => p.Name))
                .ForMember("Price", opt => opt.MapFrom(p => p.Price))).CreateMapper();
            Model.Product product = mapper.Map<DAL.Models.Product, Model.Product>(itemProduct);
            _modelOfSalesContainer.ProductSet.Add(product);
        }

        public Model.Product FindById(int id)
        {
            Model.Product product = _modelOfSalesContainer.ProductSet.FirstOrDefault(x => x.ProductId == id);
            return product;
        }

        public Model.Product FindByEntity(DAL.Models.Product itemProduct)
        {
            return _modelOfSalesContainer.ProductSet.FirstOrDefault(p => p.Name == itemProduct.Name);
        }

        public IEnumerable<Model.Product> Read()
        {
            return _modelOfSalesContainer.ProductSet.AsNoTracking();
        }
        public void Update(DAL.Models.Product itemProduct)
        {
            Model.Product product =
                this._modelOfSalesContainer.ProductSet.FirstOrDefault(p => p.ProductId == itemProduct.ProductId);
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
        public void Delete(DAL.Models.Product itemProduct)
        {
            Model.Product product =
                _modelOfSalesContainer.ProductSet.FirstOrDefault(p => p.ProductId == itemProduct.ProductId);
            if (product != null)
            {
                _modelOfSalesContainer.ProductSet.Remove(product);
            }
            else
            {
                throw new ArgumentException("This product ID not found!");
            }
        }
        public void SaveChanges()
        {
            _modelOfSalesContainer.SaveChanges();
        }
    }
}
