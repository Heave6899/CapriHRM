using DataLayer1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility.Costant;
using ViewModel;

namespace Service
{
    public class LeaveService
    {
        //Save LeaveType 
        public bool saveLeaveType(LeaveTypeMasterModel leaveTypeMasterModel)
        {
            bool IsSaved = false;
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    LeaveType leaveType;
                    //edit
                    if (leaveTypeMasterModel.leaveTypeModel.id != 0)
                    {
                        leaveType = db.LeaveTypes.Where(c => c.id == leaveTypeMasterModel.leaveTypeModel.id).FirstOrDefault();
                        leaveType.UpdatedBy = leaveTypeMasterModel.leaveTypeModel.UpdatedBy;
                        leaveType.UpdatedDate = leaveTypeMasterModel.leaveTypeModel.UpdatedDate;

                    }
                    else
                    {
                        leaveType = new LeaveType();
                        leaveType.CreatedBy = leaveTypeMasterModel.leaveTypeModel.CreatedBy;
                        leaveType.CreatedDate = leaveTypeMasterModel.leaveTypeModel.CreatedDate;
                        db.LeaveTypes.Add(leaveType);

                    }
                    leaveType.LeaveTypeName = leaveTypeMasterModel.leaveTypeModel.LeaveTypeName;
                    leaveType.IsActive = leaveTypeMasterModel.leaveTypeModel.IsActive;
                    int res = db.SaveChanges();
                    if (res > 0)
                    {
                        IsSaved = true;
                    }


                }
            }
            catch (Exception)
            {

                throw;
            }

            return IsSaved;
        }
        //List Of LeaveType
        public List<LeaveTypeModel> GetList()
        {
            List<LeaveTypeModel> result = new List<LeaveTypeModel>();
            LeaveTypeModel leaveTypeModel = new LeaveTypeModel();
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    result = db.LeaveTypes.Select(c => new LeaveTypeModel()
                    {
                        id = c.id,
                        LeaveTypeName = c.LeaveTypeName,
                        IsActive = c.IsActive
                    }).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        //Get LeaveType By Id
        public LeaveTypeModel GetLeaveTypeById(int id)
        {
            LeaveTypeModel leaveTypeModel = new LeaveTypeModel();
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    leaveTypeModel = db.LeaveTypes.Where(c => c.id == id).Select(c => new LeaveTypeModel()
                    {
                        id = c.id,
                        LeaveTypeName = c.LeaveTypeName


                    }).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return leaveTypeModel;
        }
        //Deactive Leave Type
        public bool DeActiveLeaveType(int id)
        {
            bool IsDelete = false;
            try
            {
                using (var db = new CapriHRMEntities())
                {

                    LeaveType leaveType;
                    //role = new Role();
                    leaveType = db.LeaveTypes.Where(c => c.id == id).FirstOrDefault();
                    leaveType.IsActive = false;
                    int res = db.SaveChanges();
                    if (res > 0)
                    {
                        IsDelete = true;
                    }


                }
            }
            catch (Exception)
            {

                throw;
            }

            return IsDelete;
        }
        //Active LeaveType
        public bool ActiveLeaveType(int id)
        {
            bool Active = false;
            try
            {
                using (var db = new CapriHRMEntities())
                {

                    LeaveType leaveType;
                    //role = new Role();
                    leaveType = db.LeaveTypes.Where(c => c.id == id).FirstOrDefault();
                    leaveType.IsActive = true;
                    int res = db.SaveChanges();
                    if (res > 0)
                    {
                        Active = true;
                    }


                }
            }
            catch (Exception)
            {

                throw;
            }

            return Active;
        }
        //List Of LeaveType For Dropdown 
        public List<SelectListItem> GetLeaveType()
        {
            using (var db = new CapriHRMEntities())
            {

                List<SelectListItem> List1 = (from s in db.LeaveTypes
                                              where s.IsActive == true
                                              select new SelectListItem
                                              {
                                                  Text = s.LeaveTypeName,
                                                  Value = s.id.ToString()
                                              }).ToList();

                List1.Insert(0, new SelectListItem { Text = "-- Select Leave Type --", Value = "" });
                return List1;
            }



        }
        //Get All Employee
        public List<SelectListItem> GetEmployee()
        {
            using (var db = new CapriHRMEntities())
            {

                List<SelectListItem> List1 = (from s in db.Employees
                                              select new SelectListItem
                                              {
                                                  Text = s.FristName,
                                                  Value = s.EmployeeId.ToString()
                                              }).ToList();

                List1.Insert(0, new SelectListItem { Text = "All", Value = "0" });
                return List1;
            }



        }
        //List of all Leave Type
        public List<SelectListItem> GetAllLeaveType()
        {
            using (var db = new CapriHRMEntities())
            {

                List<SelectListItem> List1 = (from s in db.LeaveTypes
                                              where s.IsActive == true
                                              select new SelectListItem
                                              {
                                                  Text = s.LeaveTypeName,
                                                  Value = s.id.ToString()
                                              }).ToList();

                List1.Insert(0, new SelectListItem { Text = "All", Value = "0" });
                return List1;
            }



        }
        //List For LeaveBalance
        public List<LeaveBalanceModel> GetListBalance()
        {
            List<LeaveBalanceModel> result = new List<LeaveBalanceModel>();
            LeaveBalanceModel leaveBalance = new LeaveBalanceModel();
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    result = db.LeaveBalances.AsEnumerable().Where(c => c.Year == DateTime.Now.Year.ToString()).GroupBy(n => new { n.LeaveType.LeaveTypeName, n.Employee, n.Year })
                .Select(g => new LeaveBalanceModel()
                {
                    LeaveTypename = g.Key.LeaveTypeName,
                    LeavesBalance = g.Sum(c => Convert.ToDecimal(c.LeavesBalance)),
                    EmployeeName = g.Key.Employee.FristName + " " + g.Key.Employee.MiddleName + " " + g.Key.Employee.LastName,
                    Year = g.Key.Year
                }).ToList();

                    //result = db.LeaveBalances.GroupBy(p => p.LeaveType.LeaveTypeName, p => p.Year
                    //     (key, g)).Select(c => new LeaveBalanceModel()
                    //     {
                    //         LeaveBalanceid = c.id,
                    //         LeaveTypename = c.LeaveType.LeaveTypeName,
                    //         LeavesBalance = c.LeavesBalance,
                    //         EmployeeName = c.Employee.FristName + " " + c.Employee.MiddleName + " " + c.Employee.LastName,
                    //         Year = c.Year
                    //     }).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        //Save LeaveBalance
        public bool saveLeaveBalance(LeaveBalanceMasterModel leaveBalanceMasterModel)
        {
            bool IsSaved = false;
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    LeaveBalance leaveBalance;
                    //edit  
                    if (leaveBalanceMasterModel.leaveBalanceModel.LeaveBalanceid != 0)
                    {
                        leaveBalance = db.LeaveBalances.Where(c => c.id == leaveBalanceMasterModel.leaveBalanceModel.LeaveBalanceid).FirstOrDefault();
                        leaveBalance.UpdatedBy = leaveBalanceMasterModel.leaveBalanceModel.UpdatedBy;
                        leaveBalance.UpdatedDate = leaveBalanceMasterModel.leaveBalanceModel.UpdatedDate;

                    }
                    else
                    {
                        leaveBalance = new LeaveBalance();
                        leaveBalance.CreatedBy = leaveBalanceMasterModel.leaveBalanceModel.CreatedBy;
                        leaveBalance.CreatedDate = leaveBalanceMasterModel.leaveBalanceModel.CreatedDate;
                        db.LeaveBalances.Add(leaveBalance);

                    }
                    leaveBalance.LeavesBalance = leaveBalanceMasterModel.leaveBalanceModel.LeavesBalance;
                    leaveBalance.IsActive = leaveBalanceMasterModel.leaveBalanceModel.IsActive;
                    leaveBalance.LeaveTypeId = leaveBalanceMasterModel.leaveBalanceModel.LeaveTypeId;
                    leaveBalance.EmployeeId = leaveBalanceMasterModel.leaveBalanceModel.EmployeeId;
                    leaveBalance.Year = leaveBalanceMasterModel.leaveBalanceModel.Year;
                    leaveBalance.LeaveBy = Convert.ToByte(Constant.admin);
                    int res = db.SaveChanges();
                    if (res > 0)
                    {
                        IsSaved = true;
                    }


                }
            }
            catch (Exception)
            {

                throw;
            }

            return IsSaved;
        }
        //Get LeaveBalance By Id
        public LeaveBalanceModel GetLeaveBalanceById(int id)
        {
            LeaveBalanceModel leaveTypeModel = new LeaveBalanceModel();
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    leaveTypeModel = db.LeaveBalances.Where(c => c.id == id).Select(c => new LeaveBalanceModel()
                    {
                        LeaveBalanceid = c.id,
                        EmployeeId = c.EmployeeId,
                        LeaveTypeId = c.LeaveTypeId,
                        LeavesBalance = c.LeavesBalance,
                        Year = c.Year
                    }).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return leaveTypeModel;
        }
        //Get no of Leave On Base od Leavetype and Paerticular User
        public decimal getLeaveBalance(int id, int? Uid)
        {
            decimal Leavebalance = 0;

            using (var db = new CapriHRMEntities())
            {

                Leavebalance = db.LeaveBalances.AsEnumerable().Where(c => c.LeaveTypeId == id && c.EmployeeId == Uid && c.Year == DateTime.Now.Year.ToString()).GroupBy(n => new { n.LeaveTypeId, n.EmployeeId })
                       .Select(g => g.Sum(c => Convert.ToDecimal(c.LeavesBalance))).FirstOrDefault();
                //NoOfLeave = db.LeaveBalances.Where(c => c.LeaveTypeId == id && c.EmployeeId == Uid).Select(x => x.LeavesBalance).FirstOrDefault();


                return Leavebalance;
            };

        }
        //Save Employee Leave
        public bool saveEmployeeLeave(EmployeeLeaveModel employeeLeaveModel)
        {

            bool IsSaved = false;
            try
            {
                //if (employeeLeaveModel.LeaveTypeId != Constant.UnpaidLeave)
                //{
                //    using (var db = new CapriHRMEntities())
                //    {
                //        LeaveBalance leaveBalance;
                //        leaveBalance = db.LeaveBalances.Where(c => c.EmployeeId == employeeLeaveModel.EmployeeId && c.LeaveTypeId == employeeLeaveModel.LeaveTypeId).FirstOrDefault();
                //        int? a = Convert.ToInt32(leaveBalance.LeavesBalance.ToString()) - employeeLeaveModel.NoOfLeave;
                //        leaveBalance.LeavesBalance = a.ToString();
                //        db.SaveChanges();
                //    }
                //}

                using (var db = new CapriHRMEntities())
                {

                    string year = db.LeaveBalances.Where(c => c.EmployeeId == employeeLeaveModel.EmployeeId && c.LeaveTypeId == employeeLeaveModel.LeaveTypeId).Select(x => x.Year).FirstOrDefault();
                    EmployeeLeave employeeLeave = new EmployeeLeave();
                    employeeLeave.CreatedBy = employeeLeaveModel.CreatedBy;
                    employeeLeave.CreatedDate = employeeLeaveModel.CreatedDate;
                    employeeLeave.LeaveTypeId = employeeLeaveModel.LeaveTypeId;
                    employeeLeave.EmployeeId = employeeLeaveModel.EmployeeId;
                    employeeLeave.FromDate = employeeLeaveModel.FromDate;
                    employeeLeave.ToDate = employeeLeaveModel.ToDate;
                    employeeLeave.Comment = employeeLeaveModel.Comment;
                    employeeLeave.HalfDay = employeeLeaveModel.HalfDay;
                    employeeLeave.Shift = employeeLeaveModel.Shift;
                    employeeLeave.NoOfLeave = employeeLeaveModel.NoOfLeave;
                    employeeLeave.year = year;
                    db.EmployeeLeaves.Add(employeeLeave);
                    int res = db.SaveChanges();
                    if (res > 0)
                    {
                        IsSaved = true;
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

            return IsSaved;
        }
        //List Of Employee Leaves
        public List<EmployeeLeaveModel> GetMyLeave(int? id)
        {
            List<EmployeeLeaveModel> result = new List<EmployeeLeaveModel>();
            EmployeeLeaveModel employeeLeaveModel = new EmployeeLeaveModel();
            decimal Leavebalance = 0;
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    Leavebalance = db.LeaveBalances.AsEnumerable().Where(c => c.EmployeeId == id && c.Year == DateTime.Now.Year.ToString()).GroupBy(n => new { n.EmployeeId })
                      .Select(g => g.Sum(c => Convert.ToDecimal(c.LeavesBalance))).FirstOrDefault();
                    result = db.EmployeeLeaves.Where(x => x.EmployeeId == id).Select(c => new EmployeeLeaveModel()
                    {
                        id = c.id,
                        LeaveTypeName = c.LeaveType.LeaveTypeName,
                        LeaveBalance = Leavebalance,
                        FromDate = c.FromDate,
                        ToDate = c.ToDate,
                        NoOfLeave = c.NoOfLeave,
                        Comment = c.Comment,
                        ApprovalStatus = c.ApprovalStatus.ToString()
                    }).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        public List<EmployeeLeaveModel> GetMyLeaveFromYear(string Yearid, int? id)
        {
            List<EmployeeLeaveModel> result = new List<EmployeeLeaveModel>();
            EmployeeLeaveModel employeeLeaveModel = new EmployeeLeaveModel();
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    if (Convert.ToInt32(Yearid) == 0)
                    {
                        result = db.EmployeeLeaves.Where(x => x.EmployeeId == id).Select(c => new EmployeeLeaveModel()
                        {
                            id = c.id,
                            LeaveTypeName = c.LeaveType.LeaveTypeName,
                            FromDate = c.FromDate,
                            ToDate = c.ToDate,
                            NoOfLeave = c.NoOfLeave,
                            Comment = c.Comment,
                            ApprovalStatus = c.ApprovalStatus.ToString()
                        }).ToList();
                    }
                    else
                    {
                        result = db.EmployeeLeaves.Where(x => x.EmployeeId == id && x.year == Yearid).Select(c => new EmployeeLeaveModel()
                        {
                            id = c.id,
                            LeaveTypeName = c.LeaveType.LeaveTypeName,
                            FromDate = c.FromDate,
                            ToDate = c.ToDate,
                            NoOfLeave = c.NoOfLeave,
                            Comment = c.Comment,
                            ApprovalStatus = c.ApprovalStatus.ToString()
                        }).ToList();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        // Get Nof of Leaves on base of selection of Fromdate and Todate
        public int? GetNoOfLeave(string FromDate, string ToDate, int? LeavrtypeId, int? empid)
        {
            decimal LeaveBalanceid = 0;
            if (LeavrtypeId == Constant.UnpaidLeave)
            {
                LeaveBalanceid = 0;
            }
            if (LeavrtypeId != Constant.UnpaidLeave)
            {
                using (var db = new CapriHRMEntities())
                {
                    LeaveBalanceid = db.LeaveBalances.AsEnumerable().Where(c => c.LeaveTypeId == LeavrtypeId && c.EmployeeId == empid).GroupBy(n => new { n.LeaveTypeId, n.EmployeeId })
                           .Select(g => g.Sum(c => Convert.ToDecimal(c.LeavesBalance))).FirstOrDefault();

                }
            }

            int? Days = 0;
            if (FromDate.Equals(ToDate) && Convert.ToDecimal(LeaveBalanceid) != 0)
            {
                return Days = 1;
            }
            else
            {
                DateTime dt1 = DateTime.Parse(FromDate);
                DateTime dt2 = DateTime.Parse(ToDate);
                TimeSpan ts = dt2.Subtract(dt1);
                Days = 1 + ts.Days;

                for (var date = dt1; date <= dt2; date = date.AddDays(1))
                {
                    List<weekendmodel> weekendmodels = new List<weekendmodel>();

                    List<HolydayListModel> HolydayListModel = new List<HolydayListModel>();
                    using (var db = new CapriHRMEntities())
                    {



                        weekendmodels = db.WeekendDays.Select(c => new weekendmodel()
                        {
                            Date = c.Date
                        }).ToList();
                        HolydayListModel = db.HolidayLists.Select(c => new HolydayListModel()
                        {
                            FromDate = c.FromDate,
                            ToDate = c.ToDate,
                            NoOfDays = c.NoOfDays,
                        }).ToList();

                    }
                    foreach (var item in weekendmodels)
                    {
                        if (item.Date == date)
                        {
                            Days--;
                        }

                    }
                    foreach (var Holydayitem in HolydayListModel)
                    {
                        if (date >= Holydayitem.FromDate && date <= Holydayitem.ToDate)
                        {
                            for (var Fromdate = Holydayitem.FromDate; Fromdate <= Holydayitem.ToDate; Fromdate = Fromdate.Value.AddDays(1))
                            {
                                if (date == Fromdate)
                                {
                                    Days--;
                                    foreach (var item in weekendmodels)
                                    {
                                        if (item.Date == Fromdate)
                                        {
                                            Days++;
                                        }
                                    }


                                }
                            }
                        }
                    }
                }


                if (Convert.ToInt32(Days) <= Convert.ToInt32(LeaveBalanceid) && LeavrtypeId != Constant.UnpaidLeave)
                {
                    return Days;
                }
                else if (LeavrtypeId == Constant.UnpaidLeave && Convert.ToInt32(LeaveBalanceid) == 0)
                {
                    return Days;
                }
                else
                {
                    Days = 0;
                    return Days;
                }
            }
        }
        public List<EmployeeUnapproveLeaveModel> GetUnApproveLeave()
        {
            List<EmployeeUnapproveLeaveModel> result = new List<EmployeeUnapproveLeaveModel>();

            try
            {
                using (var db = new CapriHRMEntities())
                {
                    result = db.EmployeeLeaves.Where(x => x.ApprovalStatus == null).Select(c => new EmployeeUnapproveLeaveModel()
                    {
                        id = c.id,
                        EmployeeName = c.Employee.FristName + " " + c.Employee.LastName,
                        LeaveTypeName = c.LeaveType.LeaveTypeName,
                        FromDate = c.FromDate,
                        ToDate = c.ToDate,
                        NoOfLeave = c.NoOfLeave,
                        Comment = c.Comment,
                        ApprovalStatus = c.ApprovalStatus.ToString()

                    }).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        public bool ApproveLeave(int id, DateTime ApprovalDate, int ApproveBy)
        {

            bool IsSaved = false;
            try
            {
                EmployeeLeaveModel employeeLeaveModel = new EmployeeLeaveModel();
                using (var db = new CapriHRMEntities())
                {
                    employeeLeaveModel = db.EmployeeLeaves.Where(c => c.id == id).Select(c => new EmployeeLeaveModel
                    {
                        EmployeeId = c.EmployeeId,
                        LeaveTypeId = c.LeaveTypeId,
                        NoOfLeave = c.NoOfLeave
                    }).FirstOrDefault();
                }
                if (employeeLeaveModel.LeaveTypeId == Constant.UnpaidLeave)
                {
                    employeeLeaveModel.NoOfLeave = 0;
                    using (var db = new CapriHRMEntities())
                    {
                        LeaveBalance leaveBalance = new LeaveBalance();
                        leaveBalance.CreatedBy = ApproveBy;
                        leaveBalance.CreatedDate = ApprovalDate;
                        leaveBalance.LeavesBalance = employeeLeaveModel.NoOfLeave;
                        leaveBalance.IsActive = true;
                        leaveBalance.LeaveTypeId = employeeLeaveModel.LeaveTypeId;
                        leaveBalance.EmployeeId = employeeLeaveModel.EmployeeId;
                        leaveBalance.Year = DateTime.Now.Year.ToString();
                        leaveBalance.LeaveBy = Convert.ToByte(Constant.LeaveApprove);
                        leaveBalance.EmployeeLeaveId = id;
                        db.LeaveBalances.Add(leaveBalance);
                        int res = db.SaveChanges();
                    }
                }
                else
                {
                    using (var db = new CapriHRMEntities())
                    {
                        LeaveBalance leaveBalance = new LeaveBalance();
                        leaveBalance.CreatedBy = ApproveBy;
                        leaveBalance.CreatedDate = ApprovalDate;
                        leaveBalance.LeavesBalance = Convert.ToDecimal("-" + employeeLeaveModel.NoOfLeave.ToString());
                        leaveBalance.IsActive = true;
                        leaveBalance.LeaveTypeId = employeeLeaveModel.LeaveTypeId;
                        leaveBalance.EmployeeId = employeeLeaveModel.EmployeeId;
                        leaveBalance.Year = DateTime.Now.Year.ToString();
                        leaveBalance.LeaveBy = Convert.ToByte(Constant.LeaveApprove);
                        leaveBalance.EmployeeLeaveId = id;
                        db.LeaveBalances.Add(leaveBalance);
                        int res = db.SaveChanges();
                    }
                }

                using (var db = new CapriHRMEntities())
                {
                    EmployeeLeave employeeLeave;
                    employeeLeave = db.EmployeeLeaves.Where(c => c.id == id).FirstOrDefault();
                    employeeLeave.ApprovalStatus = true;
                    employeeLeave.ApprovalDate = ApprovalDate;
                    employeeLeave.ApproveBy = ApproveBy;
                    int res = db.SaveChanges();
                    if (res > 0)
                    {
                        IsSaved = true;
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

            return IsSaved;
        }
        public bool NotApproveLeave(int id)
        {

            bool IsSaved = false;
            try
            {
                EmployeeLeaveModel employeeLeaveModel = new EmployeeLeaveModel();
                using (var db = new CapriHRMEntities())
                {
                    EmployeeLeave employeeLeave;
                    employeeLeave = db.EmployeeLeaves.Where(c => c.id == id).FirstOrDefault();
                    employeeLeave.ApprovalStatus = false;
                    int res = db.SaveChanges();
                    if (res > 0)
                    {
                        IsSaved = true;
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

            return IsSaved;
        }
        public bool DeleteLeave(int id)
        {
            bool IsSaved = false;
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    var delete = (from obj in db.EmployeeLeaves where obj.id == id select obj).First();
                    db.EmployeeLeaves.Remove(delete);
                    int res = db.SaveChanges();
                    if (res > 0)
                    {
                        IsSaved = true;
                    }


                }
            }
            catch (Exception)
            {

                throw;
            }

            return IsSaved;
        }
        //group by
        public List<LeaveBalanceModel> FindLeavebalance(int? employeeid, int? leavetypeid, string year)
        {
            List<LeaveBalanceModel> result = new List<LeaveBalanceModel>();
            LeaveBalanceModel leaveBalance = new LeaveBalanceModel();
            try
            {
                if (employeeid == 0 || leavetypeid == 0)
                {
                    if (employeeid != 0 && leavetypeid == 0)
                    {
                        using (var db = new CapriHRMEntities())
                        {

                            result = db.LeaveBalances.AsEnumerable().Where(c => c.Year == year && c.EmployeeId == employeeid).GroupBy(n => new { n.LeaveType.LeaveTypeName, n.Employee, n.Year })
                       .Select(g => new LeaveBalanceModel()
                       {
                           LeaveTypename = g.Key.LeaveTypeName,
                           LeavesBalance = g.Sum(c => Convert.ToDecimal(c.LeavesBalance)),
                           EmployeeName = g.Key.Employee.FristName + " " + g.Key.Employee.MiddleName + " " + g.Key.Employee.LastName,
                           Year = g.Key.Year
                       }).ToList();
                        }
                    }
                    else if (employeeid == 0 && leavetypeid != 0)
                    {
                        using (var db = new CapriHRMEntities())
                        {

                            result = db.LeaveBalances.AsEnumerable().Where(c => c.Year == year && c.LeaveTypeId == leavetypeid).GroupBy(n => new { n.LeaveType.LeaveTypeName, n.Employee, n.Year })
                       .Select(g => new LeaveBalanceModel()
                       {
                           LeaveTypename = g.Key.LeaveTypeName,
                           LeavesBalance = g.Sum(c => Convert.ToDecimal(c.LeavesBalance)),
                           EmployeeName = g.Key.Employee.FristName + " " + g.Key.Employee.MiddleName + " " + g.Key.Employee.LastName,
                           Year = g.Key.Year
                       }).ToList();
                        }
                    }
                    else
                    {
                        using (var db = new CapriHRMEntities())
                        {

                            result = db.LeaveBalances.AsEnumerable().Where(c => c.Year == year).GroupBy(n => new { n.LeaveType.LeaveTypeName, n.Employee, n.Year })
                       .Select(g => new LeaveBalanceModel()
                       {
                           LeaveTypename = g.Key.LeaveTypeName,
                           LeavesBalance = g.Sum(c => Convert.ToDecimal(c.LeavesBalance)),
                           EmployeeName = g.Key.Employee.FristName + " " + g.Key.Employee.MiddleName + " " + g.Key.Employee.LastName,
                           Year = g.Key.Year
                       }).ToList();
                        }
                    }
                }
                else
                {
                    using (var db = new CapriHRMEntities())
                    {

                        result = db.LeaveBalances.AsEnumerable().Where(c => c.Year == year && c.LeaveTypeId == leavetypeid && c.EmployeeId == employeeid).GroupBy(n => new { n.LeaveType.LeaveTypeName, n.Employee, n.Year })
                       .Select(g => new LeaveBalanceModel()
                       {
                           LeaveTypename = g.Key.LeaveTypeName,
                           LeavesBalance = g.Sum(c => Convert.ToDecimal(c.LeavesBalance)),
                           EmployeeName = g.Key.Employee.FristName + " " + g.Key.Employee.MiddleName + " " + g.Key.Employee.LastName,
                           Year = g.Key.Year
                       }).ToList();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        public int? GetNoOfLeaves(string FromDate, string ToDate)
        {
            int? Days = 0;
            List<weekendmodel> weekendmodels = new List<weekendmodel>();
            List<HolydayListModel> HolydayListModel = new List<HolydayListModel>();
            using (var db = new CapriHRMEntities())
            {

                weekendmodels = db.WeekendDays.Select(c => new weekendmodel()
                {
                    Date = c.Date
                }).ToList();
                HolydayListModel = db.HolidayLists.Select(c => new HolydayListModel()
                {
                    FromDate = c.FromDate,
                    ToDate = c.ToDate,
                    NoOfDays = c.NoOfDays,
                }).ToList();

            }
            if (FromDate.Equals(ToDate))
            {
                Days = 1;
                foreach (var item in weekendmodels)
                {
                    if (item.Date == DateTime.Parse(FromDate))
                    {
                        Days--;
                    }

                }
                return Days;
            }
            else
            {
                DateTime dt1 = DateTime.Parse(FromDate);
                DateTime dt2 = DateTime.Parse(ToDate);
                TimeSpan ts = dt2.Subtract(dt1);
                Days = 1 + ts.Days;

                for (var date = dt1; date < dt2; date = date.AddDays(1))
                {

                    foreach (var item in weekendmodels)
                    {
                        if (item.Date == date)
                        {
                            Days--;
                        }

                    }

                }

                return Days;
            }
        }
        public int? GetNoOfCancleLeaves(string FromDate, string ToDate)
        {
            int? Days = 0;
            List<weekendmodel> weekendmodels = new List<weekendmodel>();
            List<HolydayListModel> HolydayListModel = new List<HolydayListModel>();
            using (var db = new CapriHRMEntities())
            {

                weekendmodels = db.WeekendDays.Select(c => new weekendmodel()
                {
                    Date = c.Date
                }).ToList();
                HolydayListModel = db.HolidayLists.Select(c => new HolydayListModel()
                {
                    FromDate = c.FromDate,
                    ToDate = c.ToDate,
                    NoOfDays = c.NoOfDays,
                }).ToList();

            }
            if (FromDate.Equals(ToDate))
            {
                Days = 1;
                foreach (var item in weekendmodels)
                {
                    if (item.Date == DateTime.Parse(FromDate))
                    {
                        Days--;
                    }

                }
                foreach (var Holydayitem in HolydayListModel)
                {
                    if (DateTime.Parse(FromDate) >= Holydayitem.FromDate && DateTime.Parse(FromDate) <= Holydayitem.ToDate)
                    {
                        for (var Fromdate = Holydayitem.FromDate; Fromdate <= Holydayitem.ToDate; Fromdate = Fromdate.Value.AddDays(1))
                        {

                            if (DateTime.Parse(FromDate) == Fromdate)
                            {
                                Days--;
                            }

                        }
                    }
                }
                return Days;
            }
            else
            {
                DateTime dt1 = DateTime.Parse(FromDate);
                DateTime dt2 = DateTime.Parse(ToDate);
                TimeSpan ts = dt2.Subtract(dt1);
                Days = 1 + ts.Days;

                for (var date = dt1; date <= dt2; date = date.AddDays(1))
                {

                    foreach (var item in weekendmodels)
                    {
                        if (item.Date == date)
                        {
                            Days--;
                        }

                    }
                    foreach (var Holydayitem in HolydayListModel)
                    {
                        if (date >= Holydayitem.FromDate && date <= Holydayitem.ToDate)
                        {
                            for (var Fromdate = Holydayitem.FromDate; Fromdate <= Holydayitem.ToDate; Fromdate = Fromdate.Value.AddDays(1))
                            {

                                if (date == Fromdate)
                                {
                                    Days--;
                                    foreach (var item in weekendmodels)
                                    {
                                        if (item.Date == Fromdate)
                                        {
                                            Days++;
                                        }
                                    }

                                }

                            }
                        }
                    }

                }

                return Days;
            }
        }
        public bool saveEmployeeCancleLeave(EmployeeLeaveMasterModel employeeLeaveMasterModel)
        {

            bool IsSaved = false;
            try
            {
                using (var db = new CapriHRMEntities())
                {

                    EmployeeLeave employeeLeave;
                    employeeLeave = db.EmployeeLeaves.Where(c => c.id == employeeLeaveMasterModel.LeaveTypeId).FirstOrDefault();

                    CancleLeave cancleLeave = new CancleLeave();
                    cancleLeave.employeeLeaveId = employeeLeaveMasterModel.LeaveTypeId;
                    cancleLeave.createby = employeeLeaveMasterModel.CreatedBy;
                    cancleLeave.createdate = employeeLeaveMasterModel.CreatedDate;
                    cancleLeave.noOfLeave = employeeLeaveMasterModel.NoOfLeave;
                    cancleLeave.todate = employeeLeaveMasterModel.ToDate;
                    cancleLeave.fromdate = employeeLeaveMasterModel.FromDate;
                    cancleLeave.HalfDay = employeeLeaveMasterModel.HalfDay;
                    cancleLeave.Shift = employeeLeaveMasterModel.Shift;
                    cancleLeave.comment = employeeLeave.Comment;
                    cancleLeave.year = employeeLeave.year;
                    db.CancleLeaves.Add(cancleLeave);
                    int res = db.SaveChanges();
                    if (res > 0)
                    {
                        IsSaved = true;
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

            return IsSaved;
        }
        public bool NotApproveCancleLeave(int id)
        {

            bool IsSaved = false;
            try
            {

                using (var db = new CapriHRMEntities())
                {
                    CancleLeave cancleLeave;
                    cancleLeave = db.CancleLeaves.Where(c => c.id == id).FirstOrDefault();
                    cancleLeave.approveStatus = false;
                    int res = db.SaveChanges();
                    if (res > 0)
                    {
                        IsSaved = true;
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

            return IsSaved;
        }
        public List<EmployeeUnapproveCancleLeaveModel> GetUnApproveCancleLeave()
        {
            List<EmployeeUnapproveCancleLeaveModel> result = new List<EmployeeUnapproveCancleLeaveModel>();

            try
            {
                using (var db = new CapriHRMEntities())
                {
                    result = db.CancleLeaves.Where(x => x.approveStatus == null).Select(c => new EmployeeUnapproveCancleLeaveModel()
                    {
                        id = c.id,
                        EmployeeName = c.EmployeeLeave.Employee.FristName + " " + c.EmployeeLeave.Employee.LastName,
                        LeaveTypeName = c.EmployeeLeave.LeaveType.LeaveTypeName,
                        FromDate = c.fromdate,
                        ToDate = c.todate,
                        NoOfLeave = c.noOfLeave,
                        Comment = c.comment,
                        ApprovalStatus = c.approveStatus.ToString()

                    }).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        public List<EmployeeLeaveDatesModel> GetEmployeesDateByLeaveId(int? id)
        {
            List<EmployeeLeaveDatesModel> result = new List<EmployeeLeaveDatesModel>();

            try
            {
                using (var db = new CapriHRMEntities())
                {
                    result = db.CancleLeaves.Where(x => x.employeeLeaveId == id).Select(c => new EmployeeLeaveDatesModel()
                    {
                        FromDate = c.fromdate,
                        ToDate = c.todate,
                    }).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        public List<EmployeeUnapproveCancleLeaveModel> GetCancleLeaveById(int? id)
        {
            List<EmployeeUnapproveCancleLeaveModel> result = new List<EmployeeUnapproveCancleLeaveModel>();

            try
            {
                using (var db = new CapriHRMEntities())
                {
                    result = db.CancleLeaves.Where(x => x.EmployeeLeave.Employee.EmployeeId == id).Select(c => new EmployeeUnapproveCancleLeaveModel()
                    {
                        id = c.id,
                        LeaveTypeName = c.EmployeeLeave.LeaveType.LeaveTypeName,
                        ApprovalStatus = c.approveStatus.ToString(),
                        FromDate = c.fromdate,
                        ToDate = c.todate,
                        NoOfLeave = c.noOfLeave,
                        Comment = c.comment,
                    }).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        public bool CancleApproveLeave(int id, DateTime ApprovalDate, int ApproveBy)
        {

            bool IsSaved = false;
            try
            {
                EmployeeUnapproveCancleLeaveModel employeeUnapproveCancleLeaveModel = new EmployeeUnapproveCancleLeaveModel();
                using (var db = new CapriHRMEntities())
                {
                    employeeUnapproveCancleLeaveModel = db.CancleLeaves.Where(c => c.id == id).Select(c => new EmployeeUnapproveCancleLeaveModel
                    {
                        EmployeeId = c.EmployeeLeave.Employee.EmployeeId,
                        LeaveTypeId = c.EmployeeLeave.LeaveTypeId,
                        NoOfLeave = c.noOfLeave,
                        year = c.year
                    }).FirstOrDefault();
                }

                if (employeeUnapproveCancleLeaveModel.LeaveTypeId == Constant.UnpaidLeave)
                {
                    employeeUnapproveCancleLeaveModel.NoOfLeave = 0;
                    using (var db = new CapriHRMEntities())
                    {
                        LeaveBalance leaveBalance = new LeaveBalance();
                        leaveBalance.CreatedBy = ApproveBy;
                        leaveBalance.CreatedDate = ApprovalDate;
                        leaveBalance.LeavesBalance = employeeUnapproveCancleLeaveModel.NoOfLeave;
                        leaveBalance.IsActive = true;
                        leaveBalance.LeaveTypeId = employeeUnapproveCancleLeaveModel.LeaveTypeId;
                        leaveBalance.EmployeeId = employeeUnapproveCancleLeaveModel.EmployeeId;
                        leaveBalance.Year = employeeUnapproveCancleLeaveModel.year;
                        leaveBalance.LeaveBy = Convert.ToByte(Constant.CancleLeave);
                        leaveBalance.CancleLeaveId = id;
                        db.LeaveBalances.Add(leaveBalance);
                        int res = db.SaveChanges();
                    }

                }
                else
                {
                    using (var db = new CapriHRMEntities())
                    {
                        LeaveBalance leaveBalance = new LeaveBalance();
                        leaveBalance.CreatedBy = ApproveBy;
                        leaveBalance.CreatedDate = ApprovalDate;
                        leaveBalance.LeavesBalance = employeeUnapproveCancleLeaveModel.NoOfLeave;
                        leaveBalance.IsActive = true;
                        leaveBalance.LeaveTypeId = employeeUnapproveCancleLeaveModel.LeaveTypeId;
                        leaveBalance.EmployeeId = employeeUnapproveCancleLeaveModel.EmployeeId;
                        leaveBalance.Year = employeeUnapproveCancleLeaveModel.year;
                        leaveBalance.LeaveBy = Convert.ToByte(Constant.CancleLeave);
                        leaveBalance.CancleLeaveId = id;
                        db.LeaveBalances.Add(leaveBalance);
                        int res = db.SaveChanges();
                    }
                }
                using (var db = new CapriHRMEntities())
                {
                    CancleLeave cancleLeave;
                    cancleLeave = db.CancleLeaves.Where(c => c.id == id).FirstOrDefault();
                    cancleLeave.approveStatus = true;
                    cancleLeave.approvedate = ApprovalDate;
                    cancleLeave.approveby = ApproveBy;
                    int res = db.SaveChanges();
                    if (res > 0)
                    {
                        IsSaved = true;
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

            return IsSaved;
        }
        public bool DeleteCancleLeave(int id)
        {
            bool IsSaved = false;
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    var delete = (from obj in db.CancleLeaves where obj.id == id select obj).First();
                    db.CancleLeaves.Remove(delete);
                    int res = db.SaveChanges();
                    if (res > 0)
                    {
                        IsSaved = true;
                    }


                }
            }
            catch (Exception)
            {

                throw;
            }

            return IsSaved;
        }
        public List<EmployeeLeaveDatesModel> CheckEmployeesDateByLeaveId(int? id)
        {
            List<EmployeeLeaveDatesModel> result = new List<EmployeeLeaveDatesModel>();

            try
            {
                using (var db = new CapriHRMEntities())
                {
                    result = db.EmployeeLeaves.Where(x => x.id == id).Select(c => new EmployeeLeaveDatesModel()
                    {
                        FromDate = c.FromDate,
                        ToDate = c.ToDate,
                    }).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        public bool saveHolyday(HolydayListModel holydayListModel)
        {

            bool IsSaved = false;
            try
            {
                using (var db = new CapriHRMEntities())
                {


                    HolidayList holiday = new HolidayList();
                    holiday.HolydayName = holydayListModel.HolydayName;
                    holiday.Description = holydayListModel.Description;
                    holiday.FromDate = holydayListModel.FromDate;
                    holiday.ToDate = holydayListModel.ToDate;
                    holiday.NoOfDays = holydayListModel.NoOfDays;
                    holiday.year = holydayListModel.FromDate.Value.Year.ToString();
                    holiday.CreateBy = holydayListModel.CreateBy;
                    holiday.CreateDate = holydayListModel.CreateDate;
                    db.HolidayLists.Add(holiday);
                    int res = db.SaveChanges();
                    if (res > 0)
                    {
                        IsSaved = true;
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

            return IsSaved;
        }
        public List<HolydayListModel> GetHolydayList()
        {
            List<HolydayListModel> result = new List<HolydayListModel>();
            HolydayListModel holydayListModel = new HolydayListModel();
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    result = db.HolidayLists.Where(c => c.year == DateTime.Now.Year.ToString()).Select(g => new HolydayListModel()
                    {
                        id = g.id,
                        HolydayName = g.HolydayName,
                        Description = g.Description,
                        FromDate = g.FromDate,
                        ToDate = g.ToDate,
                        NoOfDays = g.NoOfDays,
                        year = g.year,
                    }).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        public List<HolydayListModel> GetAllHolydayList()
        {
            List<HolydayListModel> result = new List<HolydayListModel>();
            HolydayListModel holydayListModel = new HolydayListModel();
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    result = db.HolidayLists.Select(g => new HolydayListModel()
                    {
                        id = g.id,
                        HolydayName = g.HolydayName,
                        Description = g.Description,
                        FromDate = g.FromDate,
                        ToDate = g.ToDate,
                        NoOfDays = g.NoOfDays,
                        year = g.year,
                    }).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        public List<HolydayListModel> GetHolydayListByYear(string year)
        {
            List<HolydayListModel> result = new List<HolydayListModel>();
            HolydayListModel holydayListModel = new HolydayListModel();
            try
            {
                if (year == "0")
                {
                    using (var db = new CapriHRMEntities())
                    {
                        result = db.HolidayLists.Select(g => new HolydayListModel()
                        {
                            id = g.id,
                            HolydayName = g.HolydayName,
                            Description = g.Description,
                            FromDate = g.FromDate,
                            ToDate = g.ToDate,
                            NoOfDays = g.NoOfDays,
                            year = g.year,
                        }).ToList();
                    }
                }
                else
                {

                    using (var db = new CapriHRMEntities())
                    {
                        result = db.HolidayLists.Where(c => c.year == year).Select(g => new HolydayListModel()
                        {
                            id = g.id,
                            HolydayName = g.HolydayName,
                            Description = g.Description,
                            FromDate = g.FromDate,
                            ToDate = g.ToDate,
                            NoOfDays = g.NoOfDays,
                            year = g.year,
                        }).ToList();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        public List<alternativedayModel> getalternativeday()
        {
            List<alternativedayModel> List1 = new List<alternativedayModel>
            {
                 new alternativedayModel{value = 1, text = "First", cheked = false},
                 new alternativedayModel{value = 2, text = "Second", cheked = false},
                 new alternativedayModel{value = 3, text = "Third", cheked = false},
                 new alternativedayModel{value = 4, text = "Fourth", cheked = false},
                 new alternativedayModel{value = 5, text = "Fifth", cheked = false},


            };
            return List1;

        }
        public bool GetDates(int year, int day, string type, List<alternativedayModel> alternativedays, DateTime approvadates, int approveby)
        {
            bool isvalid = true;
            string Weekday = ((DayOfWeek)day).ToString();
            string addedday = "";
            try
            {

                using (var db = new CapriHRMEntities())
                {

                    addedday = db.WeekendDays.Where(c => c.Day == Weekday && c.Year == year.ToString()).Select(x => x.Day).FirstOrDefault();

                }
            }
            catch (Exception)
            {

                throw;
            }
            if (addedday == "" || addedday == null)
            {

                var selectedDays = alternativedays.Where(c => c.cheked == true).Select(c => c.value).ToList();
                DateTime startdate = new DateTime(year, 1, 1);
                DateTime enddate = new DateTime(year, 12, 31);
                return GetDatesBetween(startdate, enddate, day, type, selectedDays, approvadates, approveby);

            }
            else
            {
                return isvalid = false;
            }
        }
        public bool GetDatesBetween(DateTime startDate, DateTime endDate, int day, string type, List<int> selectedDays, DateTime approvadates, int approveby)
        {
            bool isValid = false;
            List<DateTime> dateTimes = new List<DateTime>();
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                dateTimes.Add(date);

            List<DateTime> dates = new List<DateTime>();
            for (int i = 0; i <= dateTimes.Count() - 1; i++)
            {
                DateTime date = dateTimes[i];

                if (date.DayOfWeek == (DayOfWeek)day)
                {
                    dates.Add(date);
                }

            }
            if (type == "alternative")
            {

                var result = dates.GroupBy(x => x.Month)
                    .SelectMany(grp =>
                        grp.Select((d, counter) => new
                        {
                            Month = grp.Key,
                            PosInMonth = counter + 1,
                            Day = d
                        }))
                    .Where(x => selectedDays.Contains(x.PosInMonth))
                    .ToList();
                try
                {
                    using (var db = new CapriHRMEntities())
                    {
                        foreach (var item in result)
                        {
                            WeekendDay weekendDay = new WeekendDay();
                            weekendDay.Date = item.Day.Date;
                            weekendDay.Day = item.Day.Date.DayOfWeek.ToString();
                            weekendDay.DayOfMonth = item.Day.Day.ToString();
                            weekendDay.Year = item.Day.Year.ToString();
                            weekendDay.CreateDate = approvadates;
                            weekendDay.CreateBy = approveby;
                            db.WeekendDays.Add(weekendDay);
                            int res = db.SaveChanges();
                            if (res > 0)
                            {
                                isValid = true;
                            }
                        }

                    }
                }
                catch (Exception)
                {

                    throw;
                }

                return isValid;
            }
            else
            {
                try
                {
                    using (var db = new CapriHRMEntities())
                    {
                        foreach (var item in dates)
                        {

                            WeekendDay weekendDay = new WeekendDay();
                            weekendDay.Date = item.Date;
                            weekendDay.Day = item.Date.DayOfWeek.ToString();
                            weekendDay.DayOfMonth = item.Day.ToString();
                            weekendDay.Year = item.Date.Year.ToString();
                            weekendDay.CreateDate = approvadates;
                            weekendDay.CreateBy = approveby;
                            db.WeekendDays.Add(weekendDay);
                            int res = db.SaveChanges();
                            if (res > 0)
                            {
                                isValid = true;
                            }
                        }

                    }
                }
                catch (Exception)
                {

                    throw;
                }


                return isValid;


            }

        }

        public List<weekendmodel> GetWeekend()
        {
            List<weekendmodel> result = new List<weekendmodel>();
            weekendmodel weekendmodel = new weekendmodel();
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    result = db.WeekendDays.Select(g => new weekendmodel()
                    {
                        id = g.id,
                        Date = g.Date,
                        Year = g.Year,
                        Weekday = g.Day,

                    }).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
    }
}