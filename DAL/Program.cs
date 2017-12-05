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
            using (SaleInfoRepository pr = new SaleInfoRepository())
            {
                Client test = new Client { ClientId = 999, Name = "Test" };
                Manager test2 = new Manager { LastName = "Epamov" };
                Product test3 = new Product { Name = "Lime", Price = 666 };
                string s = DateTime.Now.Date.ToString();
                SaleInfo test4 = new SaleInfo { ClientId = test.ClientId, ManagerId = test2.ManagerId, ProductId = test3.ProductId, DateOfSale = s };
                pr.Create(test4);
                pr.SaveChanges();
                Console.WriteLine("done");
                Console.ReadLine();
            }
        }
    }
}
