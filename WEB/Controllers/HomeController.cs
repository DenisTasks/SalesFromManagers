using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.DTO;
using BLL.Services;

namespace WEB.Controllers
{
    public class HomeController : Controller
    {
        private BLLService _service = new BLLService();

        // GET: Home
        public ActionResult Index()
        {
            IEnumerable<ManagerDTO> managersDTO = _service.GetManagers();
            //mapper
            var managers = Mapper.Map
            return View(managers);
        }
    }
}