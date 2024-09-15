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

            var stock = DB.tblPurchaseCartDetails.Where(p => p.CompanyID == companyid && p.BranchID == branchid && p.UserID == userid).ToList();
            var purchaseitems = new PurchaseCartMV();
            var list = new List<PurchaseItemsMV>();
           
            double subTotal = 0;


            foreach (var product in stock)
            {
                var item = new PurchaseItemsMV();              
                item.CreateBy = product.tblUser.UserName;
                item.PreviousPurchaseUnitPrice = product.PreviousPurchaseUnitPrice;
                item.CurrentPurchaseUnitPrice = product.purchaseUnitPrice;
                item.Description = product.Description;
                item.ExpiryDate = product.ExpiryDate;
                item.Manufacture = product.Manufacture;
                //item.IsActive = product.IsActive;
                item.ProductID = product.ProductID;
                var getproduct = DB.tblStocks.Find(product.ProductID);
                item.ProductName = getproduct != null ? getproduct.ProductName : "?";
                item.Quantity = product.PurchaseQuantity;
                item.SaleUnitPrice = (double)product.SaleUnitPrice;
                item.PurchaseCartDetailID = product.PurchaseCartDetailID;
                item.UserID = product.UserID;

                item.CategoryName = product.tblStock.tblCategory.categoryName;
               
                list.Add(item);
                subTotal = subTotal + ((double)product.purchaseUnitPrice * product.PurchaseQuantity);
            }







            purchaseitems.PurchaseItemsList = list;
            purchaseitems.OrderSummary = new PurchaseCartSummaryMV() { SubTotal = subTotal};


            ViewBag.ProductID = new SelectList(DB.tblStocks.Where(s=>s.BranchID == branchid && s.CompanyID == companyid).ToList(), "ProductID", "ProductName", "0");

            ViewBag.SupplierID = new SelectList(DB.tblSuppliers.Where(s => s.BranchID == branchid && s.CompanyID == companyid).ToList(), "SupplierID", "SupplierName", "0");
            return View(purchaseitems);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PurchaseStockProducts(PurchaseCartMV purchaseCartMV)
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

            if(ModelState.IsValid)
            {

                var checkproductincart = DB.tblPurchaseCartDetails.Where(i=>i.ProductID == purchaseCartMV.ProductID && i.CompanyID == companyid && i.BranchID == branchid && i.UserID == userid).FirstOrDefault();
                if (checkproductincart == null)
                {
                    var item = new tblPurchaseCartDetail();
                    
                    item.BranchID = branchid;
                    item.CompanyID = companyid;
                    item.ProductID = purchaseCartMV.ProductID;
                    item.PurchaseQuantity = purchaseCartMV.PurchaseQuantity;
                    item.purchaseUnitPrice = purchaseCartMV.CurrentPurchaseUnitPrice;
                    item.SaleUnitPrice = purchaseCartMV.SaleUnitPrice;
                    item.PreviousPurchaseUnitPrice = purchaseCartMV.PreviousPurchaseUnitPrice;
                    item.Manufacture = purchaseCartMV.Manufacture;
                    item.ExpiryDate = purchaseCartMV.ExpiryDate;
                    item.Description = purchaseCartMV.Description;
                    item.UserID = userid;
                    DB.tblPurchaseCartDetails.Add(item);
                    DB.SaveChanges();
                    //  return RedirectToAction("PurchaseStockProducts");


                    purchaseCartMV.ProductID = 0;
                    purchaseCartMV.PurchaseQuantity = 0;
                    purchaseCartMV.CurrentPurchaseUnitPrice = 0;
                    purchaseCartMV.SaleUnitPrice = 0;
                    purchaseCartMV.PreviousPurchaseUnitPrice = 0;
                    purchaseCartMV.Manufacture = DateTime.Now;
                    purchaseCartMV.ExpiryDate = DateTime.Now;
                    purchaseCartMV.Description = string.Empty;
                }
                else
                {

                    ModelState.AddModelError("ProductID", "Already in Purchase Cart!");

                }
            }


            var stock = DB.tblPurchaseCartDetails.Where(p => p.CompanyID == companyid && p.BranchID == branchid && p.UserID == userid).ToList();
            var purchaseitems = new PurchaseCartMV();
            var list = new List<PurchaseItemsMV>();


            foreach (var product in stock)
            {
                var item = new PurchaseItemsMV();
                
                item.CreateBy = product.tblUser.UserName;
                item.PreviousPurchaseUnitPrice = product.PreviousPurchaseUnitPrice;
                item.CurrentPurchaseUnitPrice = product.purchaseUnitPrice;
                item.Description = product.Description;
                item.ExpiryDate = product.ExpiryDate;
                item.Manufacture = product.Manufacture;
                //item.IsActive = product.IsActive;
                item.ProductID = product.ProductID;
                var getproduct = DB.tblStocks.Find(product.ProductID);
                item.ProductName = getproduct != null ? getproduct.ProductName : "?";
                item.Quantity = product.PurchaseQuantity;
                item.SaleUnitPrice = (double)product.SaleUnitPrice;
                item.PurchaseCartDetailID = product.PurchaseCartDetailID;
                item.UserID = product.UserID;

                item.CategoryName = product.tblStock.tblCategory.categoryName;

                list.Add(item);

            }
            purchaseCartMV.PurchaseItemsList = list;
            ViewBag.ProductID = new SelectList(DB.tblStocks.Where(s => s.BranchID == branchid && s.CompanyID == companyid).ToList(), "ProductID", "ProductName", purchaseCartMV.ProductID);
            ViewBag.SupplierID = new SelectList(DB.tblSuppliers.Where(s => s.BranchID == branchid && s.CompanyID == companyid).ToList(), "SupplierID", "SupplierName", purchaseCartMV.SupplierID);
            return View(purchaseCartMV);

        }

        public ActionResult DeletePurchaseCartItem(int? id)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }


            var pitem = DB.tblPurchaseCartDetails.Find(id);
            DB.Entry(pitem).State = System.Data.Entity.EntityState.Deleted;
            DB.SaveChanges();
            return RedirectToAction("PurchaseStockProducts");

        }

        public ActionResult CheckoutPurchase(
            int ? supplierid,
            bool ispaymentispaid,
            float? estimatedtax,
            float? shippingfee, 
            float? subtotal)
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


                using (var transaction = DB.Database.BeginTransaction())
                {

                    try
                     {

                    float totalamount = (float)subtotal + (float)estimatedtax + (float)shippingfee;
                    string invoiceno = "PUR" + DateTime.Now.ToString("yyyymmddhhmmss") + userid;
                    
                    var invoiceheader = new tblSupplierInvoice();


                    invoiceheader.SupplierID = (int )supplierid;
                    invoiceheader.CompanyID = companyid;
                    invoiceheader.BranchID = branchid;
                    invoiceheader.InvoiceNo = invoiceno;
                    invoiceheader.TotalAmount = totalamount;
                    invoiceheader.InvoiceDate = DateTime.Now;
                    invoiceheader.Description = " ";   
                    invoiceheader.UserID = userid;
                    invoiceheader.subtotalamount = subtotal;
                    invoiceheader.estimatedtax = (float)estimatedtax;
                    invoiceheader.shippingfee = (float)shippingfee;

                    DB.tblSupplierInvoices.Add(invoiceheader);
                    DB.SaveChanges();


                    var purchasestock = DB.tblPurchaseCartDetails.Where(p => p.CompanyID == companyid && p.BranchID == branchid && p.UserID == userid).ToList();
                   

                    foreach (var product in purchasestock)
                    {
                        var purchaseitem = new tblSupplierInvoiceDetail();



                        purchaseitem.SupplierInvoiceID = invoiceheader.SupplierInvoiceID;
                        purchaseitem.ProductID = product.ProductID;
                        purchaseitem.PurchaseQuantity = product.PurchaseQuantity;   
                        purchaseitem.purchaseUnitPrice = product.purchaseUnitPrice;
                        purchaseitem.previouspurchaseunitprice = product.PreviousPurchaseUnitPrice;
                        purchaseitem.manufacturedate = (DateTime)product.Manufacture;
                        purchaseitem.expirydate = (DateTime)product.ExpiryDate;
                        DB.tblSupplierInvoiceDetails.Add(purchaseitem); 
                        DB.SaveChanges();

                        var stockproduct = DB.tblStocks.Find(product.ProductID);
                        stockproduct.Manufacture = (DateTime)product.Manufacture;
                        stockproduct.ExpiryDate = (DateTime)product.ExpiryDate; 
                        stockproduct.Quantity = stockproduct.Quantity + product.PurchaseQuantity;
                        stockproduct.CurrentPurchaseUnitPrice = product.purchaseUnitPrice;
                        stockproduct.SaleUnitPrice = (double)product.SaleUnitPrice;
                        DB.Entry(stockproduct).State = System.Data.Entity.EntityState.Modified;
                        DB.SaveChanges();

                    }

                    if(ispaymentispaid == true)
                    {
                        invoiceno = "PPP" + DateTime.Now.ToString("yyyymmddhhmmss") + userid;
                        var invoicepayment = new tblSupplierPayment();
                        invoicepayment.SupplierID = (int)supplierid;
                        invoicepayment.SupplierInvoiceID = invoiceheader.SupplierInvoiceID;
                        invoicepayment.CompanyID = companyid;
                        invoicepayment.BranchID = branchid;
                        invoicepayment.InvoiceNo = invoiceno; 
                        invoicepayment.TotalAmount = totalamount;
                        invoicepayment.PaymentAmount = totalamount;
                        invoicepayment.RemainingBalance = 0;
                        invoicepayment.UserID = userid;
                        invoicepayment.InvoiceDate =  DateTime.Now;
                        DB.tblSupplierPayments.Add(invoicepayment);
                        DB.SaveChanges();   
    }

                    transaction.Commit();
                     }
                    catch 
                     { 
                
                        transaction.Rollback();
                
                     }
                   
                }
           

            return View();
        }
    }
}