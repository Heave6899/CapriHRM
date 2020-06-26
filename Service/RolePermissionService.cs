using DataLayer1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;

namespace Service
{
    public class RolePermissionService
    {
        //List Of Rolepermission By ID
        public List<RolePermissionModel> GetList(int id = 0)
        {
            List<RolePermissionModel> result = new List<RolePermissionModel>();
            RolePermissionModel rolePermissionModel = new RolePermissionModel();
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    result = db.ApplicationPermissions.Select(c => new RolePermissionModel()
                    {
                        ControllerName = c.ControllerName,
                        ActionName = c.ActionName,
                        PermissionName = c.PermissionMaster.PermissionName,
                        ApplicationPermissionId = c.ApplicationPermissionId
                    }).ToList();
                    //PermissionId = c.PermissionId,
                    //if role have permission
                    if (id != 0)
                    {
                        var roleResult = db.RolePermissions.Where(c => c.RoleId == id && c.IsPermission == true).Select(c => c.ApplicationPermissionId).ToList();

                        result.Where(c => roleResult.Contains(c.ApplicationPermissionId)).ToList().ForEach(c => c.IsPermission = true);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            //roleModel.RoleId = 1;
            //roleModel.RoleName = "Admin";
            //result.Add(roleModel);
            //RoleModel roleModel1 = new RoleModel();
            //roleModel1.RoleId = 2;
            //roleModel1.RoleName = "Employee";
            //result.Add(roleModel1);
            // new RoleModel { RoleId = 1, RoleName = "D" }
            //result.Add();
            return result;
        }
        //Get List of Role 
        public List<SelectListItem> GetRole()
        {
            using (var db = new CapriHRMEntities())
            {

                List<SelectListItem> List1 = (from s in db.Roles
                                              select new SelectListItem
                                              {
                                                  Text = s.RoleName,
                                                  Value = s.RoleId.ToString()
                                              }).ToList();

                List1.Insert(0, new SelectListItem { Text = "--Select Role--", Value = "" });
                return List1;
            };

        }
        //Save Rolepermission On Base of Role
        public bool save(RolePermissionMasterModel rolePermissionMasterModel)
        {
            bool IsSaved = false;
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    foreach (var item in rolePermissionMasterModel.rolePermissionModels)
                    {
                        var oldRolePermissions = db.RolePermissions.Where(c => c.RoleId == rolePermissionMasterModel.RoleId).ToList();
                        var oldRolePermission = oldRolePermissions.Where(c => c.RoleId == rolePermissionMasterModel.RoleId && c.ApplicationPermissionId == item.ApplicationPermissionId).FirstOrDefault();


                        //permission already exist
                        if (oldRolePermission == null && item.IsPermission == true)
                        {
                            RolePermission rolePermission = new RolePermission();
                            rolePermission.RoleId = rolePermissionMasterModel.RoleId;
                            rolePermission.ApplicationPermissionId = item.ApplicationPermissionId;
                            rolePermission.IsPermission = true;
                            db.RolePermissions.Add(rolePermission);
                        }
                        else if (oldRolePermission != null)
                        {
                            oldRolePermission.IsPermission = item.IsPermission;
                        }
                    }
                    int res = db.SaveChanges();
                    if (res > 0)
                    {
                        IsSaved = true;
                    }


                }
            }
            catch (Exception)
            {

                throw;
            }

            return IsSaved;
        }
    }
}