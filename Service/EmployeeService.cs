using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ViewModel;
using DataLayer1;
using Utility.Costant;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Service
{
    public class EmployeeService
    {
        //Get All Country
        public List<SelectListItem> GetCountries()
        {
            using (var db = new CapriHRMEntities())
            {

                List<SelectListItem> List1 = (from s in db.CountryMasters
                                              select new SelectListItem
                                              {
                                                  Text = s.Name,
                                                  Value = s.ID.ToString()
                                              }).ToList();

                List1.Insert(0, new SelectListItem { Text = "--Select Country--", Value = "" });
                return List1;
            };

        }
        //Get India 
        public List<SelectListItem> GetCountries(int id)
        {

            using (var db = new CapriHRMEntities())
            {

                List<SelectListItem> List1 = (from s in db.CountryMasters
                                              where s.ID == id
                                              select new SelectListItem
                                              {
                                                  Text = s.Name,
                                                  Value = s.ID.ToString()
                                              }).ToList();

                List1.Insert(0, new SelectListItem { Text = "--Select Country--", Value = "" });
                return List1;
            };

        }
        //Get All state from specific Country
        public List<SelectListItem> GetState(int id)
        {

            using (var db = new CapriHRMEntities())
            {

                List<SelectListItem> List1 = (from s in db.StateMasters
                                              where s.CountryID == id
                                              select new SelectListItem
                                              {
                                                  Text = s.Name,
                                                  Value = s.ID.ToString()
                                              }).ToList();


                return List1;
            };

        }
        //Get All City From Specific State
        public List<SelectListItem> GetCity(int id)
        {

            using (var db = new CapriHRMEntities())
            {

                List<SelectListItem> List1 = (from s in db.CityMasters
                                              where s.StateID == id
                                              select new SelectListItem
                                              {
                                                  Text = s.Name,
                                                  Value = s.ID.ToString()
                                              }).ToList();


                return List1;
            };

        }
        //Save Employee
        public bool save(EmployeeModel employeeModel)
        {
            bool IsSaved = false;
            try
            {
                using (var db = new CapriHRMEntities())
                {

                    Employee employee;


                    //edit
                    if (employeeModel.EmployeeId != 0)
                    {
                        employee = db.Employees.Where(c => c.EmployeeId == employeeModel.EmployeeId && c.IsActive == true).FirstOrDefault();
                        employee.EmployeeContacts.FirstOrDefault().Address = employeeModel.EmployeeContact.Address;
                        employee.EmployeeContacts.FirstOrDefault().ZipCode = employeeModel.EmployeeContact.ZipCode;
                        employee.EmployeeContacts.FirstOrDefault().MobileNo = employeeModel.EmployeeContact.MobileNo;
                        employee.EmployeeContacts.FirstOrDefault().EmailId = employeeModel.EmployeeContact.EmailId;
                        employee.EmployeeContacts.FirstOrDefault().Country = employeeModel.EmployeeContact.Country;
                        employee.EmployeeContacts.FirstOrDefault().State = employeeModel.EmployeeContact.State;
                        employee.EmployeeContacts.FirstOrDefault().City = employeeModel.EmployeeContact.City;
                        employee.EmployeeContacts.FirstOrDefault().BloodGroup = employeeModel.EmployeeContact.BloodGroup;
                        employee.UpdatedDate = DateTime.Today;
                        employee.UpdateBy = employeeModel.UpdateBy;

                    }
                    else
                    {
                        employee = new Employee();
                        EmployeeContact employeeContact = new EmployeeContact();
                        db.Employees.Add(employee);
                        employee.EmployeeContacts = new List<EmployeeContact>();
                        employeeContact.Address = employeeModel.EmployeeContact.Address;
                        employeeContact.ZipCode = employeeModel.EmployeeContact.ZipCode;
                        employeeContact.MobileNo = employeeModel.EmployeeContact.MobileNo;
                        employeeContact.EmailId = employeeModel.EmployeeContact.EmailId;
                        employeeContact.Country = employeeModel.EmployeeContact.Country;
                        employeeContact.State = employeeModel.EmployeeContact.State;
                        employeeContact.City = employeeModel.EmployeeContact.City;
                        employeeContact.BloodGroup = employeeModel.EmployeeContact.BloodGroup;

                        employee.EmployeeContacts.Add(employeeContact);
                        employee.CreatedDate = DateTime.Today;
                        employee.CreateBy = employeeModel.CreateBy;

                    }
                    employee.FristName = employeeModel.FristName;
                    employee.MiddleName = employeeModel.MiddleName;
                    employee.LastName = employeeModel.LastName;
                    employee.Gender = employeeModel.Gender;
                    employee.Nationality = employeeModel.Nationality;
                    employee.BirthDate = employeeModel.BirthDate;
                    employee.PanCardNo = employeeModel.PanCardNo;
                    employee.MaritalStatus = employeeModel.MaritalStatus;
                    employee.IsActive = true;



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
        //Get Employee By Id
        public EmployeeModel GetListById(int id)
        {
            EmployeeModel employee = new EmployeeModel();
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    employee = db.Employees.Where(c => c.EmployeeId == id).Select(c => new EmployeeModel()
                    {
                        FristName = c.FristName,
                        LastName = c.LastName,
                        BirthDate = c.BirthDate,
                        PanCardNo = c.PanCardNo,
                        EmployeeId = c.EmployeeId,
                        MiddleName = c.MiddleName,
                        Gender = c.Gender,
                        Nationality = c.Nationality,
                        MaritalStatus = c.MaritalStatus,
                        EmployeeContact = new EmployeeContactModel()
                        {
                            BloodGroup = c.EmployeeContacts.FirstOrDefault().BloodGroup,
                            Country = c.EmployeeContacts.FirstOrDefault().Country,
                            State = c.EmployeeContacts.FirstOrDefault().State,
                            City = c.EmployeeContacts.FirstOrDefault().City,
                            EmailId = c.EmployeeContacts.FirstOrDefault().EmailId,
                            MobileNo = c.EmployeeContacts.FirstOrDefault().MobileNo,
                            Address = c.EmployeeContacts.FirstOrDefault().Address,
                            ZipCode = c.EmployeeContacts.FirstOrDefault().ZipCode,
                        }

                    }).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return employee;
        }
        //List of Employee
        public List<EmployeeModel> GetList()
        {
            List<EmployeeModel> employee = new List<EmployeeModel>();
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    employee = db.Employees.Select(c => new EmployeeModel()
                    {
                        EmployeeId = c.EmployeeId,
                        FristName = c.FristName,
                        LastName = c.LastName,
                        BirthDate = c.BirthDate,
                        PanCardNo = c.PanCardNo,
                        Nationality = c.Nationality,
                        NationalityName = c.CountryMaster.Name,
                        MaritalStatus = c.MaritalStatus,

                    }).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return employee;
        }
        //Delete Employee
        public bool Delete(int id)
        {
            bool IsSaved = false;
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    var delete = (from obj in db.Employees where obj.EmployeeId == id select obj).First();
                    db.EmployeeContacts.RemoveRange(delete.EmployeeContacts);
                    db.Employees.Remove(delete);
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
        //Profile Of User
        public UserProfileModel GetEmployee(int id)
        {
            UserProfileModel userProfileModel = new UserProfileModel();
            User user = new User();
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    int id1 = db.Users.Where(c => c.UserId == id).Select(c => c.EmployeeId).FirstOrDefault().Value;
                    userProfileModel = db.Employees.Where(c => c.EmployeeId == id1).Select(c => new UserProfileModel()
                    {
                        Name = c.FristName + " " + c.MiddleName + " " + c.LastName,
                        FirstName=c.FristName,
                        MiddleName=c.MiddleName,
                        LastName=c.LastName,
                        Nationality=c.Nationality,
                        BirthDate = c.BirthDate,
                        PanCardNo = c.PanCardNo,
                        EmployeeId = c.EmployeeId,
                        Gender = c.Gender,
                        NationalityName = c.CountryMaster.Name,
                        MaritalStatus = c.MaritalStatus,
                        ProfileImage = c.Image,
                        userProfilePersonalModel = new UserProfilePersonalModel()
                        {
                            BloodGroup = c.EmployeeContacts.FirstOrDefault().BloodGroup,
                            CountryName = c.EmployeeContacts.FirstOrDefault().CountryMaster.Name,
                            StateName = c.EmployeeContacts.FirstOrDefault().StateMaster.Name,
                            CityName = c.EmployeeContacts.FirstOrDefault().CityMaster.Name,
                            Country = c.EmployeeContacts.FirstOrDefault().Country,
                            State = c.EmployeeContacts.FirstOrDefault().State,
                            City = c.EmployeeContacts.FirstOrDefault().City,
                            EmailId = c.EmployeeContacts.FirstOrDefault().EmailId,
                            MobileNo = c.EmployeeContacts.FirstOrDefault().MobileNo,
                            Address = c.EmployeeContacts.FirstOrDefault().Address,
                            ZipCode = c.EmployeeContacts.FirstOrDefault().ZipCode,
                        }

                    }).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return userProfileModel;
        }
       
    }
}
