using DataLayer1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ViewModel;

namespace Service
{
    public class RoleService
    {
        //Save Role 
        public bool save(RoleMasterModel roleMasterModel)
        {
            bool IsSaved = false;
            try
            {
                using (var db = new CapriHRMEntities())
                {

                    Role role;


                    //edit
                    if (roleMasterModel.roleModel.RoleId != 0)
                    {
                        role = db.Roles.Where(c => c.RoleId == roleMasterModel.roleModel.RoleId).FirstOrDefault();


                    }
                    else
                    {
                        role = new Role();
                        db.Roles.Add(role);

                    }
                    role.RoleName = roleMasterModel.roleModel.RoleName;
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
        // List Of Roles
        public List<RoleModel> GetList()
        {
            List<RoleModel> result = new List<RoleModel>();
            RoleModel roleModel = new RoleModel();
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    result = db.Roles.Select(c => new RoleModel()
                    {
                        RoleId = c.RoleId,
                        RoleName = c.RoleName

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
        //Get  Role By ID
        public RoleModel GetById(int id)
        {
            RoleModel roleModel = new RoleModel();
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    roleModel = db.Roles.Where(c => c.RoleId == id).Select(c => new RoleModel()
                    {
                        RoleId = c.RoleId,
                        RoleName = c.RoleName


                    }).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return roleModel;
        }
        //Delete Role Base On id
        public bool Delete(int id)
        {
            bool IsDelete = false;
            try
            {
                using (var db = new CapriHRMEntities())
                {

                    Role role;
                    //role = new Role();
                    role = db.Roles.Where(c => c.RoleId == id).FirstOrDefault();
                    
                    db.Roles.Remove(role);
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