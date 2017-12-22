using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Services;
using WEB.Models;
using WEB.Hubs;
using System.Web.Helpers;

namespace WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBLLService _service;
        private FilterViewModel _filterView;
        public HomeController(IBLLService service)
        {
            _service = service;
        }
        // GET: Home
        public ActionResult Index()
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
        public ActionResult Details(int id)
        {
            var details = _service.FindSaleInfoById(id);

            List<string> products = new List<string>() { "iPhone", "iPad", "iMac", "iPod" };
            List<int> count = new List<int>() { 10, 20, 5, 1 };
            ViewBag.PRODUCTS = products;
            ViewBag.COUNT = count;

            return View(details);
        }

        public ActionResult Delete(int id)
        {
            var delete = _service.FindSaleInfoById(id);
            return View(delete);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _service.DeleteSaleInfoById(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var edit = _service.FindSaleInfoById(id);
            return View(edit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SaleInfoDTO item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _service.UpdateSaleInfo(item);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes!");
            }
            return View(item);
        }

        public JsonResult ValidateDate(string DateOfSale)
        {
            //var result = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
            DateTime parsedDate;
            if (!DateTime.TryParse(DateOfSale, out parsedDate))
            {
                return Json($"Please, enter a valid date >> dd/MM/yyyy <<! Now we have {DateOfSale}",
                    JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        private void SendMessageAboutFilter(string message)
        {
            var context = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.displayMessage(message);
        }

    }
}