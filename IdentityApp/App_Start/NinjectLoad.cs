using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Interfaces;
using BLL.Services;
using Ninject.Modules;

namespace IdentityApp.App_Start
{
    public class NinjectLoad : NinjectModule
    {
        public override void Load()
        {
            Bind<IBLLService>().To<BLLService>();
        }
    }
}