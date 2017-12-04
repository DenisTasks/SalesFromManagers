using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories;
using DAL.Models;

namespace Entity
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
                //Product test5 = new Product { Name = "Lime1", Price = 6661, ProductId = 8};
                //foreach (var item in pr.Read())
                //{
                //    Console.WriteLine(item.ProductId + item.Name + item.Price);
                //}
                //pr.Update(test5);
                //pr.SaveChanges();
                //foreach (var item in pr.Read())
                //{
                //    Console.WriteLine(item.ProductId + item.Name + item.Price);
                //}
                //pr.Delete(test5);
                //pr.SaveChanges();

                foreach (var item in pr.Read())
                {
                    Console.WriteLine(item.ProductId + item.Name + item.Price);
                }
                Console.WriteLine("done");
                Console.ReadLine();
            }
        }
    }
}
