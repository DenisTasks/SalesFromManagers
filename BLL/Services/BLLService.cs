using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using DAL.Interfaces;
using DAL.Repositories;

namespace BLL.Services
{
    public class BLLService
    {
        IUnitOfWork Database { get; set; }

        public BLLService()
        {
            Database = new UnitOfWork();
        }

        public IEnumerable<ManagerDTO> GetManagers()
        {
            return Mapper.Map
        }
    }
}
