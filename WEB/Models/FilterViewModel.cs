using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.DTO;

namespace IdentityApp.Models
{
    public class FilterViewModel
    {
        private List<string> _managers;
        private List<string> _products;
        private List<string> _datesOfSale;
        public FilterViewModel(IEnumerable<SaleInfoDTO> saleInfo)
        {
            SaleInfo = saleInfo;
            Initialize(saleInfo);
            Managers = new SelectList(_managers);
            Products = new SelectList(_products);
            DatesOfSale = new SelectList(_datesOfSale);
        }

        private void Initialize(IEnumerable<SaleInfoDTO> saleInfo)
        {
            _managers = new List<string>();
            _products = new List<string>();
            _datesOfSale = new List<string>();
            foreach (var item in saleInfo)
            {
                if (!_managers.Contains(item.ManagerName))
                {
                    _managers.Add(item.ManagerName);
                }
                if (!_products.Contains(item.ProductName))
                {
                    _products.Add(item.ProductName);
                }
                if (!_datesOfSale.Contains(item.DateOfSale))
                {
                    _datesOfSale.Add(item.DateOfSale);
                }
            }
            _managers.Insert(0, "All");
            _products.Insert(0, "All");
            _datesOfSale.Sort();
            _datesOfSale.Insert(0, "All");
            _managers.Sort();
            _products.Sort();
        }
        public IEnumerable<SaleInfoDTO> SaleInfo { get; set; }
        public SelectList Managers { get; set; }
        public SelectList DatesOfSale { get; set; }
        public SelectList Products { get; set; }
    }
}