using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViewModel
{
    public class ChangepasswordModel
    {
        public int UserId { get; set; }

        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirmation Password is required.")]
        [Compare("NewPassword", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConformPassword { get; set; }
        public string Salt { get; set; }




    }

}