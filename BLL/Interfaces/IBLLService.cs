using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IBLLService
    {
        IEnumerable<ManagerDTO> GetManagers();
        IEnumerable<SaleInfoDTO> GetSaleInfo();
        void Dispose();
    }
}
