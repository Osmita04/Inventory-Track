using DatabaseLayer;
using Inventory_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory_App.Controllers
{
    public class AdminConfigController : Controller
    {
        
        private InventoryERPDbEntities DB = new InventoryERPDbEntities();

        //Branch Types Configuration
        //=============================================================================================================================================
        public ActionResult AllBranchTypes()
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            if (usertypeid != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }

            var list = new List<BranchTypeMV>();
            var branchtypes = DB.tblBranchTypes.ToList();
            foreach (var branchtype in branchtypes)
            {
                list.Add(new BranchTypeMV() { BranchTypeID = branchtype.BranchTypeID, BranchType = branchtype.BranchType });
            }

            return View(list);
            
        }
        public ActionResult CreateBranchType()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            if (usertypeid != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }
            var branchtypemv = new BranchTypeMV();
            return View(branchtypemv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBranchType(BranchTypeMV branchtypemv)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            if (usertypeid != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }
            if (ModelState.IsValid)
            {
                var checkbranchtype = DB.tblBranchTypes.Where(u => u.BranchType == branchtypemv.BranchType.Trim()).FirstOrDefault();
                if (checkbranchtype == null)
                {
                    var newbranchtype = new tblBranchType();
                    newbranchtype.BranchType = branchtypemv.BranchType;
                    DB.tblBranchTypes.Add(newbranchtype);
                    DB.SaveChanges();
                    return RedirectToAction("AllBranchTypes");

                }
                else
                {
                    ModelState.AddModelError("BranchType", "Already Exist!");
                }
            }
            return View(branchtypemv);
        }
        public ActionResult EditBranchType(int? branchtypeid)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var UserTypeID = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out UserTypeID);
            if (UserTypeID != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }
            var editbranchtype = DB.tblBranchTypes.Find(branchtypeid);
            var branchtypemv = new BranchTypeMV();
            branchtypemv.BranchTypeID = editbranchtype.BranchTypeID;
            branchtypemv.BranchType = editbranchtype.BranchType;
            return View(branchtypemv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBranchType(BranchTypeMV branchtypemv)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            if (usertypeid != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }
            if (ModelState.IsValid)
            {
                var checkbranchtype = DB.tblBranchTypes.Where(bt => bt.BranchType == branchtypemv.BranchType.Trim() && bt.BranchTypeID != branchtypemv.BranchTypeID).FirstOrDefault();
                if (checkbranchtype == null)
                {
                    var editbranchtype = new tblBranchType();
                    editbranchtype.BranchType = branchtypemv.BranchType;
                    editbranchtype.BranchTypeID = branchtypemv.BranchTypeID;
                    DB.Entry(editbranchtype).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("AllBranchTypes");

                }
                else
                {
                    ModelState.AddModelError("BranchType", "Already Exist!");
                }
            }
            return View(branchtypemv);
        }
        //Account Activity Configuration
        //=============================================================================================================================================
        public ActionResult AllAccountActivity()
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            if (usertypeid != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }

            var list = new List<AccountActivityMV>();
            var accountactivities = DB.tblAccountActivities.ToList();
            foreach (var accountactivity in accountactivities)
            {
                list.Add(new AccountActivityMV() { AccountActivityID = accountactivity.AccountActivityID, Name = accountactivity.Name});
            }

            return View(list);

        }


        public ActionResult CreateAccountActivity()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            if (usertypeid != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }
            var accountactivitymv = new AccountActivityMV();
            return View(accountactivitymv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccountActivity(AccountActivityMV accountactivitymv)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            if (usertypeid != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }
            if (ModelState.IsValid)
            {
                var checkaccountactivity = DB.tblAccountActivities.Where(u => u.Name == accountactivitymv.Name.Trim()).FirstOrDefault();
                if (checkaccountactivity == null)
                {
                    var newaccountactivity = new tblAccountActivity();
                    newaccountactivity.Name = accountactivitymv.Name;
                    DB.tblAccountActivities.Add(newaccountactivity);
                    DB.SaveChanges();
                    return RedirectToAction("AllAccountActivity");

                }
                else
                {
                    ModelState.AddModelError("Name", "Already Exist!");
                }
            }
            return View(accountactivitymv);
        }
        public ActionResult EditAccountActivity(int? accountactivityid)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var UserTypeID = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out UserTypeID);
            if (UserTypeID != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }
            var editaccountactivity = DB.tblAccountActivities.Find(accountactivityid);
            var accountactivitymv = new AccountActivityMV();
            accountactivitymv.AccountActivityID = editaccountactivity.AccountActivityID;
            accountactivitymv.Name = editaccountactivity.Name;
            return View(accountactivitymv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccountActivity(AccountActivityMV accountactivitymv)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            if (usertypeid != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }
            if (ModelState.IsValid)
            {
                var checkaccountactivity = DB.tblAccountActivities.Where(at => at.Name == accountactivitymv.Name.Trim() && at.AccountActivityID != accountactivitymv.AccountActivityID).FirstOrDefault();
                if (checkaccountactivity == null)
                {
                    var editaccountactivity = new tblAccountActivity();
                    editaccountactivity.AccountActivityID = accountactivitymv.AccountActivityID;
                    editaccountactivity.Name = accountactivitymv.Name;
                    DB.Entry(editaccountactivity).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("AllAccountActivity");

                }
                else
                {
                    ModelState.AddModelError("Name", "Already Exist!");
                }
            }
            return View(accountactivitymv);
        }
    }
}