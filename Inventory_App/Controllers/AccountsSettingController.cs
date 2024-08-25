using DatabaseLayer;
using Inventory_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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


            var accountsettinglist = DB.tblAccountSettings.Where(a => a.CompanyID == companyid && a.BranchID == branchid).ToList();
            var list = new List<AccountSettingMV>();
            foreach(var item in accountsettinglist)
            {

                var accountsetting = new AccountSettingMV();
                accountsetting.AccountSubControlID = item.AccountSubControlID;
                accountsetting.AccountSubControl = item.tblAccountSubControl != null ? item.tblAccountSubControl.AccountSubControlName : string.Empty;
                accountsetting.AccountSettingID = item.AccountSettingID;
                accountsetting.AccountHeadID = item.AccountHeadID;
                accountsetting.AccountHead = item.tblAccountHead != null ? item.tblAccountHead.AccountHeadName : string.Empty;
                accountsetting.AccountControlID = item.AccountControlID;
                accountsetting.AccountControl = item.tblAccountControl != null ? item.tblAccountControl.AccountControlName : string.Empty ;
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
            var accountSettingMV = new AccountSettingMV();
            accountSettingMV.CompanyID = companyid;
            accountSettingMV.BranchID = branchid;

            ViewBag.AccountHeadID = new SelectList(DB.tblAccountHeads.ToString(),"AccountHeadID", "AccountHeadName", "0");
            ViewBag.AccountControlID = new SelectList(DB.tblAccountControls.ToString(), "AccountControlID", "AccountControlName", "0");
            ViewBag.AccountSubControlID = new SelectList(DB.tblAccountSubControls.ToString(), "AccountSubControlID", "AccountSubControlName", "0");
            ViewBag.AccountActivityID = new SelectList(DB.tblAccountActivities.ToString(), "AccountActivityID", "Name", "0");
            return View(accountSettingMV);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBranchAccountSetting(AccountSettingMV accountSettingMV)
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
       
            try
            {

                if (ModelState.IsValid)
                {
                    var checkaccountsetting = DB.tblAccountSettings.Where(e => e.AccountActivityID == accountSettingMV.AccountActivityID).FirstOrDefault();
                    if (checkaccountsetting == null)
                    {
                        var newsetting = new tblAccountSetting();
       
                        newsetting.AccountHeadID = accountSettingMV.AccountHeadID;
                        newsetting.AccountControlID = accountSettingMV.AccountControlID;
                        newsetting.AccountSubControlID = accountSettingMV.AccountSubControlID;
                        newsetting.AccountActivityID = accountSettingMV.AccountActivityID;
                        newsetting.CompanyID = companyid;
                        newsetting.BranchID = branchid;

                        DB.tblAccountSettings.Add(newsetting);
                        DB.SaveChanges();
                        return RedirectToAction("BranchAccountSetting");

                    }
                    else
                    {
                        ModelState.AddModelError("CNIC", "Already Exist!");
                    }
                }

                
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("Name", "Must be Filled All Field with correct data!");

            
            }
            ViewBag.AccountHeadID = new SelectList(DB.tblAccountHeads.ToString(), "AccountHeadID", "AccountHeadName", accountSettingMV.AccountHeadID);
            ViewBag.AccountControlID = new SelectList(DB.tblAccountControls.ToString(), "AccountControlID", "AccountControlName", accountSettingMV.AccountControlID);
            ViewBag.AccountSubControlID = new SelectList(DB.tblAccountSubControls.ToString(), "AccountSubControlID", "AccountSubControlName", accountSettingMV.AccountSubControlID);
            ViewBag.AccountActivityID = new SelectList(DB.tblAccountActivities.ToString(), "AccountActivityID", "Name", accountSettingMV.AccountActivityID);

            return View(accountSettingMV);
        }

    }
}