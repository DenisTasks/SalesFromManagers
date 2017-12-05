using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class SaleInfo
    {
        public int SaleInfoId { get; set; }
        public int ProductId { get; set; }
        public int ClientId { get; set; }
        public int ManagerId { get; set; }
        public string DateOfSale { get; set; }
    }
}
