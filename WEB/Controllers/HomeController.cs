using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.AspNet.SignalR.Infrastructure;
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

        private void SendMessageAboutFilter(string message)
        {
            var context = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.displayMessage(message);
        }

        public ActionResult BuildChart(string managerName, int managerId)
        {
            List<string> xValuesList = new List<string>();
            List<int> yValuesList = new List<int>();
            IEnumerable<SaleInfoDTO> saleInfo = _service.GetSaleInfo().Where(m => m.ManagerId == managerId);
            foreach (var item in saleInfo)
            {
                xValuesList.Add(item.DateOfSale);
                yValuesList.Add(item.ProductPrice);
            }

            var chart = new Chart(width: 500, height: 250, theme: ChartTheme.Green)
                .AddTitle($"Chart of {managerName} productive")
                .AddSeries(
                    name: "Manager result",
                    chartType: "Line",
                    xValue: xValuesList,
                    yValues: yValuesList)
                .AddLegend()
                .Write();
            return null;
        }

        public JsonResult AjaxMethod(string input)
        {
            string output = input + "!!!";
            return Json(output);
        }
    }
}