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
    public class ManagerRepository : ICreateRepository<DAL.Models.Manager, Model.Manager>
    {
        private readonly ModelOfSalesContainer _modelOfSalesContainer;
        public ManagerRepository()
        {
            _modelOfSalesContainer = new ModelOfSalesContainer();
        }

        public void Create(DAL.Models.Manager itemManager)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DAL.Models.Manager, Model.Manager>()
                .ForMember("Name", opt => opt.MapFrom(m => m.Name))
                .ForMember("LastName", opt => opt.MapFrom(m => m.LastName)));
            Model.Manager manager = Mapper.Map<DAL.Models.Manager, Model.Manager>(itemManager);
            _modelOfSalesContainer.ManagerSet.Add(manager);
        }

        public Model.Manager FindById(int id)
        {
            Model.Manager manager = _modelOfSalesContainer.ManagerSet.FirstOrDefault(x => x.ManagerId == id);
            return manager;
        }

        public Model.Manager FindByEntity(DAL.Models.Manager itemManager)
        {
            return _modelOfSalesContainer.ManagerSet.FirstOrDefault(m => m.LastName == itemManager.LastName && m.Name == itemManager.Name);
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
                manager.Name = itemManager.Name;
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
        public void Dispose()
        {
            _modelOfSalesContainer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

