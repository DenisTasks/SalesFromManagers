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
    public class ClientRepository : GenericRepository<ModelOfSalesContainer, Client>, ICreateRepository<DAL.Models.Client, Client>
    {
        public ClientRepository(ModelOfSalesContainer modelOfSalesContainer) : base(modelOfSalesContainer)
        {
        }

        public void Create(DAL.Models.Client itemClient)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DAL.Models.Client, Client>()
                .ForMember("Name", opt => opt.MapFrom(c => c.Name))).CreateMapper();
            Client client = mapper.Map<DAL.Models.Client, Client>(itemClient);
            _modelOfSalesContainer.ClientSet.Add(client);
        }

        public void Update(Client itemClient)
        {
            Client client = FindBy(c => c.ClientId == itemClient.ClientId);
            if (client != null)
            {
                client.Name = itemClient.Name;
            }
            else
            {
                throw new ArgumentException("This client ID not found!");
            }
        }
    }
}
