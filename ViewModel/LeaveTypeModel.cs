using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViewModel
{
    public class LeaveTypeMasterModel
    {

        public LeaveTypeModel leaveTypeModel { get; set; }
        public List<LeaveTypeModel> leaveTypeModels { get; set; }
    }
    public class LeaveTypeModel
    {
        public int id { get; set; }
        [Display(Name = "Leave Type")]
        [Required(ErrorMessage ="Leave Type is Required")]
        public string LeaveTypeName { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
    }
    public class HolidayListMasterModel
    {

        public string WeekdaySaturday { get; set; }
        public string HolydayYear { get; set; }
        public bool  day { get; set; }

        public HolydayListModel HolydayListModel { get; set; }
        public List<HolydayListModel> holydayListModels { get; set; }
        public List<alternativedayModel> alternativedays { get; set; }
        public List<weekendmodel> weekendmodels { get; set; }
    }
    public class HolydayListModel
    {
        public int id { get; set; }
        [Display(Name = "Year")]
        
        public string year { get; set; }
        [Display(Name = "Holyday Name")]
        [Required(ErrorMessage = "Holyday name is required")]
        public string HolydayName { get; set; }
        [Display(Name = "Holyday Description")]
        [Required(ErrorMessage = "Holyday description is required")]
        public string Description { get; set; }
        [Display(Name = "From Date")]
        [Required(ErrorMessage = "From date is required")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FromDate { get; set; }
        [Display(Name = "To Date")]
        [Required(ErrorMessage = "To date is required")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ToDate { get; set; }
        [Display(Name = "No of days")]
        public decimal? NoOfDays { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
    }
    public class alternativedayModel
    {
        public int value { get; set; }
        public string text { get; set; }
        public bool cheked { get; set; }
    }
    public class weekendmodel
    {
        public int id { get; set; }
        public DateTime?  Date { get; set; }
        public string Weekday { get; set; }
        public string Year { get; set; }

    }


}