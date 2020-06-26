using DataLayer1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utility.Costant;
using ViewModel;

namespace Service
{
    public class GetUserPermission
    {
        //List For Menubar
        public List<MenubarModel> GetPermissionById(int id)
        {
            List<MenubarModel> menubarModel = new List<MenubarModel>();
            try
            {

                using (var db = new CapriHRMEntities())
                {
                    menubarModel = db.RolePermissions.Where(c => c.RoleId == id && c.IsPermission == true).Select(c => new MenubarModel()
                    {
                        ActionName = c.ApplicationPermission.ActionName,
                        ControllerName = c.ApplicationPermission.ControllerName,
                        PermissionName = c.ApplicationPermission.PermissionMaster.PermissionName

                    }).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return menubarModel;
        }
        // Cheack action,Controller which has Read Permission
        public bool CheckActionController(string actionname, string controllername, List<MenubarModel> menubarModels)
        {
            bool IsSaved = false;
            try
            {
                var res = menubarModels.Where(c => c.ActionName == actionname && c.ControllerName == controllername && c.PermissionName == Constant.ReadPermission).FirstOrDefault();
                if (res != null)
                {
                    IsSaved = true;
                }

            }
            catch (Exception)
            {

                throw;
            }
            return IsSaved;
        }
        //Cheack Action,Controller permission 
        public bool CheckActionControllerPermission(string actionname, string controllername, int id, string permissionName)
        {
            bool IsPermission = false;
            RolePermission rolePermission = new RolePermission();
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    rolePermission = db.RolePermissions.Where(c => c.RoleId == id && c.IsPermission == true && c.ApplicationPermission.ControllerName == controllername && c.ApplicationPermission.ActionName == actionname && c.ApplicationPermission.PermissionMaster.PermissionName == permissionName).FirstOrDefault();
                    if (rolePermission != null)
                    {
                        IsPermission = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return IsPermission;
        }
        //List Of Page Permission On Base Of Id
        public List<PagePermissionModel> GetControllerActionAllPermissionByID(string controllname, string actionname, int RoleId)
        {
            List<PagePermissionModel> PagePermissionModel = new List<PagePermissionModel>();
            try
            {

                using (var db = new CapriHRMEntities())
                {
                    PagePermissionModel = db.RolePermissions.Where(c => c.RoleId == RoleId && c.IsPermission == true && c.ApplicationPermission.ControllerName == controllname && c.ApplicationPermission.ActionName == actionname).Select(c => new PagePermissionModel()
                    {
                        PermissionName = c.ApplicationPermission.PermissionMaster.PermissionName
                    }).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return PagePermissionModel;
        }

    }
}