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
            return RedirectToAction("Statistics", "Home");
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
            //var result = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
            DateTime parsedDate;
            if (!DateTime.TryParse(DateOfSale, out parsedDate))
            {
                return Json($"Please, enter a valid date >> dd/MM/yyyy <<!",
                    JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
    }
}