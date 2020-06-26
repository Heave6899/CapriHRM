using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;
using Service;
using TestCapriHRM.Helper;
using Utility.ActionFilter;
using Utility.Costant;

namespace TestCapriHRM.Controllers
{
    [CoustomAuthenticationFilter]
    public class LeaveController : Controller
    {
        // GET: Leave
        LeaveService leaveService = new LeaveService();
        UserService userService = new UserService();
        MasterService masterService = new MasterService();
        public ActionResult LeaveType()
        {
            LeaveTypeMasterModel leaveTypeMasterModel = new LeaveTypeMasterModel();
            leaveTypeMasterModel.leaveTypeModels = leaveService.GetList();
            return View(leaveTypeMasterModel);
        }
        [HRMAuthorization]
        [HttpPost]
        public ActionResult LeaveType(LeaveTypeMasterModel leaveTypeMasterModel)
        {

            if (ModelState.ContainsKey("leaveTypeModel.id"))
                ModelState["leaveTypeModel.id"].Errors.Clear();

            if (ModelState.IsValid)
            {
                var uid = Session["UserID"].ToString();
                if (leaveTypeMasterModel.leaveTypeModel.id == 0)
                {
                    leaveTypeMasterModel.leaveTypeModel.CreatedBy = int.Parse(uid);
                    leaveTypeMasterModel.leaveTypeModel.CreatedDate = DateTime.Now.Date;
                }
                if (leaveTypeMasterModel.leaveTypeModel.id != 0)
                {
                    leaveTypeMasterModel.leaveTypeModel.UpdatedBy = int.Parse(Session["UserID"].ToString());
                    leaveTypeMasterModel.leaveTypeModel.UpdatedDate = DateTime.Now.Date;
                }
                leaveTypeMasterModel.leaveTypeModel.IsActive = true;
                leaveService.saveLeaveType(leaveTypeMasterModel);
                return RedirectToAction("LeaveType", "Leave", new { @id = 0 });
            }
            else
            {
                return RedirectToAction("LeaveType", "Leave");
            }
            //return View();
        }
        public ActionResult ActiveLeaveType(string id)
        {
            if (id != "0")
            {
                int decodeid = Convert.ToInt32(Constant.Decode(id));
                if (leaveService.ActiveLeaveType(decodeid))
                {
                    return RedirectToAction("LeaveType", "Leave");
                }
                else
                {
                    return RedirectToAction("LeaveType", "Leave");
                }
            }

            return RedirectToAction("LeaveType", "LeaveType");
        }
        public ActionResult DeleteLeaveType(string id)
        {
            if (id != "0")
            {
                int decodid = Convert.ToInt32(Constant.Decode(id));
                leaveService.DeActiveLeaveType(decodid);
                return RedirectToAction("LeaveType", "Leave");
            }

            return RedirectToAction("LeaveType", "LeaveType");
        }
        public ActionResult DeleteLeaveApproval(string id)
        {
            if (id != "0")
            {
                int decodeid = Convert.ToInt32(Constant.Decode(id));
                leaveService.DeleteLeave(decodeid);
                return RedirectToAction("MyLeave", "Leave");
            }

            return RedirectToAction("MyLeave", "Leave");
        }
        public ActionResult ListLeaveBalance()
        {
            List<SelectListItem> Employee = leaveService.GetEmployee();
            List<SelectListItem> LeaveTypeId = leaveService.GetAllLeaveType();
            List<SelectListItem> Year = masterService.AllYear(Constant.fromYear, Constant.toYear);
            ViewBag.LeaveTypeId = LeaveTypeId;
            ViewBag.Employee = Employee;
            ViewBag.Year = Year;
            LeaveBalanceMasterModel leaveBalanceMasterModel = new LeaveBalanceMasterModel();
            LeaveBalanceModel leaveBalanceModel = new LeaveBalanceModel();
            leaveBalanceModel.Year = DateTime.Now.Year.ToString();
            leaveBalanceMasterModel.leaveBalanceModel = leaveBalanceModel;
            leaveBalanceMasterModel.LeaveBalanceModels = leaveService.GetListBalance();
            return View(leaveBalanceMasterModel);
        }
        [HttpPost]
        public ActionResult ListLeaveBalance(LeaveBalanceMasterModel leaveBalanceMasterModels)
        {
            LeaveBalanceMasterModel leaveBalanceMasterModel = new LeaveBalanceMasterModel();
            leaveBalanceMasterModel.LeaveBalanceModels = leaveService.FindLeavebalance(leaveBalanceMasterModels.leaveBalanceModel.EmployeeId, leaveBalanceMasterModels.leaveBalanceModel.LeaveTypeId, leaveBalanceMasterModels.leaveBalanceModel.Year);
            List<SelectListItem> Employee = leaveService.GetEmployee();
            List<SelectListItem> LeaveTypeId = leaveService.GetAllLeaveType();
            List<SelectListItem> Year = masterService.AllYear(Constant.fromYear, Constant.toYear);
            ViewBag.LeaveTypeId = LeaveTypeId;
            ViewBag.Employee = Employee;
            ViewBag.Year = Year;
            return View(leaveBalanceMasterModel);
        }
        public ActionResult LeaveBalance(string id = "0")
        {
            List<SelectListItem> Employee = userService.GetEmployee();
            List<SelectListItem> LeaveTypeId = leaveService.GetLeaveType();
            List<SelectListItem> Year = masterService.Year(Constant.fromYear, Constant.toYear);
            ViewBag.LeaveTypeId = LeaveTypeId;
            ViewBag.Employee = Employee;
            ViewBag.Year = Year;
            LeaveBalanceMasterModel leaveBalanceMasterModel = new LeaveBalanceMasterModel();
            leaveBalanceMasterModel.LeaveBalanceModels = leaveService.GetListBalance();
            if (id != "0")
            {
                int decodeid = Convert.ToInt32(Constant.Decode(id));
                leaveBalanceMasterModel.leaveBalanceModel = leaveService.GetLeaveBalanceById(decodeid);

            }
            return View(leaveBalanceMasterModel);
        }
        [HttpPost]
        public ActionResult LeaveBalance(LeaveBalanceMasterModel leaveBalanceMasterModel)
        {
            if (ModelState.ContainsKey("leaveBalanceModel.LeaveBalanceid"))
                ModelState["leaveBalanceModel.LeaveBalanceid"].Errors.Clear();

            if (ModelState.IsValid)
            {
                var uid = Session["UserID"].ToString();
                if (leaveBalanceMasterModel.leaveBalanceModel.LeaveBalanceid == 0)
                {
                    leaveBalanceMasterModel.leaveBalanceModel.CreatedBy = int.Parse(uid);
                    leaveBalanceMasterModel.leaveBalanceModel.CreatedDate = DateTime.Now.Date;
                }
                if (leaveBalanceMasterModel.leaveBalanceModel.LeaveBalanceid != 0)
                {
                    leaveBalanceMasterModel.leaveBalanceModel.UpdatedBy = int.Parse(Session["UserID"].ToString());
                    leaveBalanceMasterModel.leaveBalanceModel.UpdatedDate = DateTime.Now.Date;
                }
                leaveBalanceMasterModel.leaveBalanceModel.IsActive = true;
                if (leaveService.saveLeaveBalance(leaveBalanceMasterModel))
                {
                    return RedirectToAction("ListLeaveBalance", "Leave");
                }
                else
                {
                    return RedirectToAction("ListLeaveBalance", "Leave");
                }
            }
            else
            {
                return RedirectToAction("ListLeaveBalance", "Leave");
            }
        }
        public JsonResult GetLeaveBalance(string id)
        {
            decimal State = 0;
            var uid = Session["UserID"].ToString();
            int? EmployeeId = userService.GetEmployeeId(int.Parse(uid));
            if (Convert.ToInt32(id) != Constant.UnpaidLeave)
            {
                State = leaveService.getLeaveBalance(int.Parse(id), EmployeeId);
                if (State == 0)
                {
                    State = 0;
                }
            }
            else
            {
                State = 0;

            }

            return Json(State, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetNoOfLeave(string FromDate, string ToDate, int LeavrtypeId, decimal LeaveBalanceid)
        {
            string Days = "";
            if (ToDate == "")
            {
                Days = "-1";
                return Json(Days, JsonRequestBehavior.AllowGet);
            }
            if (FromDate.Equals(ToDate))
            {
                Days = "1";
                return Json(Days, JsonRequestBehavior.AllowGet);
            }
            else
            {

                DateTime dt1 = DateTime.Parse(FromDate);
                DateTime dt2 = DateTime.Parse(ToDate);
                if (dt1 > dt2)
                {
                    return Json(Days, JsonRequestBehavior.AllowGet);
                }
                TimeSpan ts = dt2.Subtract(dt1);
                Days = (1 + ts.Days).ToString();

                if (Convert.ToInt32(Days) <= LeaveBalanceid && LeavrtypeId != Constant.UnpaidLeave)
                {
                    return Json(Days, JsonRequestBehavior.AllowGet);
                }
                else if (LeavrtypeId == Constant.UnpaidLeave && LeaveBalanceid == 0)
                {
                    Days = "Unpaid leave";
                    return Json(Days, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Days = "0";
                    return Json(Days, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult EmployeeLeave()
        {
            List<SelectListItem> LeaveTypeId = leaveService.GetLeaveType();
            ViewBag.LeaveTypeId = LeaveTypeId;
            List<SelectListItem> Shift = masterService.GetShift();
            ViewBag.Shift = Shift;
            return View();
        }
        [HttpPost]
        public ActionResult EmployeeLeave(EmployeeLeaveModel employeeLeaveModel, FormCollection form)
        {

            if ((employeeLeaveModel.FromDate) > (employeeLeaveModel.ToDate))
            {
                TempData["error"] = "Plase select valid date.";
                return RedirectToAction("EmployeeLeave", "Leave");
            }

            if (ModelState.ContainsKey("employeeLeaveModel.id"))
                ModelState["employeeLeaveModel.id"].Errors.Clear();
            var uid = Session["UserID"].ToString();
            employeeLeaveModel.EmployeeId = userService.GetEmployeeId(int.Parse(uid));
            employeeLeaveModel.NoOfLeave = leaveService.GetNoOfLeave(employeeLeaveModel.FromDate.ToString(), employeeLeaveModel.ToDate.ToString(), employeeLeaveModel.LeaveTypeId, employeeLeaveModel.EmployeeId);
            if (employeeLeaveModel.HalfDay == true)
            {
                int shift = Convert.ToInt32(form["rbg"]);
                employeeLeaveModel.Shift = shift;
                employeeLeaveModel.NoOfLeave = Constant.halfLeave;
            }
            if (ModelState.IsValid)
            {
                if (employeeLeaveModel.id == 0)
                {
                    employeeLeaveModel.CreatedBy = int.Parse(uid);
                    employeeLeaveModel.CreatedDate = DateTime.Now.Date;
                }


                leaveService.saveEmployeeLeave(employeeLeaveModel);
                return RedirectToAction("EmployeeLeave", "Leave");
            }
            else
            {
                return RedirectToAction("EmployeeLeave", "Leave");
            }
        }
        public ActionResult MyLeave()
        {
            ViewData["error"] = TempData["error"];
            ViewData["NotValidDate"] = TempData["NotValidDate"];
            List<SelectListItem> Year = masterService.AllYearOfleave(Constant.fromYear, Constant.toYear);
            ViewBag.Year = Year;
            var uid = Session["UserID"].ToString();
            int? EmployeeId = userService.GetEmployeeId(int.Parse(uid));
            EmployeeLeaveMasterModel employeeLeaveMasterModel = new EmployeeLeaveMasterModel();
            employeeLeaveMasterModel.employeeLeaveModels = leaveService.GetMyLeave(EmployeeId);
            employeeLeaveMasterModel.employeeUnapproveCancleLeaveModels = leaveService.GetCancleLeaveById(EmployeeId);
            return View(employeeLeaveMasterModel);
        }
        public ActionResult ListLeaveApproval()
        {
            UnapproveLeaveModel unapproveLeaveModel = new UnapproveLeaveModel();
            unapproveLeaveModel.employeeUnapproveLeaveModels = leaveService.GetUnApproveLeave();
            unapproveLeaveModel.employeeUnapproveCancleLeaveModels = leaveService.GetUnApproveCancleLeave();
            return View(unapproveLeaveModel);
        }
        public ActionResult LeaveApproval(string id)
        {
            int ApproveBy = int.Parse(Session["UserID"].ToString());
            DateTime ApprovalDate = DateTime.Now.Date;
            int decodeid = Convert.ToInt32(Constant.Decode(id));
            if (leaveService.ApproveLeave(decodeid, ApprovalDate, ApproveBy))
            {
                return RedirectToAction("ListLeaveApproval", "Leave");
            }
            return View();
        }
        public ActionResult LeaveNotApproval(string id)
        {
            int decodeid = Convert.ToInt32(Constant.Decode(id));
            if (leaveService.NotApproveLeave(decodeid))
            {
                return RedirectToAction("ListLeaveApproval", "Leave");
            }
            return View();
        }
        public JsonResult GetYearLeave(string id)
        {
            var uid = Session["UserID"].ToString();
            int? EmployeeId = userService.GetEmployeeId(int.Parse(uid));

            List<EmployeeLeaveModel> State = leaveService.GetMyLeaveFromYear(id, EmployeeId);
            foreach (var item in State)
            {
                if (item.ApprovalStatus == "")
                {
                    item.ApprovalStatus = "Approval Is Inprocess";
                }
                else if (item.ApprovalStatus == "True")
                {
                    item.ApprovalStatus = "Approved";
                }
                else if (item.ApprovalStatus == "False")
                {
                    item.ApprovalStatus = "Leave Not Approve by Admin";
                }

            }
            return Json(State, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetEmployeeNoOfLeave(DateTime FromDate, DateTime ToDate)
        {
            var Days = 0;
            if (ToDate.ToString() == "")
            {
                ToDate = FromDate;
            }
            if (FromDate.Equals(ToDate))
            {

                Days = 1;
                return Json(Days, JsonRequestBehavior.AllowGet);
            }
            else
            {
                DateTime dt1 = FromDate;
                DateTime dt2 = ToDate;
                if (dt1 > dt2)
                {
                    return Json(Days, JsonRequestBehavior.AllowGet);
                }
                TimeSpan ts = dt2.Subtract(dt1);
                Days = (1 + ts.Days);
                return Json(Days, JsonRequestBehavior.AllowGet);

            }

        }
        [HttpPost]
        public ActionResult MyLeave(EmployeeLeaveMasterModel employeeLeaveMasterModel, FormCollection form)
        {
            //Check to date is not lessthan from date
            if (employeeLeaveMasterModel.FromDate > employeeLeaveMasterModel.ToDate)
            {
                TempData["error"] = "Plase select valid date.";
                return RedirectToAction("MyLeave", "Leave");
            }

            if (ModelState.ContainsKey("employeeLeaveModel.id"))
                ModelState["employeeLeaveModel.id"].Errors.Clear();
            var uid = Session["UserID"].ToString();

            employeeLeaveMasterModel.NoOfLeave = leaveService.GetNoOfCancleLeaves(employeeLeaveMasterModel.FromDate.ToString(), employeeLeaveMasterModel.ToDate.ToString());
            if (employeeLeaveMasterModel.HalfDay == true)
            {
                int shift = Convert.ToInt32(form["rbg"]);
                employeeLeaveMasterModel.Shift = shift;
                employeeLeaveMasterModel.NoOfLeave = Constant.halfLeave;
            }
            if (ModelState.IsValid)
            {
                bool isValid = false;
                List<EmployeeLeaveDatesModel> employeeLeaveDatesModel = leaveService.CheckEmployeesDateByLeaveId(employeeLeaveMasterModel.LeaveTypeId);
                //check apply cancle leave date is apply between apply leave date
                foreach (var item in employeeLeaveDatesModel)
                {
                    if ((employeeLeaveMasterModel.FromDate >= item.FromDate && employeeLeaveMasterModel.FromDate <= item.ToDate) && (employeeLeaveMasterModel.ToDate >= item.FromDate && employeeLeaveMasterModel.ToDate <= item.ToDate))
                    {
                        isValid = true;

                    }
                }
                if (isValid == true)
                {
                    List<EmployeeLeaveDatesModel> leaveDatesModels = leaveService.GetEmployeesDateByLeaveId(employeeLeaveMasterModel.LeaveTypeId);
                    bool isExixsts = false;
                    //Check cancle leave is aleady apply betwen this date or not
                    foreach (var item in leaveDatesModels)
                    {
                        if (employeeLeaveMasterModel.FromDate >= item.FromDate && employeeLeaveMasterModel.FromDate <= item.ToDate)
                        {
                            isExixsts = true;
                            break;
                        }
                        //if((employeeLeaveMasterModel.FromDate>=item.FromDate && employeeLeaveMasterModel.ToDate <=item.ToDate) && (employeeLeaveMasterModel.ToDate >= item.FromDate && employeeLeaveMasterModel.ToDate <= item.ToDate))
                        if (employeeLeaveMasterModel.ToDate >= item.FromDate && employeeLeaveMasterModel.ToDate <= item.ToDate)
                        {
                            isExixsts = true;
                            break;
                        }
                    }
                    if (isExixsts == false)
                    {
                        employeeLeaveMasterModel.CreatedBy = int.Parse(uid);
                        employeeLeaveMasterModel.CreatedDate = DateTime.Now.Date;

                        leaveService.saveEmployeeCancleLeave(employeeLeaveMasterModel);
                        return RedirectToAction("MyLeave", "Leave");
                    }
                    else
                    {
                        TempData["error"] = "This Leave Is Already Applys.";
                        return RedirectToAction("MyLeave", "Leave");
                    }
                }

                else
                {
                    TempData["NotValidDate"] = "Please  apply Cancle leave bettween apply leave dates ";
                    return RedirectToAction("MyLeave", "Leave");
                }
            }
            else
            {
                return RedirectToAction("EmployeeLeave", "Leave");
            }
        }
        public ActionResult CancleLeaveApproval(string id)
        {
            int ApproveBy = int.Parse(Session["UserID"].ToString());
            DateTime ApprovalDate = DateTime.Now.Date;
            int decodeid = Convert.ToInt32(Constant.Decode(id));
            if (leaveService.CancleApproveLeave(decodeid, ApprovalDate, ApproveBy))
            {
                return RedirectToAction("ListLeaveApproval", "Leave");
            }
            return View();
        }
        public ActionResult CancleLeaveNotApproval(string id)
        {
            int decodeid = Convert.ToInt32(Constant.Decode(id));
            if (leaveService.NotApproveCancleLeave(decodeid))
            {
                return RedirectToAction("ListLeaveApproval", "Leave");
            }
            return View();
        }
        public ActionResult DeleteCancleLeaveApproval(string id)
        {
            if (id != "0")
            {
                int decodeid = Convert.ToInt32(Constant.Decode(id));
                leaveService.DeleteCancleLeave(decodeid);
                return RedirectToAction("MyLeave", "Leave");
            }

            return RedirectToAction("MyLeave", "Leave");
        }
        public ActionResult HolyDay()
        {

            return View();
        }
        public JsonResult GetNoOfDays(string FromDate, string ToDate)
        {
            string Days = "";
            if (ToDate == "")
            {
                Days = "-1";
                return Json(Days, JsonRequestBehavior.AllowGet);
            }
            if (FromDate.Equals(ToDate))
            {
                Days = "1";
                return Json(Days, JsonRequestBehavior.AllowGet);
            }
            else
            {


                DateTime dt1 = DateTime.Parse(FromDate);
                DateTime dt2 = DateTime.Parse(ToDate);
                if (dt1 > dt2)
                {
                    return Json(Days, JsonRequestBehavior.AllowGet);
                }
                TimeSpan ts = dt2.Subtract(dt1);
                Days = (1 + ts.Days).ToString();
                return Json(Days, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult HolyDay(HolidayListMasterModel holidayListMasterModel)
        {
            if (holidayListMasterModel.HolydayListModel.FromDate > holidayListMasterModel.HolydayListModel.ToDate)
            {
                TempData["error"] = "Plase select valid date.";
                return RedirectToAction("MyLeave", "Leave");
            }
            if (ModelState.ContainsKey("holydayListModel.id"))
                ModelState["holydayListModel.id"].Errors.Clear();
            var uid = Session["UserID"].ToString();

            if (ModelState.IsValid)
            {
                holidayListMasterModel.HolydayListModel.NoOfDays = Convert.ToDecimal(leaveService.GetNoOfLeaves(holidayListMasterModel.HolydayListModel.FromDate.ToString(), holidayListMasterModel.HolydayListModel.ToDate.ToString()));
                holidayListMasterModel.HolydayListModel.CreateBy = int.Parse(uid);
                holidayListMasterModel.HolydayListModel.CreateDate = DateTime.Now.Date;

                leaveService.saveHolyday(holidayListMasterModel.HolydayListModel);
                return RedirectToAction("HolyDay", "Leave");

            }

            else
            {
                return RedirectToAction("HolyDay", "Leave");
            }
        }
        public ActionResult HolidayList()
        {
            ViewData["Success"] = TempData["Success"];
            ViewData["alreadyadd"] = TempData["alreadyadd"];

            HolidayListMasterModel holidayListMasterModel = new HolidayListMasterModel();
            List<SelectListItem> Year = masterService.ListHolydayYear(Constant.fromYear, Constant.toYear);
            ViewBag.Year = Year;
            List<SelectListItem> HolidayYear = masterService.HolydayYear(DateTime.Now.Year, DateTime.Now.Year + 5);
            ViewBag.HolidayYear = HolidayYear;
            List<SelectListItem> WeekendSaturday = masterService.GetWeekDaysSaturday();
            ViewBag.WeekendSaturday = WeekendSaturday;


            HolydayListModel holydayListModel = new HolydayListModel();
            holydayListModel.year = DateTime.Now.Year.ToString();
            holidayListMasterModel.HolydayListModel = holydayListModel;
            holidayListMasterModel.alternativedays = leaveService.getalternativeday();
            holidayListMasterModel.holydayListModels = leaveService.GetHolydayList();
            holidayListMasterModel.weekendmodels = leaveService.GetWeekend();
            return View(holidayListMasterModel);
        }
        [HttpPost]
        public ActionResult HolidayList(HolidayListMasterModel holidayListMasterModel, FormCollection form)

        {

            string type = form["Dayselectionrbg"];

            int ApproveBy = int.Parse(Session["UserID"].ToString());
            DateTime ApprovalDate = DateTime.Now.Date;

            if (leaveService.GetDates(Convert.ToInt32(holidayListMasterModel.HolydayYear), Convert.ToInt32(holidayListMasterModel.WeekdaySaturday), type, holidayListMasterModel.alternativedays, ApprovalDate, ApproveBy))
            {
                TempData["Success"] = "Leave added succesfully";
                return RedirectToAction("HolidayList", "Leave");
            }
            else
            {
                TempData["alreadyadd"] = "Year leave already added";
                return RedirectToAction("HolidayList", "Leave");
            }


        }
        public JsonResult YearHoliday(string id)
        {

            List<HolydayListModel> State = leaveService.GetHolydayListByYear(id);

            return Json(State, JsonRequestBehavior.AllowGet);
        }
        public ActionResult HolidayListForUser()
        {
            List<SelectListItem> Year = masterService.ListHolydayYear(Constant.fromYear, Constant.toYear);
            ViewBag.Year = Year;
            HolidayListMasterModel holidayListMasterModel = new HolidayListMasterModel();
            HolydayListModel holydayListModel = new HolydayListModel();
            holydayListModel.year = DateTime.Now.Year.ToString();
            holidayListMasterModel.HolydayListModel = holydayListModel;
            holidayListMasterModel.holydayListModels = leaveService.GetHolydayList();
            return View(holidayListMasterModel);
        }
        [HttpPost]
        public ActionResult HolidayListForUser(HolidayListMasterModel holidayListMasterModel)
        {
            List<SelectListItem> Year = masterService.ListHolydayYear(Constant.fromYear, Constant.toYear);
            ViewBag.Year = Year;

            holidayListMasterModel.holydayListModels = leaveService.GetHolydayListByYear(holidayListMasterModel.HolydayListModel.year);
            return View(holidayListMasterModel);
        }
    }
}