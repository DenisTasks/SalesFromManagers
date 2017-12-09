using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsService.MappingClass;
using FileHelpers;

namespace WindowsService.Interfaces
{
    public interface IConverter<T> where T : class
    {
        FileHelperEngine<T> FileHelper { get; }
        ISender<T> Sender { get; }
        T[] Records { get; }
        void CreateRecords(string path);
    }
}
