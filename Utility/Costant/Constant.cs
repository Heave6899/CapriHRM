using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Utility.Security;

namespace Utility.Costant
{


    public static class Constant
    {
        public static string DEFAULT_PASSWORD = "password";
        public static string ReadPermission = "Read";
        public static int? UnpaidLeave = 3;
        public static int fromYear = 2010;
        public static int toYear = DateTime.Now.Year;
        public static int admin = 1;
        public static int LeaveApprove = 2;
        public static int CancleLeave = 3;
        public static decimal halfLeave = Convert.ToDecimal(0.5);
        public static int AdminRoleId = 1;
        public static int HRRoleId = 19;
        public static int EmployeeRoleId = 13;
        public const string AES_KEY = "threatNG@2018";
        public static string Encrypt(this string str)
        {
            return AES.Encrypt(str, AES_KEY);
        }

        public static string Decrypt(this string str)
        {
            return AES.Decrypt(str, AES_KEY);
        }
        public static string Encode(string encodeMe)
        {
            var eid = encodeMe.Encrypt();
            byte[] encoded = System.Text.Encoding.UTF8.GetBytes(eid);
            return Convert.ToBase64String(encoded);
        }
        public static string Decode(string decodeMe)
        {
           
            byte[] encoded = Convert.FromBase64String(decodeMe);

            var descodeString = System.Text.Encoding.UTF8.GetString(encoded);
            var did = descodeString.Decrypt();
            return did;
        }
    }

    public enum Gender : int
    {
        Male = 1,
        Female = 2

    }
    public enum BloodGroup : int
    {

        [Description("O+")]
        OPositive,
        [Description("A+")]
        APositive,
        [Description("B+")]
        BPositive,
        [Description("AB+")]
        ABPositive,
        [Description("O-")]
        ONegative,
        [Description("A-")]
        ANegative,
        [Description("B-")]
        BNegative,
        [Description("AB-")]
        ABNegative

    }
    public enum MaritalStatus : int
    {
        Married = 1,
        Unmarried = 2
    }
    public enum Shift : int
    {
        Morning = 1,
        Evening = 2

    }
    public enum Weekday : int
    {
        [Description("Sunday")]
        Sunday,
        [Description("Monday")]
        Monday,
        [Description("Tuesday")]
        Tuesday,
        [Description("Wednesday")]
        Wednesday,
        [Description("Thursday")]
        Thursday,
        [Description("Friday")]
        Friday,
        [Description("Saturday")]
        Saturday,

    }
    public enum AlternativeDay : int
    {
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4,
        Fifth = 5
    }


}