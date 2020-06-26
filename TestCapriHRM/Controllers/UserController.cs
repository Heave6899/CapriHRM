using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using TestCapriHRM.Helper;
using Utility.ActionFilter;
using ViewModel;
using Utility.Costant;

namespace TestCapriHRM.Controllers
{
    [CoustomAuthenticationFilter]

    public class UserController : Controller
    {
        MasterService masterService = new MasterService();
        UserService userService = new UserService();
        EmployeeService employeeService = new EmployeeService();
        GetUserPermission getUserPermission = new GetUserPermission();
        public ActionResult UserTopMenu()
        {
            //var userId = Convert.ToInt32(Session["UserId"].ToString());
            //UserModel userModel = userService.GetUserById(userId);

            int userid = Convert.ToInt32(Session["UserID"].ToString());
            UserTopMenuModel userTopMenuModel = userService.UserProfileImage(userid);
            List<SelectListItem> Gender = masterService.GetGender();
            userTopMenuModel.Gender = Gender.Where(c => c.Value == userTopMenuModel.Gender).Select(c => c.Text).FirstOrDefault();
            if (userTopMenuModel.UserImage == "" || userTopMenuModel.UserImage == null)
            {
                if (userTopMenuModel.Gender == "Male")
                {
                    userTopMenuModel.UserImage = "/dist/img/avatar5.png";
                }
                if (userTopMenuModel.Gender == "Female")
                {
                    userTopMenuModel.UserImage = "/dist/img/avatar3.png";
                }
            }
            return PartialView("UserTopMenu", userTopMenuModel);
        }
        public ActionResult UserSidebar()
        {
            var RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            //UserModel userModel = userService.GetUserById(userId);
            List<MenubarModel> menubarModel = getUserPermission.GetPermissionById(RoleId);
            //userTopMenuModel.UserImage = "/dist/img/avatar5.png";
            //userTopMenuModel.UserName = userModel.UserName;

            return PartialView("mainsidebar", menubarModel);
        }
        public ActionResult SubmitButton(string controllname, string actionname, bool isUpdate)
        {
            var RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            var isPermission = getUserPermission.CheckActionControllerPermission(actionname, controllname, RoleId, isUpdate == true ? "Update" : "Add");

            SubmitPermissionModel submitPermissionModel = new SubmitPermissionModel();
            submitPermissionModel.isPermission = isPermission;
            submitPermissionModel.PermissionName = isUpdate == true ? "Update" : "Add";

            return PartialView("SubmitButton", submitPermissionModel);
        }
        public ActionResult GridButton(string controllname, string actionname, int id, string DeleteAction = "Delete", string UpdateAction = "")
        {
            var RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            List<PagePermissionModel> PagePermissionModel = getUserPermission.GetControllerActionAllPermissionByID(controllname, actionname, RoleId);
            //PagePermissionModel.ForEach(c => c.Id = id);
            foreach (var item in PagePermissionModel)
            {
                item.Id = id;
                item.ControllerName = controllname;
                if (item.PermissionName == "Delete")
                {
                    item.ActionName = DeleteAction;
                }
                else if (item.PermissionName == "Update")
                {
                    item.ActionName = UpdateAction;
                }
            }
            return PartialView("GridButton", PagePermissionModel);
        }
        // GET: User
        [HRMAuthorization]
        public ActionResult CreateUser()
        {
            List<SelectListItem> Employee = userService.GetEmployee();
            List<SelectListItem> Role = userService.GetRole();
            ViewBag.Employee = Employee;
            ViewBag.Role = Role;
            return View();
        }
        [HRMAuthorization]
        [HttpPost]
        public ActionResult CreateUser(UserModel userModel)
        {
            userModel.CreateBy = Convert.ToInt32(Session["UserID"]);
            userModel.CreatedDate = DateTime.Today;
            if (ModelState.ContainsKey("UserId"))
                ModelState["UserId"].Errors.Clear();
            if (ModelState.IsValid)
            {
                userService.saveUser(userModel);
                return RedirectToAction("CreateUser");

            }
            return View();
        }
        [HRMAuthorization]
        public ActionResult ListManageUser()
        {
            List<UserModelList> user = new List<UserModelList>();
            user = userService.GetList();
            return View(user);
        }
        public ActionResult ResetPasswordOfUser(int id)
        {
            if (userService.ResetPassword(id))
            {
                return RedirectToAction("ListManageUser", "User");
            }

            return View();
        }
        public ActionResult DeleteUser(string id)
        {

            int decodid = Convert.ToInt32(Constant.Decode(id));
            if (userService.DeleteUser(decodid))
            {
                return RedirectToAction("ListManageUser", "User", new { @id = 0 });
            }

            return View();
        }
        public ActionResult UserProfile()
        {
            int id = Convert.ToInt32(Session["UserID"].ToString());
            UserProfileModel userProfileModel = new UserProfileModel();
            userProfileModel = employeeService.GetEmployee(id);
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
            ViewBag.State = employeeService.GetState((int)userProfileModel.userProfilePersonalModel.Country);
            ViewBag.City = employeeService.GetCity((int)userProfileModel.userProfilePersonalModel.State);

            if (userProfileModel.ProfileImage == "" || userProfileModel.ProfileImage == null)
            {
                if (userProfileModel.Gender == "1")
                {
                    userProfileModel.ProfileImage = "/dist/img/avatar5.png";
                }
                if (userProfileModel.Gender == "2")
                {
                    userProfileModel.ProfileImage = "/dist/img/avatar3.png";
                }
            }
            ViewBag.GenderName = Gender.Where(c => c.Value == userProfileModel.Gender).Select(c => c.Text).FirstOrDefault();
            ViewBag.BloodGroupName = BloodGroup.Where(c => c.Value == userProfileModel.userProfilePersonalModel.BloodGroup).Select(c => c.Text).FirstOrDefault();
            ViewBag.MartialStatusName = MartialStatus.Where(c => c.Value == userProfileModel.MaritalStatus).Select(c => c.Text).FirstOrDefault();
            return View(userProfileModel);

        }
        [HttpPost]
        public ActionResult UserProfile(UserProfileModel userProfileModel)
        {
            if (ModelState.ContainsKey("EmployeeId"))
            {
                ModelState["EmployeeId"].Errors.Clear();
                ModelState["ProfileImage"].Errors.Clear();
            }
            if (ModelState.IsValid)
            {
                var uid = Session["UserID"].ToString();
                userProfileModel.UpdateBy = int.Parse(Session["UserID"].ToString());
                if (userService.UpdateByUser(userProfileModel))
                {
                    return RedirectToAction("UserProfile", "User");
                }
                else
                    return RedirectToAction("UserProfile", "User");
            }
            else
            {
                return RedirectToAction("UserProfile", "User");
            }
            return View();

        }
        [HttpPost]
        public ActionResult ProfileImage(UserProfileModel userProfileModel, HttpPostedFileBase ProfileImage)
        {
            if (ModelState.ContainsKey("EmployeeId"))
            {
                ModelState["EmployeeId"].Errors.Clear();
                ModelState["ProfileImage"].Errors.Clear();
            }
            if (ModelState.IsValid)
            {
                string path = "-1";
                string Imagepath = "-1";
                if (ProfileImage != null && ProfileImage.ContentLength > 0)
                {
                    if (!Directory.Exists(@"C:\Shaktiraj\MVC Tutorial Project\TestCapriHRM\TestCapriHRM\Image\UserProfileImage\" + userProfileModel.EmployeeId))
                    {
                        Directory.CreateDirectory(@"C:\Shaktiraj\MVC Tutorial Project\TestCapriHRM\TestCapriHRM\Image\UserProfileImage\" + userProfileModel.EmployeeId);
                        string extenstion = Path.GetExtension(ProfileImage.FileName);
                        if (extenstion.ToLower().Equals(".jpg") || extenstion.ToLower().Equals(".jpeg") || extenstion.ToLower().Equals(".png"))
                        {
                            try
                            {
                                path = Path.Combine(Server.MapPath("/Image/UserProfileImage/" + userProfileModel.EmployeeId), Path.GetFileName(ProfileImage.FileName));
                                ProfileImage.SaveAs(path);
                                Imagepath = "/Image/UserProfileImage/" + userProfileModel.EmployeeId + "/" + Path.GetFileName(ProfileImage.FileName);
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                        }
                        userProfileModel.ProfileImage = Imagepath;
                        userProfileModel.ServerMapPathImage = path;

                    }
                    else
                    {
                        string extenstion = Path.GetExtension(ProfileImage.FileName);
                        if (extenstion.ToLower().Equals(".jpg") || extenstion.ToLower().Equals(".jpeg") || extenstion.ToLower().Equals(".png"))
                        {
                            try
                            {
                                path = Path.Combine(Server.MapPath("/Image/UserProfileImage/" + userProfileModel.EmployeeId), Path.GetFileName(ProfileImage.FileName));
                                if (!System.IO.File.Exists(path))
                                {
                                    ProfileImage.SaveAs(path);
                                }
                                Imagepath = "/Image/UserProfileImage/" + userProfileModel.EmployeeId + "/" + Path.GetFileName(ProfileImage.FileName);
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                            userProfileModel.ProfileImage = Imagepath;
                            userProfileModel.ServerMapPathImage = path;
                        }
                    }
                }
                string ServerImagepath = userService.ProfileImageSave(userProfileModel);
                if (ServerImagepath == null || ServerImagepath == "")
                {

                    return RedirectToAction("UserProfile", "User");
                }
                else
                {
                    if (ServerImagepath != path)
                    {
                        System.IO.File.Delete(ServerImagepath);
                    }

                    return RedirectToAction("UserProfile", "User");
                }


            }
            else
            {
                return RedirectToAction("UserProfile", "User");
            }

        }
        public ActionResult RemoveImage(int id)
        {
            string imagepath = userService.RemoveProfileImage(id);
            if (imagepath != null)
            {
                System.IO.File.Delete(imagepath);
                return RedirectToAction("UserProfile", "User");
            }
            else
            {
                return RedirectToAction("UserProfile", "User");
            }


        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Account");

        }
    }
}