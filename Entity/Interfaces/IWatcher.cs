using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Interfaces
{
    public interface IWatcher<T> : IDisposable where T: class
    {
        void Start();
        void OnChanged(object source, FileSystemEventArgs e);
        void WriteToDatabase(object source, FileSystemEventArgs e);
    }
}
