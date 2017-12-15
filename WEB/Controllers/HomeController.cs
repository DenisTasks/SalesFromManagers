using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Services;

namespace WEB.Controllers
{
    public class HomeController : Controller
    {
        private IBLLService _service;

        public HomeController(IBLLService service)
        {
            _service = service;
        }
        // GET: Home
        public ActionResult Index()
        {
            IEnumerable<SaleInfoDTO> saleInfoDTO = _service.GetSaleInfo();
            return View(saleInfoDTO);
        }
    }
}