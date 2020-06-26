using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViewModel
{
    public class UserModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please enter user email")]
        [Display(Name = "User Email")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirmation Password is required.")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConformPassword { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? CreateBy { get; set; }
        public int? UpdateBy { get; set; }
        [Required(ErrorMessage = "Role is required.")]
        public int? Role { get; set; }
        public string Salt { get; set; }
        [Required(ErrorMessage = "Employee is required.")]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public List<UserModelList> userModelLists { get; set; }
    }
    public class UserModelList
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string EmployeeName { get; set; }
        public string RoleName { get; set; }


    }

    public class UserTopMenuModel
    {
        public string UserImage { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
    }
}