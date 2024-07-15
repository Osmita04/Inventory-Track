using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace Inventory_App.Models
{
    public class FocalPersonMV
    {
        public EmployeeMV employeeMV { get; set; }
        public UserMV userMV { get; set; }
        public int BranchID {  get; set; }
        public int CompanyID {  get; set; }



    }
}