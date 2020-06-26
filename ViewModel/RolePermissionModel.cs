using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViewModel
{
    public class RolePermissionMasterModel
    {
        [Required(ErrorMessage = "Role is required")]
        public int RolePermissionId { get; set; }
        public int? RoleId { get; set; }
        public List<RolePermissionModel> rolePermissionModels { get; set; }

    }
    public class RolePermissionModel
    {
        public int? ApplicationPermissionId { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string PermissionName { get; set; }
        public bool IsPermission { get; set; }
    }

}