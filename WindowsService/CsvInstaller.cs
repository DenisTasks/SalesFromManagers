using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace WindowsService
{
    [RunInstaller(true)]
    public partial class CsvInstaller : System.Configuration.Install.Installer
    {
        private ServiceInstaller _serviceInstaller;
        private ServiceProcessInstaller _processInstaller;
        public CsvInstaller()
        {
            InitializeComponent();

            _serviceInstaller = new ServiceInstaller();
            _processInstaller = new ServiceProcessInstaller();

            _processInstaller.Account = ServiceAccount.LocalSystem;
            _serviceInstaller.StartType = ServiceStartMode.Manual;
            _serviceInstaller.ServiceName = "CsvService";

            Installers.Add(_processInstaller);
            Installers.Add(_serviceInstaller);
        }
    }
}
