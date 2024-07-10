using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory_App.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Login(string useremail, string password)
        {
            if (!string.IsNullOrEmpty(useremail) )
            {
                using (InventoryERPDbEntities db = new InventoryERPDbEntities())
                {
                    var user = db.tblUsers.Where(u => u.Email == useremail && u.Password == password && u.IsActive == true).FirstOrDefault();
                    if (user == null)
                    {
                        
                        ViewBag.ErrorMessage = "Username and Password are incorrect";
                    }
                    else
                    {
                        //github check
                        Session["UserID"] = user.UserID;
                        Session["UserName"] = user.UserName;
                        Session["Email"] = user.Email;
                        Session["UserTypeID"] = user.UserTypeID;
                        Session["UserType"] = user.tblUserType.UserType;
                        Session["IsActive"] = user.IsActive == true ? "Active" : "No-Active";
                        var usertypeid = user.UserTypeID;   
                        if (user.UserTypeID == 1)
                        {
                            return RedirectToAction("Admin", "Dashboard");
                        }
                       else if (usertypeid == 2)
                        {
                            return RedirectToAction("SubAdmin", "Dashboard");

                        }
                        else if (usertypeid == 3)
                        {
                            return RedirectToAction("HeadOffice", "Dashboard");

                        }
                        else if (usertypeid == 4)
                        {
                            return RedirectToAction("HeadOfficeUser", "Dashboard");

                        }
                        else if (usertypeid == 5)
                        {
                            return RedirectToAction("BranchUser", "Dashboard");

                        }
                        else if (usertypeid == 6)
                        {
                            return RedirectToAction("BranchOperator", "Dashboard");

                        }

                    }
                }
            }
            else
            {
                ViewBag.ErrorMessage = string.Empty;
            }
            Session["UserID"] = string.Empty;
            Session["UserName"] = string.Empty;
            Session["Email"] = string.Empty;
            Session["UserTypeID"] = string.Empty;
            Session["UserType"] = string.Empty;
            Session["IsActive"] = string.Empty;
            return View();
        }

        public ActionResult Logout()
        {
            Session["UserName"] = string.Empty;
            Session["Email"] = string.Empty;
            Session["UserTypeID"] = string.Empty;
            Session["UserType"] = string.Empty;
            Session["IsActive"] = string.Empty;
            return RedirectToAction("Login");

        }
    }
}
