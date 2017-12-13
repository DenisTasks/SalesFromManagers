using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Repositories;
using Entity.Interfaces;
using Entity.MappingClass;

namespace Entity
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IWatcher<SaleInfoRecord> fileWatcher = new FileWatcher())
            {
                fileWatcher.Start();
                Console.WriteLine("Start watching now!");
                Console.WriteLine("Press 'q' to quit");
                while (Console.Read() != 'q') ;
            }
            //IUnitOfWork _unit;
            //using (_unit = new UnitOfWork())
            //{
            //    var go = _unit.SaleInfoRepository.Read();
            //    foreach (var item in go)
            //    {
            //        Console.WriteLine(item.Product.Name);
            //    }
            //}
            //Console.ReadLine();
        }
    }
}
