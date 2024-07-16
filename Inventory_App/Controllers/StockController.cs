using DatabaseLayer;
using Inventory_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory_App.Controllers
{

   
    public class StockController : Controller
    {
        private InventoryERPDbEntities DB = new InventoryERPDbEntities();
        // GET: Stock
        public ActionResult AllCategories()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            var companyid = 0;
            var branchid = 0;
            var branchtypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out companyid);
            int.TryParse(Convert.ToString(Session["BranchID"]), out branchid);
           

            var list = new List<CategoryMV>();
            var Categories = DB.tblCategories.Where(c=>c.CompanyID == companyid && c.BranchID == branchid).ToList();
            foreach (var category in Categories)
            {
                var username = category.tblUser.UserName;
                list.Add(new CategoryMV() { 
                    BranchID = category.BranchID,
                    CategoryID = category.CategoryID, 
                    categoryName = category.categoryName , 
                    CompanyID = category.CompanyID, 
                    UserID = category.UserID,
                    CreatedBy = username
                });
            }

            return View(list);

        }
    }
}