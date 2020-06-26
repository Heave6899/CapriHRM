using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViewModel
{
    public class UserProfileModel
    {
        public int EmployeeId { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [Display(Name = "Nationality")]
        public string NationalityName { get; set; }
        [Display(Name = "Nationality")]
        public int? Nationality { get; set; }
        public int? UpdateBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }
        [Display(Name = "Pancard No")]
        public string PanCardNo { get; set; }
        [Display(Name = "Martial Status")]
        public string MaritalStatus { get; set; }
        [Display(Name = "Profile Image")]
        [Required(ErrorMessage = "Please select file.")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$", ErrorMessage = "Only Image files allowed.")]
        public string ProfileImage { get; set; }
        public string ServerMapPathImage { get; set; }
        public bool? IsActive { get; set; }
        public UserProfilePersonalModel userProfilePersonalModel { get; set; }
    }
    public class UserProfilePersonalModel
    {
        [Display(Name = "Zip code")]
        public string ZipCode { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email-id Not Valid")]
        [Display(Name = "Email Id")]
        public string EmailId { get; set; }
        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }
        [Display(Name = "Country")]
        public string CountryName { get; set; }
        [Display(Name = "State")]
        public string StateName { get; set; }
        [Display(Name = "City")]
        public string CityName { get; set; }
        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country reuired ")]
        public int? Country { get; set; }
        [Display(Name = "State")]
        [Required(ErrorMessage = "State reuired ")]
        public int? State { get; set; }
        [Display(Name = "City")]
        [Required(ErrorMessage = "City reuired ")]
        public int? City { get; set; }
    }


}
