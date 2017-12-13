using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Interfaces;
using DAL.Repositories;
using Model;

namespace WebSystemOfSales.Controllers
{
    public class HomeController : Controller
    {
        IUnitOfWork test = new UnitOfWork();
        // GET: Home
        public ViewResult Index()
        {
            var test2 = test.SaleInfoRepository.Read();
            return View(test2);
        }
    }
}