using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
                Console.WriteLine("Start watching now!");
                Console.WriteLine("Press 'q' to quit");
                while (Console.Read() != 'q') ;
            }
        }
    }
}
