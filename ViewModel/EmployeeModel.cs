using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViewModel
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }
        [Required(ErrorMessage ="First Name required")]
        public string FristName { get; set; }
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Middle Name required ")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Gender is required ")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Nationality reuired ")]
        public int? Nationality { get; set; }
        public string NationalityName { get; set; }
        [Required(ErrorMessage = "Birthdate reuired ")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }
        public string PanCardNo { get; set; }
        [Required(ErrorMessage = "Martial Status reuired ")]
        public string MaritalStatus { get; set; }
        
        //public string Image { get; set; }
        public bool? IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<int> UpdateBy { get; set; }
        //public string oldImage { get; set; }
        public EmployeeContactModel EmployeeContact { get; set; }
    }

    public class EmployeeContactModel
    {

        public string ZipCode { get; set; }
        [Required(ErrorMessage = "Address reuired ")]
        public string Address { get; set; }
        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Mobile Not Valid ")]
        public string MobileNo { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email-id Not Valid")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "BloodGroup reuired ")]
        public string BloodGroup { get; set; }
        [Required(ErrorMessage = "Country reuired ")]
        public Nullable<int> Country { get; set; }
        [Required(ErrorMessage = "State reuired ")]
        public Nullable<int> State { get; set; }
        [Required(ErrorMessage = "City reuired ")]
        public Nullable<int> City { get; set; }

    }


}