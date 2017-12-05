using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace Entity.MappingClass
{
    [DelimitedRecord(",")]
    public class SaleInfoCsv
    {
        public string DateOfSale;
        public string Client;
        public string Product;
        public int Price;
    }
}
