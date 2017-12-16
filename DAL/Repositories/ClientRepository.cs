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
    public class ClientRepository : GenericRepository, ICreateRepository<DAL.Models.Client, Model.Client>
    {
        public ClientRepository(ModelOfSalesContainer modelOfSalesContainer) : base(modelOfSalesContainer)
        {
        }

        public void Create(DAL.Models.Client itemClient)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DAL.Models.Client, Model.Client>()
                .ForMember("Name", opt => opt.MapFrom(c => c.Name))).CreateMapper();
            Model.Client client = mapper.Map<DAL.Models.Client, Model.Client>(itemClient);
            _modelOfSalesContainer.ClientSet.Add(client);
        }

        public Model.Client FindById(int id)
        {
            Model.Client client = _modelOfSalesContainer.ClientSet.FirstOrDefault(x => x.ClientId == id);
            return client;
        }

        public Model.Client FindByEntity(DAL.Models.Client itemClient)
        {
            return _modelOfSalesContainer.ClientSet.FirstOrDefault(c => c.Name == itemClient.Name);
        }

        public IEnumerable<Model.Client> Read()
        {
            return _modelOfSalesContainer.ClientSet;
        }
        public void Update(DAL.Models.Client itemClient)
        {
            Model.Client client =
                this._modelOfSalesContainer.ClientSet.FirstOrDefault(c => c.ClientId == itemClient.ClientId);
            if (client != null)
            {
                client.Name = itemClient.Name;
            }
            else
            {
                throw new ArgumentException("This client ID not found!");
            }
        }

        public void Update2(Model.Client itemClient)
        {
            Model.Client client =
                this._modelOfSalesContainer.ClientSet.FirstOrDefault(c => c.ClientId == itemClient.ClientId);
            if (client != null)
            {
                client.Name = itemClient.Name;
            }
            else
            {
                throw new ArgumentException("This client ID not found!");
            }
        }
        public void Delete(DAL.Models.Client itemClient)
        {
            Model.Client client =
                _modelOfSalesContainer.ClientSet.FirstOrDefault(c => c.ClientId == itemClient.ClientId);
            if (client != null)
            {
                _modelOfSalesContainer.ClientSet.Remove(client);
            }
            else
            {
                throw new ArgumentException("This client ID not found!");
            }
        }

        public void Delete2(Model.Client itemClient)
        {
            Model.Client client =
                _modelOfSalesContainer.ClientSet.FirstOrDefault(c => c.ClientId == itemClient.ClientId);
            if (client != null)
            {
                _modelOfSalesContainer.ClientSet.Remove(client);
            }
            else
            {
                throw new ArgumentException("This client ID not found!");
            }
        }

        public void SaveChanges()
        {
            _modelOfSalesContainer.SaveChanges();
        }
    }
}
