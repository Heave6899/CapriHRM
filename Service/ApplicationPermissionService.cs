using DataLayer1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;

namespace Service
{
    public class ApplicationPermissionService
    {
        //Get Permission
        public List<SelectListItem> GetPermission()
        {
            using (var db = new CapriHRMEntities())
            {

                List<SelectListItem> List1 = (from s in db.PermissionMasters
                                              select new SelectListItem
                                              {
                                                  Text = s.PermissionName,
                                                  Value = s.PermissionId.ToString()
                                              }).ToList();

                List1.Insert(0, new SelectListItem { Text = "--Select Permission--", Value = "" });
                return List1;
            };

        }
        //Save Application Permission
        public bool save(ApplicationPermissionMasterModel applicationPermissionMasterModel)
        {
            bool IsSaved = false;
            try
            {
                using (var db = new CapriHRMEntities())
                {

                    ApplicationPermission applicationPermission;


                    //edit
                    if (applicationPermissionMasterModel.applicationPermissionModel.ApplicationPermissionId != 0)
                    {
                        applicationPermission = db.ApplicationPermissions.Where(c => c.ApplicationPermissionId == applicationPermissionMasterModel.applicationPermissionModel.ApplicationPermissionId).FirstOrDefault();


                    }
                    else
                    {
                        applicationPermission = new ApplicationPermission();
                        db.ApplicationPermissions.Add(applicationPermission);

                    }
                    applicationPermission.ControllerName = applicationPermissionMasterModel.applicationPermissionModel.ControllerName;
                    applicationPermission.ActionName = applicationPermissionMasterModel.applicationPermissionModel.ActionName;
                    applicationPermission.PermissionId = applicationPermissionMasterModel.applicationPermissionModel.PermissionId;

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
        //List Application Permission
        public List<ApplicationPermissionModel> GetList()
        {
            List<ApplicationPermissionModel> result = new List<ApplicationPermissionModel>();
            ApplicationPermissionModel applicationPermissionModel = new ApplicationPermissionModel();
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    result = db.ApplicationPermissions.Select(c => new ApplicationPermissionModel()
                    {
                        ApplicationPermissionId = c.ApplicationPermissionId,
                        ControllerName = c.ControllerName,
                        ActionName = c.ActionName,
                        PermissionId = c.PermissionId,
                        PermissionName = c.PermissionMaster.PermissionName


                    }).ToList();
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
        // Get Application Permission On base of Id
        public ApplicationPermissionModel GetById(int id)
        {
            ApplicationPermissionModel applicationPermissionModel = new ApplicationPermissionModel();
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    applicationPermissionModel = db.ApplicationPermissions.Where(c => c.ApplicationPermissionId == id).Select(c => new ApplicationPermissionModel()
                    {
                        ApplicationPermissionId = c.ApplicationPermissionId,
                        ControllerName = c.ControllerName,
                        ActionName = c.ActionName,
                        PermissionId = c.PermissionId,
                        PermissionName = c.PermissionMaster.PermissionName
                    }).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return applicationPermissionModel;
        }
        // Delete Application Permission
        public bool Delete(int id)
        {
            bool IsDelete = false;
            try
            {
                using (var db = new CapriHRMEntities())
                {

                    //ApplicationPermission applicationPermission;
                    var applicationPermission1= (from obj in db.ApplicationPermissions where obj.ApplicationPermissionId == id select obj).First();
                    //applicationPermission = db.ApplicationPermissions.Where(c => c.ApplicationPermissionId == id).FirstOrDefault();
                    db.RolePermissions.RemoveRange(applicationPermission1.RolePermissions);
                    db.ApplicationPermissions.Remove(applicationPermission1);
                    int res = db.SaveChanges();
                    if (res > 0)
                    {
                        IsDelete = true;
                    }


                }
            }
            catch (Exception)
            {

                throw;
            }

            return IsDelete;
        }
    }
}