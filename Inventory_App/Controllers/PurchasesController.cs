using DatabaseLayer;
using Inventory_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory_App.Controllers
{
   
    public class PurchasesController : Controller
    {
        private InventoryERPDbEntities DB = new InventoryERPDbEntities();
        // GET: Purchases
        public ActionResult PurchaseStockProducts()
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
            int.TryParse(Convert.ToString(Session["BranchTypeID"]), out branchtypeid);

            var stock = DB.tblStocks.Where(p => p.CompanyID == companyid && p.BranchID == branchid).ToList();
            var list = new List<PurchaseItemsMV>();

            foreach (var product in stock)
            {
                var item = new PurchaseItemsMV();
                item.BranchID = product.BranchID;
                item.CategoryID = product.CategoryID;
                item.CompanyID = product.CompanyID;
                item.CreateBy = product.tblUser.UserName;
                item.PreviousPurchaseUnitPrice = product.CurrentPurchaseUnitPrice;
                item.CurrentPurchaseUnitPrice = product.CurrentPurchaseUnitPrice;
                item.Description = product.Description;
                item.ExpiryDate = product.ExpiryDate;
                item.Manufacture = product.Manufacture;
                item.IsActive = product.IsActive;
                item.ProductID = product.ProductID;
                item.ProductName = product.ProductName;
                item.Quantity = product.Quantity;
                item.SaleUnitPrice = product.SaleUnitPrice;
                item.StockTreshHoldQuantity = product.StockTreshHoldQuantity;
                item.UserID = product.UserID;

                item.CategoryName = product.tblCategory.categoryName;

                list.Add(item);

            }

            return View(list);

        }
    }
}