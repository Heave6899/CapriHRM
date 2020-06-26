using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ViewModel;
using DataLayer1;
using Utility;
using System.Web.Mvc;
using Utility.Costant;

namespace Service
{
    public class UserService
    {
        //Ckeck Login Deteils
        public int UserLogin(string username, string password)
        {
            int uid = 0;
            try
            {

                using (var db = new CapriHRMEntities())
                {
                    var user = (from s in db.Users where s.UserName == username select s).FirstOrDefault();
                    if (user != null)
                    {
                        var salt = user.Salt;
                        var encryptPassword = Helper.EncodePassword(password, salt);
                        var query = (from s in db.Users where s.UserName == username && s.Password.Equals(encryptPassword) select s).FirstOrDefault();
                        if (query != null)
                        {

                            uid = user.UserId;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return uid;
        }
        //Get UserRopMenu 
        public UserTopMenuModel UserProfileImage(int id)
        {
            UserTopMenuModel userTopMenuModel = new UserTopMenuModel();
            using (var db = new CapriHRMEntities())
            {
                userTopMenuModel = db.Users.Where(c => c.UserId == id).Select(c => new UserTopMenuModel()
                {
                    UserImage = c.Employee.Image,
                    UserName = c.Employee.FristName +" "+ c.Employee.LastName,
                    Gender = c.Employee.Gender
                }).FirstOrDefault();
            }
            return userTopMenuModel;

        }
        // Get List of Role
        public List<SelectListItem> GetRole()
        {
            using (var db = new CapriHRMEntities())
            {

                List<SelectListItem> List1 = (from s in db.Roles
                                              select new SelectListItem
                                              {
                                                  Text = s.RoleName,
                                                  Value = s.RoleId.ToString()
                                              }).ToList();

                List1.Insert(0, new SelectListItem { Text = "--Select Role--", Value = "" });
                return List1;
            }



        }
        //Get User Details
        public UserModel GetUserById(int id)
        {
            UserModel userModel = new UserModel();
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    userModel = db.Users.Where(c => c.UserId == id).Select(c => new UserModel()
                    {
                        UserId = c.UserId,
                        UserName = c.Employee.FristName + "" + c.Employee.LastName
                    }).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return userModel;
        }
        //List Of Employee
        public List<SelectListItem> GetEmployee()
        {
            using (var db = new CapriHRMEntities())
            {

                List<SelectListItem> List1 = (from s in db.Users
                                              where s.RoleId!=1
                                              select new SelectListItem
                                              {
                                                  Text = s.Employee.FristName,
                                                  Value = s.EmployeeId.ToString()
                                              }).ToList();

                List1.Insert(0, new SelectListItem { Text = "--Select Employee--", Value = "" });
                return List1;
            }



        }
        //CreateUser
        public bool saveUser(UserModel userModel)
        {
            bool IsSaved = false;
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    User user = new User();
                    var salt = Helper.GeneratePassword(10);
                    var password = Helper.EncodePassword(userModel.Password, salt);
                    user.RoleId = userModel.Role;
                    user.EmployeeId = userModel.EmployeeId;
                    user.UserName = userModel.UserName;
                    user.Password = password;
                    user.CreateBy = userModel.CreateBy;
                    user.CreatedDate = userModel.CreatedDate;
                    user.Salt = salt;
                    user.IsActive = true;
                    db.Users.Add(user);
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
        //List Of Users
        public List<UserModelList> GetList()
        {
            List<UserModelList> user = new List<UserModelList>();
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    user = db.Users.Select(c => new UserModelList()
                    {
                        UserId = c.UserId,
                        EmployeeName = c.Employee.FristName,
                        UserName = c.UserName,
                        RoleName = c.Role.RoleName



                    }).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return user;
        }
        //Delete User
        public bool DeleteUser(int id)
        {
            bool IsSaved = false;
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    var delete = (from obj in db.Users where obj.UserId == id select obj).First();
                    db.Users.Remove(delete);
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
        //Check Old Password
        public bool CheckOldPassword(string password, int id)
        {
            bool IsSaved = false;
            try
            {
                ChangepasswordModel resetpasswordModel = new ChangepasswordModel();
                using (var db = new CapriHRMEntities())
                {

                    resetpasswordModel = db.Users.Where(c => c.UserId == id).Select(c => new ChangepasswordModel()
                    {
                        OldPassword = c.Password,
                        Salt = c.Salt
                    }).FirstOrDefault();


                    var EncryptpPassword = Helper.EncodePassword(password, resetpasswordModel.Salt);
                    if (resetpasswordModel.OldPassword == EncryptpPassword)
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
        //Change Password
        public bool ChangePassword(ChangepasswordModel changepasswordModel)
        {
            bool IsSaved = false;
            try
            {
                ChangepasswordModel model = new ChangepasswordModel();
                using (var db = new CapriHRMEntities())
                {
                    User user = new User();
                    model = db.Users.Where(c => c.UserId == changepasswordModel.UserId).Select(c => new ChangepasswordModel()
                    {
                        Salt = c.Salt
                    }).FirstOrDefault();


                    var EncryptpPassword = Helper.EncodePassword(changepasswordModel.NewPassword, model.Salt);
                    user = db.Users.Where(c => c.UserId == changepasswordModel.UserId).FirstOrDefault();
                    user.Password = EncryptpPassword;
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
        //Reset Password
        public bool ResetPassword(int id)
        {
            bool IsSaved = false;
            UserModel userModel = new UserModel();
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    User user = new User();
                    var userdata = db.Users.Where(c => c.UserId == id).Select(c => new UserModel()
                    {
                        Salt = c.Salt
                    }).FirstOrDefault();

                    //var salt = Helper.GeneratePassword(10);
                    var Newpassword = Helper.EncodePassword(Constant.DEFAULT_PASSWORD, userdata.Salt);
                    user = db.Users.Where(c => c.UserId == id).FirstOrDefault();
                    user.Password = Newpassword;
                    user.Salt = userdata.Salt;
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
        //Get User Details
        public UserDetails GetUserDetailsId(int id)
        {
            UserDetails userDetails = new UserDetails();
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    userDetails = db.Users.Where(c => c.UserId == id).Select(c => new UserDetails()
                    {
                        RoleId = c.RoleId,
                        UserName = c.Employee.FristName + "  " + c.Employee.LastName
                    }).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return userDetails;
        }
        //Save User Profile Image 
        public string ProfileImageSave(UserProfileModel userProfileModel)
        {
            string ServerImagePath = "";

            try
            {
                using (var db = new CapriHRMEntities())
                {

                    Employee employee;
                    employee = db.Employees.Where(c => c.EmployeeId == userProfileModel.EmployeeId && c.IsActive == true).FirstOrDefault();
                    ServerImagePath = employee.ServerImagePath;
                    employee.Image = userProfileModel.ProfileImage;
                    employee.ServerImagePath = userProfileModel.ServerMapPathImage;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return ServerImagePath;
        }
        //Remove Profile Image
        public string RemoveProfileImage(int id)
        {
            string imagepath = "";
            try
            {
                using (var db = new CapriHRMEntities())
                {

                    Employee employee;
                    employee = db.Employees.Where(c => c.EmployeeId == id && c.IsActive == true).FirstOrDefault();
                    imagepath = employee.ServerImagePath;
                    employee.Image = null;
                    employee.ServerImagePath = null;
                    db.SaveChanges();



                }
            }
            catch (Exception)
            {

                throw;
            }

            return imagepath;
        }
        //Update Details by User
        public bool UpdateByUser(UserProfileModel userProfileModel)
            {
            bool IsSaved = false;
            try
            {
                using (var db = new CapriHRMEntities())
                {

                    Employee employee;


                    //edit

                    employee = db.Employees.Where(c => c.EmployeeId == userProfileModel.EmployeeId && c.IsActive == true).FirstOrDefault();
                    employee.EmployeeContacts.FirstOrDefault().Address = userProfileModel.userProfilePersonalModel.Address;
                    employee.EmployeeContacts.FirstOrDefault().ZipCode = userProfileModel.userProfilePersonalModel.ZipCode;
                    employee.EmployeeContacts.FirstOrDefault().MobileNo = userProfileModel.userProfilePersonalModel.MobileNo;
                    employee.EmployeeContacts.FirstOrDefault().EmailId = userProfileModel.userProfilePersonalModel.EmailId;
                    employee.EmployeeContacts.FirstOrDefault().Country = userProfileModel.userProfilePersonalModel.Country;
                    employee.EmployeeContacts.FirstOrDefault().State = userProfileModel.userProfilePersonalModel.State;
                    employee.EmployeeContacts.FirstOrDefault().City = userProfileModel.userProfilePersonalModel.City;
                    employee.EmployeeContacts.FirstOrDefault().BloodGroup = userProfileModel.userProfilePersonalModel.BloodGroup;
                    employee.UpdatedDate = DateTime.Today;
                    employee.UpdateBy = userProfileModel.UpdateBy;
                    employee.FristName = userProfileModel.FirstName;
                    employee.MiddleName = userProfileModel.MiddleName;
                    employee.LastName = userProfileModel.LastName;
                    employee.Gender = userProfileModel.Gender;
                    employee.Nationality = userProfileModel.Nationality;
                    employee.BirthDate = userProfileModel.BirthDate;
                    employee.PanCardNo = userProfileModel.PanCardNo;
                    employee.MaritalStatus = userProfileModel.MaritalStatus;
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
        //Get Employee Id from Login Id
        public int? GetEmployeeId(int id)
        {
            int? EmployeeId = 0;
            try
            {
                using (var db = new CapriHRMEntities())
                {
                    EmployeeId = db.Users.Where(c => c.UserId == id).Select(c => c.EmployeeId).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return EmployeeId;
        }
    }
}