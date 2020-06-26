using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Utility.ActionFilter;
using ViewModel;
using Service;
using TestCapriHRM.Helper;
using Utility.Costant;

namespace TestCapriHRM.Controllers
{
    [CoustomAuthenticationFilter]
    public class HomeController : Controller
    {
        MasterService masterService = new MasterService();
        EmployeeService employeeService = new EmployeeService();
        UserService userService = new UserService();
        [HRMAuthorization]
        public ActionResult EmployeeDashbord()
        {
            return View();
        }
        [HRMAuthorization]
        public ActionResult AdminDashbord()
        {
            return View();
        }
        [HRMAuthorization]
        public ActionResult HRDashbord()
        {
            return View();
        }
        [HRMAuthorization]
        public ActionResult Employee(string id = "0")
        {
           
            List<SelectListItem> AllCountry = employeeService.GetCountries();
            List<SelectListItem> Country = employeeService.GetCountries(101);
            List<SelectListItem> Gender = masterService.GetGender();
            List<SelectListItem> MartialStatus = masterService.GetMartialStatus();
            List<SelectListItem> BloodGroup = masterService.GetBloodGroup();

            ViewBag.Country = AllCountry;
            ViewBag.CountryIndia = Country;
            ViewBag.Gender = Gender;
            ViewBag.MartialStatus = MartialStatus;
            ViewBag.BloodGroup = BloodGroup;
            if (id != "0")
            {
                int decodid = Convert.ToInt32(Constant.Decode(id));
                EmployeeModel employeeModel = employeeService.GetListById(decodid);
                ViewBag.State = employeeService.GetState((int)employeeModel.EmployeeContact.Country);
                ViewBag.City = employeeService.GetCity((int)employeeModel.EmployeeContact.State);
                return View(employeeModel);
            }

            else
            {
                ViewBag.State = new List<SelectListItem>() { new SelectListItem { Value = "", Text = "-- Select --" } };
                ViewBag.City = new List<SelectListItem>() { new SelectListItem { Value = "", Text = "-- Select --" } };
            }

            return View();
        }
        
        [HRMAuthorization]
        [HttpPost]
        public ActionResult Employee(EmployeeModel employeeModel)
        {
            if (ModelState.ContainsKey("EmployeeId"))
                ModelState["EmployeeId"].Errors.Clear();
            if (ModelState.IsValid)
            {
                var uid = Session["UserID"].ToString();
                employeeModel.CreateBy = int.Parse(uid);
                employeeModel.UpdateBy = int.Parse(Session["UserID"].ToString());
                if (employeeService.save(employeeModel))
                {
                    return RedirectToAction("ListEmployee", "Home");
                }
                else
                {
                    return RedirectToAction("ListEmployee", "Home");
                }
            }
            else
            {
                return RedirectToAction("Employee", "Home");
            }

        }
        public JsonResult GetState1(string id)
        {
            var State = employeeService.GetState(int.Parse(id));
            return Json(State, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCity1(string id)
        {
            var City = employeeService.GetCity(int.Parse(id));
            return Json(City, JsonRequestBehavior.AllowGet);
        }
        [HRMAuthorization]
        public ActionResult ListEmployee()
        {
            List<EmployeeModel> Employee = new List<EmployeeModel>();
            Employee = employeeService.GetList();
            return View(Employee);
        }

        public ActionResult Delete(string id)
        {

            int decodid = Convert.ToInt32(Constant.Decode(id));
            if (employeeService.Delete(decodid))
            {
                return RedirectToAction("ListEmployee", "Home");
            }

            return View();
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangepasswordModel changepasswordModel)
        {
            changepasswordModel.UserId = Convert.ToInt32(Session["UserId"].ToString());
            string OldPassword = changepasswordModel.OldPassword;
            if (userService.CheckOldPassword(OldPassword, changepasswordModel.UserId))
            {
                userService.ChangePassword(changepasswordModel);
                ViewBag.Success = "Your Password Change Succesfully";
                return View();
            }
            else
            {
                ViewBag.error = "Your Old Password not right";
                return View();
            }


        }
    }

}
