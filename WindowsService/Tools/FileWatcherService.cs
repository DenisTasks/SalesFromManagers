using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService.Tools
{
    public class FileWatcherService : IDisposable
    {
        private readonly FileSystemWatcher _watcher;
        private readonly ConverterToRecords _converter;
        private Task _watcherTask;
        public FileWatcherService(string path)
        {
            _watcher = new FileSystemWatcher(path, "*.csv");
            _watcher.Created += new FileSystemEventHandler(OnChanged);
            _watcher.Changed += new FileSystemEventHandler(OnChanged);
            _watcher.NotifyFilter = NotifyFilters.FileName;
            _watcher.EnableRaisingEvents = true;
            _converter = new ConverterToRecords();
        }

        public void OnChanged(object source, FileSystemEventArgs e)
        {
            _watcherTask = new Task(() => _converter.CreateRecords(e.FullPath));
            _watcherTask.Start();
        }

        public void Dispose()
        {
            _watcher.Dispose();
        }
    }
}
