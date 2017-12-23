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
        public int SaleInfoId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int ProductId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int ClientId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int ManagerId { get; set; }


        [Required(ErrorMessage = "Please, write this product name!")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Please, write product name from 2 to 25 chars!")]
        [RegularExpression("^[a-zA-Z]+$")]
        public string ProductName { get; set; }


        [Required(ErrorMessage = "Please, write this product price!")]
        [Range(1, 1000000, ErrorMessage = "Please, check the price!")]
        public int ProductPrice { get; set; }


        [Required(ErrorMessage = "Please, write the client name!")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Please, write name from 2 to 20 chars!")]
        [RegularExpression("^[a-zA-Z]+$")]
        public string ClientName { get; set; }


        public string ManagerName { get; set; }


        //[StringLength(8, MinimumLength = 8, ErrorMessage = "Please, write date as ddMMyyyy!")]
        [Remote("ValidateDate", "Admin")]
        public string DateOfSale { get; set; }
    }
}
