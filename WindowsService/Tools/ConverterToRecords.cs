using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsService.Interfaces;
using WindowsService.MappingClass;
using FileHelpers;

namespace WindowsService.Tools
{
    public class ConverterToRecords : IConverter<SaleInfoRecord>
    {
        public FileHelperEngine<SaleInfoRecord> FileHelper { get; }
        public ISender<SaleInfoRecord> Sender { get; }
        public SaleInfoRecord[] Records { get; private set; }

        public ConverterToRecords()
        {
            FileHelper = new FileHelperEngine<SaleInfoRecord>();
            Sender = new SenderToDatabase();
            Records = new SaleInfoRecord[] { };
        }
        public void CreateRecords(string path)
        {
            string fileName = Path.GetFileNameWithoutExtension(path);
            if (fileName != null && fileName.Contains("_") && !fileName.StartsWith("_"))
            {
                string managerLastName = fileName.Split('_').First();
                Records = FileHelper.ReadFile(path);
                foreach (var item in Records)
                {
                    Sender.Send(managerLastName, item);
                }
            }
        }
    }
}
