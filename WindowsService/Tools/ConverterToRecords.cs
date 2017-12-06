using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsService.MappingClass;
using FileHelpers;

namespace WindowsService.Tools
{
    public class ConverterToRecords
    {
        private readonly FileHelperEngine<SaleInfoCsv> _fileHelper;
        private readonly SenderToDatabase _sender;

        public ConverterToRecords()
        {
            _fileHelper = new FileHelperEngine<SaleInfoCsv>();
            _sender = new SenderToDatabase();
        }
        public void CreateRecords(string path)
        {
            string fileName = Path.GetFileNameWithoutExtension(path);
            string managerLastName = fileName.Split('_').First();
            if (managerLastName != null)
            {
                SaleInfoCsv[] records = _fileHelper.ReadFile(path);
                foreach (var item in records)
                {
                    _sender.Send(managerLastName, item);
                }
            }
        }
    }
}
