using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.DTO;
using PagedList;

namespace IdentityApp.Models
{
    public class FilterViewModel
    {
        public SelectList Managers { get; set; }
        public SelectList DatesOfSale { get; set; }
        public SelectList Products { get; set; }
        public IPagedList<int> ListPager { get; set; }
        public IEnumerable<SaleInfoDTO> SaleInfo { get; set; }
        public FilterViewModel(IEnumerable<SaleInfoDTO> saleInfo)
        {
            SaleInfo = saleInfo;
        }
    }
}