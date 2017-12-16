using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BLL.DTO
{
    public class SaleInfoDTO
    {
        [HiddenInput(DisplayValue = false)]
        public int SaleInfoId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int ProductId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Please, write this product name!")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Please, write this product price!")]
        [Range(1, 1000000, ErrorMessage = "Please, correct the price!")]
        public int ProductPrice { get; set; }
        [Required(ErrorMessage = "Please, write this client name!")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Please, write name more less then 20 chars!")]
        public string ClientName { get; set; }
        [Required(ErrorMessage = "Please, write this manager name!")]
        public string ManagerName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ManagerId { get; set; }

        [Required(ErrorMessage = "Please, write this date of sale!")]
        public string DateOfSale { get; set; }
    }
}
