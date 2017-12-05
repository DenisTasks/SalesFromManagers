using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ModelOfSalesContainer db = new ModelOfSalesContainer())
            {
                Client test = new Client { Name = "Dzianis" };
                db.ClientSet.Add(test);
                Manager test2 = new Manager { LastName = "Tarasevich" };
                db.ManagerSet.Add(test2);
                Product test3 = new Product { Name = "Apple", Price = 100500 };
                db.ProductSet.Add(test3);
                SaleInfo test4 = new SaleInfo { ClientId = test.ClientId, ManagerId = test2.ManagerId, ProductId = test3.ProductId, DateOfSale = "01122017" };
                db.SaleInfoSet.Add(test4);
                db.SaveChanges();

                foreach (var item in db.SaleInfoSet)
                {
                    Console.WriteLine(item.Client.Name + item.Product.Name);
                }
            }
            Console.ReadKey();
        }
    }
}
