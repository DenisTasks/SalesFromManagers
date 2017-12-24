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
        IEnumerable<SaleInfoDTO> GetSaleInfo();
        SaleInfoDTO FindSaleInfoById(int id);
        void DeleteSaleInfoById(int id);
        void UpdateSaleInfo(SaleInfoDTO item);
        ChartInfo GetChartInfo(SaleInfoDTO item);
        void Dispose();
    }
}
