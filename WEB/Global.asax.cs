using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BLL.Services;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using WEB.Utils;

namespace WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            NinjectModule ninjectLoad = new NinjectLoad();
            NinjectModule serviceModule = new ServiceModule("ModelOfSalesContainer");
            var kernel = new StandardKernel(ninjectLoad, serviceModule);
            kernel.Unbind<ModelValidatorProvider>();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
