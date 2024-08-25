using DatabaseLayer;
using Inventory_App.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Inventory_App.Controllers
{
    public class AccountsSettingController : Controller
    {
        private InventoryERPDbEntities DB = new InventoryERPDbEntities();

        // GET: AccountsSetting
        public ActionResult BranchAccountSetting()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int.TryParse(Convert.ToString(Session["UserID"]), out int userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out int usertypeid);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out int companyid);
            int.TryParse(Convert.ToString(Session["BranchID"]), out int branchid);
            int.TryParse(Convert.ToString(Session["BranchTypeID"]), out int branchtypeid);

            var accountsettinglist = DB.tblAccountSettings.Where(a => a.CompanyID == companyid && a.BranchID == branchid).ToList();
            var list = new List<AccountSettingMV>();
            foreach (var item in accountsettinglist)
            {
                var accountsetting = new AccountSettingMV();
                accountsetting.AccountSubControlID = item.AccountSubControlID;
                accountsetting.AccountSubControl = item.tblAccountSubControl != null ? item.tblAccountSubControl.AccountSubControlName : string.Empty;
                accountsetting.AccountSettingID = item.AccountSettingID;
                accountsetting.AccountHeadID = item.AccountHeadID;
                accountsetting.AccountHead = item.tblAccountHead != null ? item.tblAccountHead.AccountHeadName : string.Empty;
                accountsetting.AccountControlID = item.AccountControlID;
                accountsetting.AccountControl = item.tblAccountControl != null ? item.tblAccountControl.AccountControlName : string.Empty;
                accountsetting.AccountActivityID = item.AccountActivityID;
                accountsetting.AccountActivity = item.tblAccountActivity != null ? item.tblAccountActivity.Name : string.Empty;
                list.Add(accountsetting);
            }
            return View(list);
        }

        public ActionResult CreateBranchAccountSetting()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            int.TryParse(Convert.ToString(Session["UserID"]), out int userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out int usertypeid);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out int companyid);
            int.TryParse(Convert.ToString(Session["BranchID"]), out int branchid);
            int.TryParse(Convert.ToString(Session["BranchTypeID"]), out int branchtypeid);

            var accountsettingMV = new AccountSettingMV();
            accountsettingMV.CompanyID = companyid;
            accountsettingMV.BranchID = branchid;


            ViewBag.AccountHeadID = new SelectList(DB.tblAccountHeads.ToList(), "AccountHeadID", "AccountHeadName", "0");
            ViewBag.AccountControlID = new SelectList(DB.tblAccountControls.ToList(), "AccountControlID", "AccountControlName", "0");
            ViewBag.AccountSubControlID = new SelectList(DB.tblAccountSubControls.ToList(), "AccountSubControlID", "AccountSubControlName", "0");
            ViewBag.AccountActivityID = new SelectList(DB.tblAccountActivities.ToList(), "AccountActivityID", "Name", "0");
            return View(accountsettingMV);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBranchAccountSetting(AccountSettingMV accountsettingMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int.TryParse(Convert.ToString(Session["UserID"]), out int userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out int usertypeid);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out int companyid);
            int.TryParse(Convert.ToString(Session["BranchID"]), out int branchid);
            int.TryParse(Convert.ToString(Session["BranchTypeID"]), out int branchtypeid);
            try
            {

                if (ModelState.IsValid)
                {
                   
                    var checkaccountsetting = DB.tblAccountSettings.Where(e => e.CompanyID == companyid && e.BranchID == branchid && e.AccountSettingID != accountsettingMV.AccountSettingID && e.AccountActivityID == accountsettingMV.AccountActivityID).FirstOrDefault();
                    if (checkaccountsetting == null)
                    {
                        var newsetting = new tblAccountSetting();
                        newsetting.AccountHeadID = accountsettingMV.AccountHeadID;
                        newsetting.AccountControlID = accountsettingMV.AccountControlID;
                        newsetting.AccountSubControlID = accountsettingMV.AccountSubControlID;
                        newsetting.AccountActivityID = accountsettingMV.AccountActivityID;
                        newsetting.CompanyID = companyid;
                        newsetting.BranchID = branchid;
                        DB.tblAccountSettings.Add(newsetting);
                        DB.SaveChanges();
                        return RedirectToAction("BranchAccountSetting");
                    }
                    else
                    {
                        ModelState.AddModelError("AccountActivityID", "Already Exist!");
                    }
                }
                return View(accountsettingMV);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Name", "Must be Filled All Field with Correct data!");               
            }
            ViewBag.AccountHeadID = new SelectList(DB.tblAccountHeads.ToList(), "AccountHeadID", "AccountHeadName", accountsettingMV.AccountHeadID);
            ViewBag.AccountControlID = new SelectList(DB.tblAccountControls.ToList(), "AccountControlID", "AccountControlName", accountsettingMV.AccountControlID);
            ViewBag.AccountSubControlID = new SelectList(DB.tblAccountSubControls.ToList(), "AccountSubControlID", "AccountSubControlName", accountsettingMV.AccountSubControlID);
            ViewBag.AccountActivityID = new SelectList(DB.tblAccountActivities.ToList(), "AccountActivityID", "Name", accountsettingMV.AccountActivityID);
            return View(accountsettingMV);
        }
    
 
        
        
        
        
        
        public ActionResult EditBranchAccountSetting(int? id)
         {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            
            int.TryParse(Convert.ToString(Session["UserID"]), out int userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out int usertypeid);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out int companyid);
            int.TryParse(Convert.ToString(Session["BranchID"]), out int branchid);
            int.TryParse(Convert.ToString(Session["BranchTypeID"]), out int branchtypeid);
            var accountsetting = DB.tblAccountSettings.Find(id);
            var accountsettingMV = new AccountSettingMV();
            accountsettingMV.AccountSettingID = accountsetting.AccountSettingID;
            accountsettingMV.AccountHeadID = accountsetting.AccountHeadID;
            accountsettingMV.AccountControlID = accountsetting.AccountControlID;
            accountsettingMV.AccountSubControlID = accountsetting.AccountSubControlID;
            accountsettingMV.AccountActivityID = accountsetting.AccountActivityID;
            accountsettingMV.CompanyID = companyid;
            accountsettingMV.BranchID = branchid;
            ViewBag.AccountHeadID = new SelectList(DB.tblAccountHeads.ToList(), "AccountHeadID", "AccountHeadName",accountsetting.AccountHeadID);
            ViewBag.AccountControlID = new SelectList(DB.tblAccountControls.Where(a => a.AccountHeadID == accountsetting.AccountHeadID).ToList(), "AccountControlID", "AccountControlName",accountsetting.AccountControlID);
            ViewBag.AccountSubControlID = new SelectList(DB.tblAccountSubControls.Where(a => a.AccountControlID == accountsetting.AccountControlID).ToList(), "AccountSubControlID", "AccountSubControlName",accountsetting.AccountSubControlID);
            ViewBag.AccountActivityID = new SelectList(DB.tblAccountActivities.ToList(), "AccountActivityID", "Name",accountsetting.AccountActivityID);

            return View(accountsettingMV);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBranchAccountSetting(AccountSettingMV accountsettingMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int.TryParse(Convert.ToString(Session["UserID"]), out int userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out int usertypeid);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out int companyid);
            int.TryParse(Convert.ToString(Session["BranchID"]), out int branchid);
            int.TryParse(Convert.ToString(Session["BranchTypeID"]), out int branchtypeid);
            try
            {
                if (ModelState.IsValid)
                {
                    var checkaccountsetting = DB.tblAccountSettings.Where(e=>e.CompanyID == companyid && e.BranchID == branchid && e.AccountSettingID != accountsettingMV.AccountSettingID && e.AccountActivityID ==accountsettingMV.AccountActivityID).FirstOrDefault();
                    if (checkaccountsetting == null)
                    {
                        var editsetting = DB.tblAccountSettings.Find(accountsettingMV.AccountSettingID);

                        editsetting.AccountHeadID = accountsettingMV.AccountHeadID;
                        editsetting.AccountControlID = accountsettingMV.AccountControlID;
                        editsetting.AccountSubControlID = accountsettingMV.AccountSubControlID;
                        editsetting.AccountActivityID = accountsettingMV.AccountActivityID;
                        DB.Entry(editsetting).State = System.Data.Entity.EntityState.Modified;
                        DB.SaveChanges();
                        return RedirectToAction("BranchAccountSetting");
                    }
                    else
                    {
                        ModelState.AddModelError("AccountActivityID", "Already Exist!");
                    }
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Name", "Must be Filled All Field with Correct Data!");
            }
            ViewBag.AccountHeadID = new SelectList(DB.tblAccountHeads.ToList(), "AccountHeadID", "AccountHeadName", accountsettingMV.AccountHeadID);
            ViewBag.AccountControlID = new SelectList(DB.tblAccountControls.Where(a => a.AccountHeadID == accountsettingMV.AccountHeadID).ToList(), "AccountControlID", "AccountControlName", accountsettingMV.AccountControlID);
            ViewBag.AccountSubControlID = new SelectList(DB.tblAccountSubControls.Where(a => a.AccountControlID == accountsettingMV.AccountControlID).ToList(), "AccountSubControlID", "AccountSubControlName", accountsettingMV.AccountSubControlID);
            ViewBag.AccountActivityID = new SelectList(DB.tblAccountActivities.ToList(), "AccountActivityID", "Name", accountsettingMV.AccountActivityID);


            return View(accountsettingMV);
        }
       
    }
}
