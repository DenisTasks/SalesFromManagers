using System;
using System.Linq;
using System.Web.Mvc;
using BLL.DTO;
using BLL.Interfaces;
using IdentityApp.Models;
using IdentityApp.Utils;
using PagedList;

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
        public ActionResult Statistics(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 2;
            try
            {
                _filterView = new FilterViewModel(_service.GetSaleInfo());
            }
            catch (Exception)
            {
                return View("Error");
            }
            _filterView.Result = _filterView.SaleInfo.ToPagedList(pageNumber, pageSize);
            return Request.IsAjaxRequest()
                ? (ActionResult) PartialView("UpdateSaleInfo", _filterView)
                : View(_filterView);
        }


        public PartialViewResult UpdateSaleInfoTable(string managerName, string dateOfSale, string productName, int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 2;
            ViewBag.ManagerName = managerName;
            ViewBag.DateOfSale = dateOfSale;
            ViewBag.ProductName = productName;
            try
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
                if (String.IsNullOrEmpty(managerName) && String.IsNullOrEmpty(dateOfSale) && String.IsNullOrEmpty(productName))
                {
                    message = string.Empty;
                }
                SendMessageAboutFilter(message);
            }
            catch (Exception)
            {
                return PartialView("Error");
            }
            _filterView.Result = _filterView.SaleInfo.ToPagedList(pageNumber, pageSize);
            return PartialView(_filterView);
        }



        private void SendMessageAboutFilter(string message)
        {
            var context = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.displayMessage(message);
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            base.Dispose(disposing);
        }
    }
}