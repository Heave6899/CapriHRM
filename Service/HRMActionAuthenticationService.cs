using DataLayer1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service
{
    public class HRMActionAuthenticationService
    {
        //Check User Have Permission For this Action and Controller or not .
        public bool HRMActionAuthenticationFillter(string actionname, string controllername, int id)
        {
            bool IsPermission = false;
           
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    RolePermission rolePermission = new RolePermission();
                    rolePermission = db.RolePermissions.Where(c => c.RoleId == id && c.IsPermission == true && c.ApplicationPermission.ControllerName == controllername && c.ApplicationPermission.ActionName == actionname && c.ApplicationPermission.PermissionMaster.PermissionName =="Read").FirstOrDefault();
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
    }
}