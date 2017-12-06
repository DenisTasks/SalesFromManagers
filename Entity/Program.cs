using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Entity
{
    class Program
    {
        static void Main(string[] args)
        {
            using (FileWatcher fileWatcher = new FileWatcher("D:\\Entity"))
            {
                Console.WriteLine("Start watching now!");
                Console.WriteLine("Press 'q' to quit");
                while (Console.Read() != 'q') ;
            }
        }
    }
}
