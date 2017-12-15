using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class SaleInfoDTO
    {
        public int SaleInfoId { get; set; }
        public int ProductId { get; set; }
        public int ClientId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public string ClientName { get; set; }
        public string ManagerName { get; set; }
        public int ManagerId { get; set; }
        public string DateOfSale { get; set; }
    }
}
