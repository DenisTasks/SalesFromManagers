using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Services;
using WEB.Models;

namespace WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBLLService _service;

        public HomeController(IBLLService service)
        {
            _service = service;
        }
        // GET: Home
        public ActionResult Index(string managerName, string dateOfSale, string productName)
        {
            IQueryable<SaleInfoDTO> saleInfo = _service.GetSaleInfo().AsQueryable();
            if (!String.IsNullOrEmpty(managerName) && !managerName.Equals("All"))
            {
                saleInfo = saleInfo.Where(m => m.ManagerName == managerName);
            }

            if (!String.IsNullOrEmpty(dateOfSale) && !dateOfSale.Equals("All"))
            {
                saleInfo = saleInfo.Where(d => d.DateOfSale == dateOfSale);
            }

            if (!String.IsNullOrEmpty(productName) && !productName.Equals("All"))
            {
                saleInfo = saleInfo.Where(p => p.ProductName == productName);
            }
            FilterViewModel filterView = new FilterViewModel(saleInfo);
            return View(filterView);
        }
    }
}