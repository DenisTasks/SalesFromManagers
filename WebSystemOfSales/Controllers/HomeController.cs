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
        IUnitOfWork _unitOfWork = new UnitOfWork();
        // GET: Home
        public ViewResult Index()
        {
            var test2 = _unitOfWork.SaleInfoRepository.Read();
            return View(test2);
        }

        public ActionResult Products()
        {
            var products = _unitOfWork.ProductRepository.Read();
            return View(products);
        }
        public ActionResult Details(int id)
        {
            var test3 = _unitOfWork.SaleInfoRepository.FindById(id);
            return View(test3);
        }

        public ActionResult Delete(int id)
        {
            var test4 = _unitOfWork.SaleInfoRepository.FindById(id);
            return View(test4);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var test5 = _unitOfWork.SaleInfoRepository.FindById(id);
            _unitOfWork.SaleInfoRepository.Delete2(test5);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var test6 = _unitOfWork.SaleInfoRepository.FindById(id);
            return View(test6);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SaleInfo item)
        {
            _unitOfWork.SaleInfoRepository.Update2(item);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}