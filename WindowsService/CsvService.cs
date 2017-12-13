using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace WindowsService
{
    public partial class CsvService : ServiceBase
    {
        private FileWatcher _fileWatcher;
        public CsvService()
        {
            InitializeComponent();
            _fileWatcher = new FileWatcher();
        }

        protected override void OnStart(string[] args)
        {
            _fileWatcher.Start();
        }

        protected override void OnStop()
        {
            _fileWatcher.Dispose();
        }
    }
}
