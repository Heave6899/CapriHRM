using DataLayer1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ViewModel;

namespace Service
{
    public class PermissionService
    {
        //Save Permission
        public bool save(PermissionMasterModel permissionMasterModel)
        {
            bool IsSaved = false;
            try
            {
                using (var db = new CapriHRMEntities())
                {

                    PermissionMaster permissionMaster;


                    //edit
                    if (permissionMasterModel.permissionModel.PermissionId != 0)
                    {
                        permissionMaster = db.PermissionMasters.Where(c => c.PermissionId == permissionMasterModel.permissionModel.PermissionId).FirstOrDefault();


                    }
                    else
                    {
                        permissionMaster = new PermissionMaster();
                        db.PermissionMasters.Add(permissionMaster);

                    }
                    permissionMaster.PermissionName = permissionMasterModel.permissionModel.PermissionName;
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
        //List of Permissions
        public List<PermissionModel> GetList()
        {
            List<PermissionModel> result = new List<PermissionModel>();
            PermissionModel permissionModel = new PermissionModel();
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    result = db.PermissionMasters.Select(c => new PermissionModel()
                    {
                        PermissionId = c.PermissionId,
                        PermissionName = c.PermissionName

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
        //Get Permission By Id
        public PermissionModel GetById(int id)
        {
            PermissionModel permissionModel = new PermissionModel();
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    permissionModel = db.PermissionMasters.Where(c => c.PermissionId == id).Select(c => new PermissionModel()
                    {
                        PermissionId = c.PermissionId,
                        PermissionName = c.PermissionName


                    }).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return permissionModel;
        }
        //Delete Permission
        public bool Delete(int id)
        {
            bool IsDelete = false;
            try
            {
                using (var db = new CapriHRMEntities())
                {

                    PermissionMaster permissionMaster;
                    //role = new Role();
                    permissionMaster = db.PermissionMasters.Where(c => c.PermissionId == id).FirstOrDefault();
                    
                    db.PermissionMasters.Remove(permissionMaster);
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