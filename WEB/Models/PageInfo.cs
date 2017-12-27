﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.DTO;

namespace IdentityApp.Models
{
    public class PageInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
    }

    public class IndexViewModel
    {
        public IEnumerable<SaleInfoDTO> Sales { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}