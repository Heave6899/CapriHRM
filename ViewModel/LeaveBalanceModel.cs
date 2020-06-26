using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViewModel
{
    public class LeaveBalanceMasterModel
    {

        public LeaveBalanceModel leaveBalanceModel { get; set; }
        public List<LeaveBalanceModel> LeaveBalanceModels { get; set; }
    }
    public class LeaveBalanceModel
    {

        public int LeaveBalanceid { get; set; }
        [Display(Name = "Employee")]
        [Required(ErrorMessage = "Select Employee")]
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        [Display(Name = "Leave Type")]
        [Required(ErrorMessage = "Select Leave Type")]
        public int? LeaveTypeId { get; set; }
        [Display(Name = "Year")]
        [Required(ErrorMessage = "Select Year")]
        public string Year { get; set; }
        public string LeaveTypename { get; set; }
        [Display(Name = "Leave Balance")]
        [Required(ErrorMessage = "Leave Balance Required.")]
        public decimal? LeavesBalance { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsActive { get; set; }

    }
    public class EmployeeLeaveMasterModel
    {
        public int EmployeeId { get; set; }
        public string Year { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        [Display(Name = "From Date")]
        [Required(ErrorMessage = "Select From Date please")]
        public DateTime? FromDate { get; set; }
        [Display(Name = "To Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        [Required(ErrorMessage = "Select To Date lease")]
        public DateTime? ToDate { get; set; }
        public decimal? NoOfLeave { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> LeaveTypeId { get; set; }
        public bool HalfDay { get; set; }
        public int Shift { get; set; }
        public List<EmployeeLeaveModel> employeeLeaveModels { get; set; }
        public List<EmployeeUnapproveCancleLeaveModel> employeeUnapproveCancleLeaveModels { get; set; }

    }
    public class EmployeeLeaveModel
    {

        public int id { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        [Display(Name = "Leave Type")]
        [Required(ErrorMessage = "Select Leave type please")]
        public int? LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }
        [Display(Name = "Leave Balance")]

        public decimal? LeaveBalance { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "From Date")]
        [Required(ErrorMessage ="Select From Date please")]
        public DateTime? FromDate { get; set; }
        [Required(ErrorMessage ="Select To Date lease")]
        [Display(Name = "To Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ToDate { get; set; }
        [Required(ErrorMessage ="Enter comment please")]
        public string Comment { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ApproveBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
        [Display(Name = "No Of Leaves")]
        public decimal? NoOfLeave { get; set; }
        public string ApprovalStatus { get; set; }
        public bool HalfDay { get; set; }
        public int Shift { get; set; }

    }
    public class UnapproveLeaveModel
    {
        public List<EmployeeUnapproveLeaveModel> employeeUnapproveLeaveModels { get; set; }
        public List<EmployeeUnapproveCancleLeaveModel> employeeUnapproveCancleLeaveModels { get; set; }
    }
    public class EmployeeUnapproveLeaveModel
    {

        public int id { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public Nullable<int> LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }
        public Nullable<int> LeaveBalance { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "From Date")]
        public Nullable<System.DateTime> FromDate { get; set; }
        [Display(Name = "To Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ToDate { get; set; }
        public string Comment { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ApproveBy { get; set; }
        public Nullable<System.DateTime> ApprovalDate { get; set; }
        public decimal? NoOfLeave { get; set; }
        public string ApprovalStatus { get; set; }

    }
    public class EmployeeUnapproveCancleLeaveModel
    {

        public int id { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public Nullable<int> LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }
        public Nullable<int> LeaveBalance { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "From Date")]
        [Required(ErrorMessage ="Select From Date Please")]
        public Nullable<System.DateTime> FromDate { get; set; }
        [Required(ErrorMessage ="Select To Date Please")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "To Date")]
        public Nullable<System.DateTime> ToDate { get; set; }
        [Required]
        public string Comment { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ApproveBy { get; set; }
        public Nullable<System.DateTime> ApprovalDate { get; set; }
        public decimal? NoOfLeave { get; set; }
        public string ApprovalStatus { get; set; }
        public string year { get; set; }
        public int EmployeeLeaveId { get; set; }

    }
    public class EmployeeLeaveDatesModel
    {

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "From Date")]
        public Nullable<System.DateTime> FromDate { get; set; }
        [Display(Name = "To Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ToDate { get; set; }

    }

}