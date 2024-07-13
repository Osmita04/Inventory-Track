using DatabaseLayer;
using Inventory_App.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory_App.Controllers
{
    public class BranchController : Controller
    {
        
        InventoryERPDbEntities DB = new InventoryERPDbEntities();

        public ActionResult AllCompanyBranches()
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

            var branches = DB.tblBranches.Where(b => b.BrchID == branchid && b.CompanyID == companyid).ToList();
            var list = new List<BranchMV>();
            foreach (var branch in branches)
            {
                var addbranch = new BranchMV();
                addbranch.BranchID = branch.BranchID;
                addbranch.BranchTypeID = branch.BranchTypeID;
                addbranch.BranchType = branch.tblBranchType.BranchType;
                addbranch.BranchName = branch.BranchName;
                addbranch.BranchContact = branch.BranchContact;
                addbranch.BranchAddress = branch.BranchAddress;
                addbranch.CompanyID = branch.CompanyID;
                var company = DB.tblCompanies.Find(branch.CompanyID).Name;
                addbranch.Company = company;
                addbranch.BrchID = branch.BrchID;
                list.Add(addbranch);
            }
            return View(list);
        }
        public ActionResult CreateCompanyBranch()
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

            ViewBag.BranchTypeID = new SelectList(DB.tblBranchTypes.Where(bt => bt.BranchTypeID > branchtypeid), "BranchTypeID", "BranchType", "0");
            var branch = new BranchMV();
            branch.CompanyID = companyid; 

            return View(branch);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCompanyBranch(BranchMV branchMV)
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
                var newbranch = new tblBranch();
                newbranch.BranchName = branchMV.BranchName;
                newbranch.BranchTypeID = branchMV.BranchTypeID;
                newbranch.BranchAddress = branchMV.BranchAddress;
                newbranch.BranchContact = branchMV.BranchContact;
                newbranch.CompanyID = branchMV.CompanyID;
                newbranch.BrchID = branchid;
                DB.tblBranches.Add(newbranch);
                DB.SaveChanges();
                return RedirectToAction("AllCompanyBranches");
            }

            ViewBag.BranchTypeID = new SelectList(DB.tblBranchTypes.Where(bt => bt.BranchTypeID > branchtypeid), "BranchTypeID", "BranchType", branchMV.BranchTypeID);
           

            return View(branchMV);
        }
    }
}