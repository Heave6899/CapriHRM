using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestCapriHRM.Helper;
using Utility.ActionFilter;
using ViewModel;
using Utility.Costant;


namespace TestCapriHRM.Controllers
{
    [CoustomAuthenticationFilter]

    public class MasterController : Controller
    {
        RoleService roleService = new RoleService();
        RolePermissionService rolepermissionService = new RolePermissionService();
        PermissionService permissionService = new PermissionService();
        ApplicationPermissionService ApplicationPermissionService = new ApplicationPermissionService();
        // GET: Master
        //Role 
        [HRMAuthorization]
        public ActionResult Role(string id = "0")
        {
            RoleMasterModel roleMasterModel = new RoleMasterModel();
            if (id != "0")
            {
                int decodeid = Convert.ToInt32(Constant.Decode(id));
                roleMasterModel.roleModel = roleService.GetById(decodeid);
            }
            roleMasterModel.roleModels = roleService.GetList();
            return View(roleMasterModel);
        }
        [HRMAuthorization]
        [HttpPost]
        public ActionResult Role(RoleMasterModel roleMasterModel)
        {

            if (ModelState.ContainsKey("roleModel.RoleId"))
                ModelState["roleModel.RoleId"].Errors.Clear();

            if (ModelState.IsValid)
            {
                roleService.save(roleMasterModel);
                return RedirectToAction("Role", "Master", new { @id = 0 });
            }
            else
            {
                return RedirectToAction("Role", "Master");
            }
            //return View();
        }

        public ActionResult DeleteRole(string id)
        {
            // RoleMasterModel roleMasterModel = new RoleMasterModel();
            if (id != "0")
            {
                int decodeid = Convert.ToInt32(Constant.Decode(id));
                roleService.Delete(decodeid);
                return RedirectToAction("Role", "Master", new { @id = "0" });
            }

            return RedirectToAction("Role", "Master", new { @id = "0" });
        }

        //Permission
        [HRMAuthorization]
        public ActionResult Permission(string id = "0")
        {
            PermissionMasterModel permissionMasterModel = new PermissionMasterModel();
            if (id != "0")
            {
                int decodeid = Convert.ToInt32(Constant.Decode(id));
                permissionMasterModel.permissionModel = permissionService.GetById(decodeid);
            }
            permissionMasterModel.permissionModels = permissionService.GetList();
            return View(permissionMasterModel);
        }
        [HRMAuthorization]
        [HttpPost]
        public ActionResult Permission(PermissionMasterModel permissionMasterModel)
        {

            if (ModelState.ContainsKey("permissionModel.PermissionId"))
                ModelState["permissionModel.PermissionId"].Errors.Clear();

            if (ModelState.IsValid)
            {
                permissionService.save(permissionMasterModel);
                return RedirectToAction("Permission", "Master", new { @id = 0 });
            }
            else
            {
                return RedirectToAction("Permission", "Permission");
            }
            //return View();
        }
        public ActionResult DeletePermission(string id)
        {
            // RoleMasterModel roleMasterModel = new RoleMasterModel();
            if (id != "0")
            {
                int decodeid = Convert.ToInt32(Constant.Decode(id));
                permissionService.Delete(decodeid);
                return RedirectToAction("Permission", "Master", new { @id = "0" });
            }

            return RedirectToAction("Permission", "Master", new { @id = "0" });
        }
        //ApplicationPermission
        [HRMAuthorization]
        public ActionResult ApplicationPermission(string id = "0")
        {
            ApplicationPermissionMasterModel applicationPermissionMasterModel = new ApplicationPermissionMasterModel();
            List<SelectListItem> AllPermission = ApplicationPermissionService.GetPermission();
            ViewBag.Permission = AllPermission;
            if (id != "0")
            {
                int decodeid = Convert.ToInt32(Constant.Decode(id));
                applicationPermissionMasterModel.applicationPermissionModel = ApplicationPermissionService.GetById(decodeid);
            }
            applicationPermissionMasterModel.applicationPermissionModels = ApplicationPermissionService.GetList();
            return View(applicationPermissionMasterModel);
        }
        [HRMAuthorization]
        [HttpPost]
        public ActionResult ApplicationPermission(ApplicationPermissionMasterModel applicationPermissionMasterModel)
        {

            if (ModelState.ContainsKey("applicationPermissionModel.ApplicationPermissionId"))
                ModelState["applicationPermissionModel.ApplicationPermissionId"].Errors.Clear();

            if (ModelState.IsValid)
            {
                ApplicationPermissionService.save(applicationPermissionMasterModel);
                return RedirectToAction("ApplicationPermission", "Master", new { @id = 0 });
            }
            else
            {
                return RedirectToAction("ApplicationPermission", "Master");
            }
            //return View();
        }
        public ActionResult DeleteApplicationPermission(string id)
        {
            // RoleMasterModel roleMasterModel = new RoleMasterModel();
            if (id != "0")
            {
                int decodeid = Convert.ToInt32(Constant.Decode(id));
                ApplicationPermissionService.Delete(decodeid);
                return RedirectToAction("ApplicationPermission", "Master", new { @id = "0" });
            }

            return RedirectToAction("ApplicationPermission", "Master", new { @id = "0" });
        }

        //RolePermission
        [HRMAuthorization]
        public ActionResult RolePermission(string id = "0")
        {
            RolePermissionMasterModel rolePermissionMasterModel = new RolePermissionMasterModel();
            List<SelectListItem> AllRole = rolepermissionService.GetRole();
            ViewBag.Role = AllRole;
            int decodeid = id == "0" ? (decodeid = 0) : (decodeid = Convert.ToInt32(Constant.Decode(id)));
            rolePermissionMasterModel.rolePermissionModels = rolepermissionService.GetList(decodeid);
            if (id != "0")
            {

                rolePermissionMasterModel.RoleId = decodeid;
            }
            return View(rolePermissionMasterModel);
        }
        [HRMAuthorization]
        [HttpPost]
        public ActionResult RolePermission(RolePermissionMasterModel rolePermissionMasterModel)
        {
            rolepermissionService.save(rolePermissionMasterModel);

            return RedirectToAction("RolePermission", "Master", new { @id = "0" });
        }

        
        public ActionResult EncodeId(int Id)
        {
            string result = Constant.Encode(Id.ToString());
            return RedirectToAction("RolePermission", new { @id=result});
        }
    }
}