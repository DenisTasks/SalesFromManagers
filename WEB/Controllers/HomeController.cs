using System;
using System.Collections.Generic;
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
            int pageSize = 10;
            try
            {
                // отображение первой страницы
                _filterView = new FilterViewModel(_service.GetSaleInfo(((pageNumber - 1) * pageSize), pageSize));

                // отображение пагинатора на всю базу данных
                int count = _service.GetSaleInfo().Count();
                List<int> pagingList = new List<int>();
                for (int i = 1; i < count; i++)
                {
                    pagingList.Add(i);
                }
                _filterView.Result = pagingList.ToPagedList(pageNumber, pageSize);

                // заполнение фильтра уникальными менеджерами/датами/продуктами
                SelectLists distinctItems = _service.DistinctItems();
                _filterView.Managers = new SelectList(distinctItems.Managers);
                _filterView.DatesOfSale = new SelectList(distinctItems.DateOfSales);
                _filterView.Products = new SelectList(distinctItems.Products);

            }
            catch (Exception)
            {
                return View("Error");
            }
            return View(_filterView);
        }
        public PartialViewResult UpdateSaleInfoTable(string managerName, string dateOfSale, string productName, int? page)
        {
            int pageSize = 10;
            ViewBag.ManagerName = managerName;
            ViewBag.DateOfSale = dateOfSale;
            ViewBag.ProductName = productName;
            try
            {
                IQueryable<SaleInfoDTO> saleInfo;
                if (page > 0)
                {
                    int pageNumber = page ?? 1;

                    saleInfo = _service.GetSaleInfo(((pageNumber - 1) * pageSize), pageSize).AsQueryable();
                    _filterView = new FilterViewModel(saleInfo);

                    int count = _service.GetSaleInfo().Count();
                    List<int> pagingList = new List<int>();
                    for (int i = 1; i < count; i++)
                    {
                        pagingList.Add(i);
                    }
                    _filterView.Result = pagingList.ToPagedList(pageNumber, pageSize);
                }
                else
                {
                    saleInfo = _service.GetSaleInfo().AsQueryable();
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

                    int count = 0;
                    foreach (var item in _filterView.SaleInfo)
                    {
                        count++;
                    }
                    List<int> pagingList = new List<int>();
                    for (int i = 1; i < count; i++)
                    {
                        pagingList.Add(i);
                    }
                    _filterView.Result = pagingList.ToPagedList(1, 5);

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
            }
            catch (Exception)
            {
                return PartialView("Error");
            }

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