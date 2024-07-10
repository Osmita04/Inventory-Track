using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Inventory_App.Models
{

    public partial class FinancialYearMV
    {
    
        public int FinancialYearID { get; set; }
        public int UserID { get; set; }
        public string FinancialYear { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    
       
    }
}
