using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using AutoMapper;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class ManagerRepository : GenericRepository, ICreateRepository<DAL.Models.Manager, Model.Manager>
    {
        public void Create(DAL.Models.Manager itemManager)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DAL.Models.Manager, Model.Manager>()
                .ForMember("LastName", opt => opt.MapFrom(m => m.LastName))).CreateMapper();
            Model.Manager manager = mapper.Map<DAL.Models.Manager, Model.Manager>(itemManager);
            _modelOfSalesContainer.ManagerSet.Add(manager);
        }

        public Model.Manager FindById(int id)
        {
            Model.Manager manager = _modelOfSalesContainer.ManagerSet.FirstOrDefault(x => x.ManagerId == id);
            return manager;
        }

        public Model.Manager FindByEntity(DAL.Models.Manager itemManager)
        {
            return _modelOfSalesContainer.ManagerSet.FirstOrDefault(m => m.LastName == itemManager.LastName);
        }

        public IEnumerable<Model.Manager> Read()
        {
            return _modelOfSalesContainer.ManagerSet.AsNoTracking();
        }
        public void Update(DAL.Models.Manager itemManager)
        {
            Model.Manager manager =
                this._modelOfSalesContainer.ManagerSet.FirstOrDefault(m => m.ManagerId == itemManager.ManagerId);
            if (manager != null)
            {
                manager.LastName = itemManager.LastName;
            }
            else
            {
                throw new ArgumentException("This manager ID not found!");
            }
        }
        public void Delete(DAL.Models.Manager itemManager)
        {
            Model.Manager manager =
                _modelOfSalesContainer.ManagerSet.FirstOrDefault(m => m.ManagerId == itemManager.ManagerId);
            if (manager != null)
            {
                _modelOfSalesContainer.ManagerSet.Remove(manager);
            }
            else
            {
                throw new ArgumentException("This manager ID not found!");
            }
        }
        public void SaveChanges()
        {
            _modelOfSalesContainer.SaveChanges();
        }
    }
}

