using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;
using Service;
using Utility.Costant;

namespace TestCapriHRM.Controllers
{
    public class AccountController : Controller
    {
        UserService userService = new UserService();
        // GET: Account

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserModel userModel)
        {
            int uid = userService.UserLogin(userModel.UserName, userModel.Password);
            if (uid != 0)
            {
                var userDetails = userService.GetUserDetailsId(uid);
                Session["UserID"] = uid;
                Session["UserName"] = userDetails.UserName;
                Session["RoleId"] = userDetails.RoleId;
                if (userDetails.RoleId == Constant.AdminRoleId)
                {
                    return RedirectToAction("AdminDashbord", "Home");
                }
                else if (userDetails.RoleId == Constant.HRRoleId)
                {
                    return RedirectToAction("HRDashbord", "Home");
                }
                else if (userDetails.RoleId == Constant.EmployeeRoleId)
                {
                    return RedirectToAction("EmployeeDashbord", "Home");
                }
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
    }
}