using System;
using System.Collections.Generic;
using System.Data;
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
        public ActionResult Index()
        {
            FilterViewModel filterView = new FilterViewModel(_service.GetSaleInfo());
            return View(filterView);
        }

        public PartialViewResult UpdateSaleInfoTable(string managerName, string dateOfSale, string productName)
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

            return PartialView(filterView);
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

        public JsonResult AjaxMethod(string input)
        {
            string output = input + "!!!";
            return Json(output);
        }
    }
}