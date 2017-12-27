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
    public class ManagerRepository : GenericRepository<ModelOfSalesContainer, Manager>, ICreateRepository<DAL.Models.Manager, Manager>
    {
        public ManagerRepository(ModelOfSalesContainer modelOfSalesContainer) : base(modelOfSalesContainer)
        {
        }

        public void Create(DAL.Models.Manager itemManager)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DAL.Models.Manager, Manager>()
                .ForMember("LastName", opt => opt.MapFrom(m => m.LastName))).CreateMapper();
            Manager manager = mapper.Map<DAL.Models.Manager, Manager>(itemManager);
            _modelOfSalesContainer.ManagerSet.Add(manager);
        }

        public void Update(Manager itemManager)
        {
            Manager manager = FindBy(m => m.ManagerId == itemManager.ManagerId);
            if (manager != null)
            {
                manager.LastName = itemManager.LastName;
            }
            else
            {
                throw new ArgumentException("This manager ID not found!");
            }
        }
    }
}

