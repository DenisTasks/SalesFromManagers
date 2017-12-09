using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Interfaces;
using Entity.MappingClass;

namespace Entity
{
    public class FileWatcher : IWatcher<SaleInfoRecord>, IDisposable
    {
        public FileSystemWatcher Watcher { get; }
        public IConverter<SaleInfoRecord> Converter { get; }
        public Task WatcherTask { get; private set; }
        public FileWatcher()
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["WatchingFolder"];
            string extension = System.Configuration.ConfigurationManager.AppSettings["WatchingExtension"];
            Watcher = new FileSystemWatcher(path, extension);
            Watcher.Created += OnChanged;
            Watcher.Changed += OnChanged;
            Watcher.NotifyFilter = NotifyFilters.FileName;
            Watcher.EnableRaisingEvents = true;
            Converter = new ConverterToRecords();
        }

        public void OnChanged(object source, FileSystemEventArgs e)
        {
            WatcherTask = new Task(() => WriteToDatabase(source, e));
            WatcherTask.Start();
        }

        public void WriteToDatabase(object source, FileSystemEventArgs e)
        {
            Converter.CreateRecords(e.FullPath);
        }
        public void Dispose()
        {
            Watcher.Dispose();
        }
    }
}
