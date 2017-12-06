using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using WindowsService.Tools;

namespace WindowsService
{
    public partial class CsvService : ServiceBase
    {
        private FileWatcherService _fileWatcher;
        public CsvService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _fileWatcher = new FileWatcherService("D:\\Entity");
        }

        protected override void OnStop()
        {
            _fileWatcher.Dispose();
        }
    }
}
