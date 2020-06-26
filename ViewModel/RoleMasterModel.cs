using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViewModel
{
    public class RoleMasterModel
    {
      
       public RoleModel roleModel { get; set; }
       public List<RoleModel> roleModels { get; set; }
    }
    public class RoleModel
    {
        public int RoleId { get; set; }
        [Required(ErrorMessage = "Role Name Required")]
        public string RoleName { get; set; }

    }
}