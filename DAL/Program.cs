using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Repositories;

namespace DAL
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ProductRepository pr = new ProductRepository())
            {
                Client test = new Client { ClientId = 999, Name = "Test" };
                Manager test2 = new Manager { LastName = "Epamov", Name = "Epam" };
                Product test3 = new Product { Name = "Lime", Price = 666 };
                SaleInfo test4 = new SaleInfo { ClientId = test.ClientId, ManagerId = test2.ManagerId, ProductId = test3.ProductId, DateOfSale = DateTime.Now };
                pr.Create(test3);
                pr.SaveChanges();
                Console.WriteLine("done");
                Console.ReadLine();
            }
        }
    }
}
