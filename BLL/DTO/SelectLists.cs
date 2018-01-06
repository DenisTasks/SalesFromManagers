using System.Collections.Generic;
using System.Linq;

namespace BLL.DTO
{
    public class SelectLists
    {
        public List<string> Managers { get; set; }
        public List<string> Products { get; set; }
        public List<string> DateOfSales { get; set; }
        public SelectLists(IEnumerable<string> managersList, IEnumerable<string> dateOfSalesList, IEnumerable<string> productsList)
        {
            Managers = managersList.ToList();
            Products = productsList.ToList();
            DateOfSales = dateOfSalesList.ToList();

            DateOfSales.Sort();
            Managers.Sort();
            Products.Sort();
            DateOfSales.Insert(0, "All");
            Managers.Insert(0, "All");
            Products.Insert(0, "All");
        }
    }
}
