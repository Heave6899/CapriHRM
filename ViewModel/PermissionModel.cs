using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViewModel
{
    public class PermissionMasterModel
    {
        public PermissionModel permissionModel  { get; set; }
        public List<PermissionModel> permissionModels { get; set; }

    }
    public class PermissionModel
    {
        public int PermissionId { get; set; }
        [Required(ErrorMessage = "Permission Name Required")]
        public string PermissionName { get; set; }

    }
}