using DataLayer1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility.Costant;


namespace Service
{
    public class MasterService
    {
        //Get Gender 
        public List<SelectListItem> GetGender()
        {
            List<SelectListItem> List1 = (from Gender e in Enum.GetValues(typeof(Gender))
                                          select new SelectListItem
                                          {
                                              Text = Enum.GetName(typeof(Gender), e),
                                              Value = ((int)e).ToString()
                                          }).ToList();

            List1.Insert(0, new SelectListItem { Text = "--Select Gender--", Value = "" });
            return List1;

        }
        //Get Martial Status
        public List<SelectListItem> GetMartialStatus()
        {
            List<SelectListItem> List1 = (from MaritalStatus e in Enum.GetValues(typeof(MaritalStatus))
                                          select new SelectListItem
                                          {
                                              Text = Enum.GetName(typeof(MaritalStatus), e),
                                              Value = ((int)e).ToString()
                                          }).ToList();

            List1.Insert(0, new SelectListItem { Text = "--Select Maritial Status--", Value = "" });
            return List1;

        }
        //Get BloodGroup
        public List<SelectListItem> GetBloodGroup()
        {
            List<SelectListItem> List1 = (from BloodGroup e in Enum.GetValues(typeof(BloodGroup))
                                          select new SelectListItem
                                          {
                                              Text = EnumUtility<BloodGroup>.GetDescriptionByKey((int)e),
                                              Value = ((int)e).ToString()
                                          }).ToList();

            List1.Insert(0, new SelectListItem { Text = "-- Select BloodGroup --", Value = "" });
            return List1;

        }
        public List<SelectListItem> Year(int fromYear, int toYear)
        {
            List<SelectListItem> yearList = new List<SelectListItem>();
            yearList.Add(new SelectListItem { Text = "-- Select Year --", Value = "" });
            for (int i = fromYear; i <= toYear; i++)
            {
                yearList.Add(new SelectListItem { Text = i + "-" + (i + 1), Value = i.ToString() });
            }
            return yearList;
        }
        //All Year
        public List<SelectListItem> AllYear(int fromYear, int toYear)
        {
            List<SelectListItem> yearList = new List<SelectListItem>();
            //yearList.Add(new SelectListItem { Text = DateTime.Now.Year + "-" + (DateTime.Now.Year + 1), Value = "" });
            for (int i = fromYear; i <= toYear; i++)
            {
                yearList.Add(new SelectListItem { Text = i + "-" + (i + 1), Value = i.ToString() });
            }
            return yearList;
        }
        //All Year for  
        public List<SelectListItem> AllYearOfleave(int fromYear, int toYear)
        {
            List<SelectListItem> yearList = new List<SelectListItem>();
            yearList.Add(new SelectListItem { Text = DateTime.Now.Year + "-" + (DateTime.Now.Year + 1), Value = "" });
            for (int i = fromYear; i <= toYear; i++)
            {
                yearList.Add(new SelectListItem { Text = i + "-" + (i + 1), Value = i.ToString() });
            }
            return yearList;


        }
        public List<SelectListItem> ListHolydayYear(int fromYear, int toYear)
        {
            List<SelectListItem> yearList = new List<SelectListItem>();
            yearList.Add(new SelectListItem { Text = "All", Value = "0" });
            for (int i = fromYear; i <= toYear; i++)
            {
                yearList.Add(new SelectListItem { Text = i + "-" + (i + 1), Value = i.ToString() });
            }
            return yearList;
        }
        public List<SelectListItem> HolydayYear(int fromYear, int toYear)
        {
            List<SelectListItem> yearList = new List<SelectListItem>();
         
            for (int i = fromYear; i <= toYear; i++)
            {
                yearList.Add(new SelectListItem { Text = i + "-" + (i + 1), Value = i.ToString() });
            }
            return yearList;
        }
        //Shift List
        //Shift List
        public List<SelectListItem> GetShift()
        {
            List<SelectListItem> List1 = (from Shift e in Enum.GetValues(typeof(Shift))
                                          select new SelectListItem
                                          {
                                              Text = Enum.GetName(typeof(Shift), e),
                                              Value = ((int)e).ToString()
                                          }).ToList();

            return List1;

        }
        public List<SelectListItem> GetWeekDaysSaturday()
        {
            List<SelectListItem> List1 = (from Weekday e in Enum.GetValues(typeof(Weekday))
                                          select new SelectListItem
                                          {
                                              Text = EnumUtility<Weekday>.GetDescriptionByKey((int)e),
                                              Value = ((int)e).ToString()
                                          }).ToList();

            return List1;

        }
       

       
    }
}