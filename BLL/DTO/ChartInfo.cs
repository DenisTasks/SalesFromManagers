using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class ChartInfo
    {
        public List<string> Products { get; set; }
        public List<int> Count { get; set; }
        public int Summ { get; set; }

        public ChartInfo()
        {
            Products = new List<string>();
            Count = new List<int>();
            Summ = 0;
        }
    }
}
