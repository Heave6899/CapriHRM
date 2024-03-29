//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer1
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmployeeLeave
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmployeeLeave()
        {
            this.CancleLeaves = new HashSet<CancleLeave>();
            this.LeaveBalances = new HashSet<LeaveBalance>();
        }
    
        public int id { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> LeaveTypeId { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<bool> HalfDay { get; set; }
        public Nullable<int> Shift { get; set; }
        public string Comment { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ApproveBy { get; set; }
        public Nullable<System.DateTime> ApprovalDate { get; set; }
        public Nullable<decimal> NoOfLeave { get; set; }
        public Nullable<bool> ApprovalStatus { get; set; }
        public string year { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CancleLeave> CancleLeaves { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual LeaveType LeaveType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeaveBalance> LeaveBalances { get; set; }
    }
}
