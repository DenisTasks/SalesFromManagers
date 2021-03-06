﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Interfaces
{
    public interface ISender<T> where T: class
    {
        void Send(string name, T item);
    }
}
