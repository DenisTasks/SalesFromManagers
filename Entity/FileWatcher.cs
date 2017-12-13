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
    public class FileWatcher : IWatcher<SaleInfoRecord>
    {
        private FileSystemWatcher _watcher;
        private readonly IConverter<SaleInfoRecord> _converter;
        private Task _watcherTask;
        public FileWatcher()
        {
            _converter = new ConverterToRecords();
        }

        public void Start()
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["WatchingFolder"];
            string extension = System.Configuration.ConfigurationManager.AppSettings["WatchingExtension"];
            _watcher = new FileSystemWatcher(path, extension);
            _watcher.Created += OnChanged;
            _watcher.Changed += OnChanged;
            _watcher.NotifyFilter = NotifyFilters.FileName;
            _watcher.EnableRaisingEvents = true;
        }

        public void OnChanged(object source, FileSystemEventArgs e)
        {
            _watcherTask = new Task(() => WriteToDatabase(source, e));
            _watcherTask.Start();
         //   _watcherTask.ContinueWith(t1 => t1.Exception);
            _watcherTask.ContinueWith(t1 => Console.WriteLine("i'm done " + t1.Id + " " + t1.IsCompleted));
        }

        public void WriteToDatabase(object source, FileSystemEventArgs e)
        {
            try
            {
                Console.WriteLine(DateTime.Now.Millisecond);
                _converter.CreateRecords(e.FullPath);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                // copy this file to debug folder
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
        public void Dispose()
        {
            _watcher.Created -= OnChanged;
            _watcher.Changed -= OnChanged;
            _watcher.Dispose();
        }
    }
}
