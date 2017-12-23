using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.DTO;
using BLL.Interfaces;
using IdentityApp.App_Start;
using IdentityApp.Models;
using IdentityApp.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace IdentityApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBLLService _service;
        private FilterViewModel _filterView;

        public HomeController(IBLLService service)
        {
            _service = service;
        }
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Statistics()
        {
            _filterView = new FilterViewModel(_service.GetSaleInfo());
            return View(_filterView);
        }

        public PartialViewResult UpdateSaleInfoTable(string managerName, string dateOfSale, string productName)
        {
            IQueryable<SaleInfoDTO> saleInfo = _service.GetSaleInfo().AsQueryable();
            #region Filtering
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
            _filterView = new FilterViewModel(saleInfo);
            #endregion
            string message = $"You see filter sale info by manager {managerName}, date {dateOfSale}, product {productName}";
            if (managerName == "All" && dateOfSale == "All" && productName == "All")
            {
                message = string.Empty;
            }
            SendMessageAboutFilter(message);
            return PartialView(_filterView);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }


        private void SendMessageAboutFilter(string message)
        {
            var context = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.displayMessage(message);
        }
    }
}