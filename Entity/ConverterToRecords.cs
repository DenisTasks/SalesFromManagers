using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Interfaces;
using FileHelpers;
using Entity.MappingClass;

namespace Entity
{
    public class ConverterToRecords : IConverter<SaleInfoRecord>
    {
        private readonly FileHelperEngine<SaleInfoRecord> _fileHelper;
        private readonly ISender<SaleInfoRecord> _sender;
        private SaleInfoRecord[] _records;

        public ConverterToRecords()
        {
            _fileHelper = new FileHelperEngine<SaleInfoRecord>();
            _sender = new SenderToDatabase();
            _records = new SaleInfoRecord[] {};
        }
        public void CreateRecords(string path)
        {
            Console.WriteLine("Writing... " + path);
            string fileName = Path.GetFileNameWithoutExtension(path);
            if (fileName != null && fileName.Contains("_") && !fileName.StartsWith("_"))
            {
                string managerLastName = fileName.Split('_').First();
                try
                {
                    _records = _fileHelper.ReadFile(path);
                    foreach (var item in _records)
                    {
                        _sender.Send(managerLastName, item);
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("{0} : The read operation could not be performed", e.GetType().Name);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }
    }
}
