using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViewModel
{
    public class ApplicationPermissionMasterModel
    {
        public ApplicationPermissionModel applicationPermissionModel { get; set; }
        public List<ApplicationPermissionModel> applicationPermissionModels { get; set; }
    }
    public class ApplicationPermissionModel
    {
        public int ApplicationPermissionId { get; set; }
        [Required(ErrorMessage = "Controller Name Required")]
        public string ControllerName { get; set; }
        [Required(ErrorMessage = "Action Name Required")]
        public string ActionName { get; set; }
        [Required(ErrorMessage = "Permission Required")]
        public int? PermissionId { get; set; }
        public string PermissionName { get; set; }

    }


    public class SubmitPermissionModel
    {
        public bool isPermission { get; set; }
        public string PermissionName { get; set; }
    }
}