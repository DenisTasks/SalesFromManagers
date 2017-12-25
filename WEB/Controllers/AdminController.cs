using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.DTO;

namespace IdentityApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IBLLService _service;
        public AdminController(IBLLService service)
        {
            _service = service;
        }

        // GET: Admin
        public ActionResult Details(int id)
        {
            SaleInfoDTO details;
            ChartInfo chartInfo;
            using (_service)
            {
                try
                {
                    details = _service.FindSaleInfoById(id);
                    chartInfo = _service.GetChartInfo(details);
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }

            ViewBag.Products = chartInfo.Products;
            ViewBag.Count = chartInfo.Count;
            ViewBag.Summ = chartInfo.Summ;

            return View(details);
        }

        public ActionResult Delete(int id)
        {
            SaleInfoDTO deleteItem;
            using (_service)
            {
                try
                {
                    deleteItem = _service.FindSaleInfoById(id);
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }
            return View(deleteItem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (_service)
            {
                try
                {
                    _service.DeleteSaleInfoById(id);
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }
            return RedirectToAction("Statistics", "Home");
        }

        public ActionResult Edit(int id)
        {
            SaleInfoDTO editItem;
            using (_service)
            {
                try
                {
                    editItem = _service.FindSaleInfoById(id);
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }
            return View(editItem);
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
                    return RedirectToAction("Statistics", "Home");
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
            DateTime parsedDate;
            if (!DateTime.TryParse(DateOfSale, out parsedDate))
            {
                return Json("Please, enter a valid date >> dd/MM/yyyy <<!",
                    JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}