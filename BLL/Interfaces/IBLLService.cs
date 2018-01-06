using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IBLLService : IDisposable
    {
        SelectLists DistinctItems();
        IEnumerable<SaleInfoDTO> GetSaleInfo();
        IEnumerable<SaleInfoDTO> GetSaleInfo(int skipItems, int takeItems);
        SaleInfoDTO FindSaleInfoById(int id);
        void DeleteSaleInfoById(int id);
        void UpdateSaleInfo(SaleInfoDTO item);
        ChartInfo GetChartInfo(SaleInfoDTO item);
    }
}
