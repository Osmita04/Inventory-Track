using DatabaseLayer;
using Inventory_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory_App.Controllers
{
    public class CompanyController : Controller
    {

        InventoryERPDbEntities DB = new InventoryERPDbEntities();
        // GET: Company
        public ActionResult Companies()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            var listcompanies = new List<CompaniesRequestMV>();
            var companies = DB.tblCompanies.ToList();

            foreach (var company in companies)
            {
                var r_company = new CompaniesRequestMV();
                r_company.CompanyID = company.CompanyID;
                r_company.Company = company.Name;

                // Branch
                var branch = DB.tblBranches.Where(b => b.CompanyID == r_company.CompanyID).FirstOrDefault();
                if (branch != null)
                {
                    r_company.BranchID = branch.BranchID;
                    r_company.BranchTypeID = branch.BranchTypeID;
                    var branchtype = branch.tblBranchType == null ? "No Type" : branch.tblBranchType.BranchType;
                    r_company.BranchType = branchtype;
                    r_company.BranchName = branch.BranchName;
                    r_company.BranchContact = branch.BranchContact;
                    r_company.BranchAddress = branch.BranchAddress;

                    // Employee
                    var branchemployee = DB.tblEmployees.Where(e => e.BranchID == branch.BranchID && e.CompanyID == r_company.CompanyID).FirstOrDefault();
                    if (branchemployee != null)
                    {
                        r_company.EmployeeID = branchemployee.EmployeeID;
                        r_company.EmployeeName = branchemployee.Name;
                        r_company.ContactNo = branchemployee.ContactNo;
                        r_company.Photo = string.Empty;
                        r_company.Email = branchemployee.Email;
                        r_company.Address = branchemployee.Address;
                        r_company.CNIC = branchemployee.CNIC;
                        r_company.Designation = branchemployee.Designation;
                        r_company.Description = branchemployee.Description;
                        r_company.MonthlySalary = branchemployee.MonthlySalary;

                        // User
                        var user = DB.tblUsers.Where(u => u.UserID == branchemployee.UserID).FirstOrDefault();
                        if (user != null)
                        {
                            r_company.UserID = user.UserID;
                            r_company.UserTypeID = user.UserTypeID;
                            var usertype = user.tblUserType != null ? user.tblUserType.UserType : "No User Type";
                            r_company.UserType = usertype;
                            r_company.UserName = user.UserName;
                            r_company.Password = user.Password;
                            r_company.Status = user.IsActive ? "Active" : "De-Active";
                        }
                        else
                        {
                            r_company.UserID = 0;
                            r_company.UserTypeID = 0;
                            r_company.UserType = "No User Found";
                            r_company.UserName = string.Empty;
                            r_company.Password = string.Empty;
                            r_company.Status = string.Empty;
                        }
                    }
                    else
                    {
                        r_company.EmployeeID = 0;
                        r_company.EmployeeName = "No Employee Found";
                        r_company.ContactNo = string.Empty;
                        r_company.Photo = string.Empty;
                        r_company.Email = string.Empty;
                        r_company.Address = string.Empty;
                        r_company.CNIC = string.Empty;
                        r_company.Designation = string.Empty;
                        r_company.Description = string.Empty;
                        r_company.MonthlySalary = 0;


                        // Handle user default values in the absence of employees
                        r_company.UserID = 0;
                        r_company.UserTypeID = 0;
                        r_company.UserType = "No User Found";
                        r_company.UserName = string.Empty;
                        r_company.Password = string.Empty;
                        r_company.Status = string.Empty;
                    }
                }
                else
                {
                    // Handle the case where no branch is found
                    r_company.BranchID = 0; // Default value
                    r_company.BranchTypeID = 0; // Default value
                    r_company.BranchType = "No Branch Found";
                    r_company.BranchName = string.Empty;
                    r_company.BranchContact = string.Empty;
                    r_company.BranchAddress = string.Empty;

                    // Handle the absence of employees when no branch is found
                    r_company.EmployeeID = 0;
                    r_company.EmployeeName = "No Employee Found";
                    r_company.ContactNo = string.Empty;
                    r_company.Photo = string.Empty;
                    r_company.Email = string.Empty;
                    r_company.Address = string.Empty;
                    r_company.CNIC = string.Empty;
                    r_company.Designation = string.Empty;
                    r_company.Description = string.Empty;
                    r_company.MonthlySalary = 0;

                    r_company.UserID = 0;
                    r_company.UserTypeID = 0;
                    r_company.UserType = "No User Found";
                    r_company.UserName = string.Empty;
                    r_company.Password = string.Empty;
                    r_company.Status = string.Empty;
                }

                listcompanies.Add(r_company);
            }

            return View(listcompanies);
        }




        public ActionResult NewCompany()
        {
            var model = new CompanyMV();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult NewCompany(
            string UserName,
            string Password,
            string CPassword,
            string EName,
            string EContactNo,
            string EEmail,
            string ECNIC,
            string EDesignation,
            float EMonthlySalary,
            string EAddress,
            string CName,
            string BranchName,
            string BranchContact,
            string BranchAddress)
        {

           using (var trans = DB.Database.BeginTransaction()) 

                try
                {
                    if (!string.IsNullOrEmpty(UserName)
                         && !string.IsNullOrEmpty(Password)
                         && !string.IsNullOrEmpty(EName)
                         && !string.IsNullOrEmpty(EContactNo)
                         && !string.IsNullOrEmpty(EEmail)
                         && !string.IsNullOrEmpty(ECNIC)
                         && !string.IsNullOrEmpty(EDesignation)
                         && EMonthlySalary > 0
                         && !string.IsNullOrEmpty(EAddress)
                         && !string.IsNullOrEmpty(CName)
                         && !string.IsNullOrEmpty(BranchName)
                         && !string.IsNullOrEmpty(BranchContact)
                         && !string.IsNullOrEmpty(BranchAddress)
                         )
                    {

                        var checkcompany = DB.tblCompanies.Where(c => c.Name == CName).FirstOrDefault();
                        if (checkcompany != null)
                        {
                            ViewBag.Message = "Company Already Exist";
                            trans.Rollback();
                            return View();

                        }
                        var company = new tblCompany()
                        {
                            Name = CName,
                            Logo = string.Empty
                        };
                        DB.tblCompanies.Add(company);
                        DB.SaveChanges();

                        var branch = new tblBranch()
                        {
                            BranchAddress = BranchAddress,
                            BranchContact = BranchContact,
                            BranchName = BranchName,
                            BranchTypeID = 1,
                            CompanyID = company.CompanyID,
                            BrchID = null
                        };

                        DB.tblBranches.Add(branch);
                        DB.SaveChanges();

                        var checkusername = DB.tblUsers.Where(c => c.UserName == UserName).FirstOrDefault();
                        if (checkusername != null)
                        {
                            ViewBag.Message = "UserName Already Exist";
                            trans.Rollback();
                            return View();

                        }
                        var user = new tblUser()
                        {
                            ContactNo = EContactNo,
                            Email = EEmail,
                            FullName = EName,
                            IsActive = false,
                            Password = Password,
                            UserName = UserName,
                            UserTypeID = 3,
                            Address = BranchAddress
                        };

                        DB.tblUsers.Add(user);
                        DB.SaveChanges();

                        var employee = new tblEmployee()
                        {
                            Address = EAddress,
                            BranchID = branch.BranchID,
                            CNIC = ECNIC,
                            CompanyID = company.CompanyID,
                            ContactNo = EContactNo,
                            Designation = EDesignation,
                            Email = EEmail,
                            MonthlySalary = EMonthlySalary,
                            UserID = user.UserID,
                            Name = EName,
                            Description = "Enter Description Here"
                        };

                        DB.tblEmployees.Add(employee);
                        DB.SaveChanges();
                        ViewBag.Message = "Registration Successfully";
                        trans.Commit();
                        return RedirectToAction("Login", "Home");
                    }
                    else
                    {
                        ViewBag.Message = "Please Provide Correct Details!";
                        trans.Rollback();
                        return View("NewCompany");
                    }

                }
                catch (Exception )
                {
                    trans.Rollback();
                    ViewBag.Message = "Please Contact To Adminstrator!";
                    return View();
                }
            
        }
        public ActionResult Approve(int? userid)
        {
            var user = DB.tblUsers.Find(userid);
            user.IsActive = true;
            DB.Entry(user).State = System.Data.Entity.EntityState.Modified;
            DB.SaveChanges();
            return RedirectToAction("Companies");
        }

        public ActionResult DeApprove(int? userid)
        {
            var user = DB.tblUsers.Find(userid);
            user.IsActive = false;
            DB.Entry(user).State = System.Data.Entity.EntityState.Modified;
            DB.SaveChanges();
            return RedirectToAction("Companies");
        }
    }
}