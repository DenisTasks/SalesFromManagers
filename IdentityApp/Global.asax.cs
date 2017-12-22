using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BLL.Services;
using IdentityApp.App_Start;
using IdentityApp.Models;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;

namespace IdentityApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // чуть что закомментить (создаем базу с уже созданными юзерами)
        //    Database.SetInitializer<ApplicationDbContext>(new AppDbInitializer());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            NinjectModule ninjectLoad = new NinjectLoad();
            NinjectModule serviceModule = new ServiceModule("ModelOfSalesContainer");
            var kernel = new StandardKernel(ninjectLoad, serviceModule);
            kernel.Unbind<ModelValidatorProvider>();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
