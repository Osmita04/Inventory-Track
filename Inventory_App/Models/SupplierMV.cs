using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Inventory_App.Models
{

    public partial class SupplierMV
    {
    
        public int SupplierID { get; set; }
        [Display(Name = "Supplier")]
        [Required(ErrorMessage = "Required*")]
        public string SupplierName { get; set; }
        [Display(Name = "Conatct No")]
        [Required(ErrorMessage = "Required*")]

        public string SupplierConatctNo { get; set; }
        [Display(Name = "Address")]
        [Required(ErrorMessage = "Required*")]

        public string SupplierAddress { get; set; }
        [Display(Name = "Email")]
        public string SupplierEmail { get; set; }
        public string Discription { get; set; }
        public int BranchID { get; set; }
        public int CompanyID { get; set; }
        public int UserID { get; set; }
        public string CreatedBy { get; set; }
    
    }
}
