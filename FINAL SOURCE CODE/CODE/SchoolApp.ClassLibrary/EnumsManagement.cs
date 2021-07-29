using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.ClassLibrary
{
    public static class EnumsManagement
    {
        public enum USERTYPE
        {
            ADMIN = 1,
            SCHOOL = 2,
            TEACHER = 3,
            PARENT = 4,
        }
        public enum USERROLE
        {
            ADMIN = 1,
            BUISNESSDEVELOPMENT = 2,
            SUPPORT = 3,
            APPSADMIN = 4,
        }
        public enum ROLETYPE
        {
            TEACHING = 1,
            NONTEACHING = 2
        }
        public enum PRIORITY
        {
            CRITICAL = 1,
            HIGH = 2,
            MEDIUM = 3,
            LOW = 4,
        }
        public enum SUPPORTSTATUS
        {
            UnResolved = 1,
            InProgress = 2,
            Resolved = 3,
            Cancelled = 4,
        }
        public enum MESSAGEUSERTYPE
        {
            School = 1,
            Teacher = 2,
            Parent = 3,
            NonTeacher = 4,
        }
        public enum ACCOUNTSTATUS
        {
            InProcess = 1,
            Active = 2,
            InActive = 3,
        }
        public enum SCHOOLTYPE
        {
            AfterSchool = 1,
            Standard = 2,
        }
        public enum CLASSTYPE
        {
            Standard = 1,
            Breakfast = 2,
            AfterSchool = 3,
            Office = 5,
            Club= 6
        }
        public enum PICKERTYPE
        {
            Individual = 1,
            Organisation = 2
        }
        public enum PICKUPSTATUS
        {
            SchoolClosed = 1,
            UnPicked = 2,
            Picked = 3,
            PickedLate = 4,
            PickedChargeable = 5,
            PickedReportable = 6,
            AfterSchool = 7,
            AfterSchool_Ex = 8,
            MarkAsAbsent = 9,
            SendToOffice = 10
        }
        public enum CREATEBYTYPE
        {
            School = 1,
            Teacher = 2
        }
        public enum APIOPERATION
        {
            GETTOKEN = 1,
            PARENTLOGIN = 2,
            IMAGEUPLOAD = 3,
            PARENTFORGOTPASSWORD = 4,
            DAILYPICKUPSTATUS = 5,
            STUDENTLISTBYPARENT = 6,
            ALLPICKERLIST = 7,
            PICKERLIST = 8,
            GENERATEPICKERCODE = 9,
            DELETEPICKER = 10,
            GETPICKER = 11,
            ADDUPDATEPICKER = 12,
            TEACHERLOGIN = 13,
            PICKUPLIST = 14,
            TEACHERCLASSASSIGNMENT = 15,
            DAILYCLASSREPORT = 16,
            STUDENTLISTBYCLASS = 17,
            STUDENTPICKUPREPORT = 18,
            TEACHERFORGOTPASSWORD = 19,
            DAILYCLASSREPORTFILTER = 20,
            SCHOOLLOGIN = 21,
            SCHOOLFORGOTPASSWORD = 22,
            GETADMINROLES = 23,
            SCHOOLCLASSYEAR = 24,
            GETCLASSBYYEAR = 25,
            GETCLASSTYPELIST = 26,
            GETSCHOOLTYPE = 27,
            CREATECLASS = 28,
            DELETEADMINROLES = 29,
            ADDUPDATEADMINROLES = 30,
            EDITADMINROLES = 31,
            CLASSDETAILS = 32,
            UPDATECLASS = 33,
            USERACTIVITY = 34,
            PAYMENTLIST = 35,
            TEACHERLIST = 36,
            GETSTUDENTINFO = 37,
            CREATEUPDATESTUDENT = 38,
            SUPPORTLIST = 39,
            ADDUPDATETEACHER = 40,
            GETTEACHERBYID = 41,
            GETROLES = 42,
            SUPPORTSTATUS = 43,
            DELETESTUDENT = 44,
            CREATESUPPORT = 45,
            VIEWSUPPORT = 46,
            SUDENTCLASSASSIGNMENT = 47,
            STUDENTIMAGEUPLOAD = 48,
            TEACHERIMAGEUPLOAD = 49,
            SCHOOLIMAGEUPLOAD = 50,
            RECIVEDMESSAGE = 51,
            SENTMESSAGE = 52,
            DELETEMESSAGE = 53,
            GETRECEIVERLIST = 54,
            SENDMESSAGETOUSER = 55,
            REASSIGNTEACHER = 56,
            LOGTYPELIST = 57,
            SENDSUPPORT = 58,
            SCHOOLPROFILE = 59,
            UPDATESCHOOLPROFILE = 60,
            ATTENDENCE = 61,
            EDITCHILDRENASSIGNMENT = 62,
            REMOVEPICKERFROMCHILD = 63,
            VIEWPICKERASSIGNMENT = 64,
            SENDATTENDENCESTATUS = 65,
            ATTENDENCELISTBYCLASS = 66,
            SCHOOLMESSAGE = 67,
            PICKUPMESSAGE = 68,
            STUDENTATTENDENCEREPORT = 69,
            SENDUNPICKALERT = 70,
            EDITMORECHILDRENASSIGNMENT = 71,
            VIEWPICKERFORTODAY = 72,
            CONFIRMPICKUP = 73,
            MAKEPICKERFORTODAY = 74,
            STUDENTWISESCHOOL = 75,
            EXPORTTOEXCELATTENDENCE = 76,
            EXPORTTOEXCELSTUDENTPICKUP = 77,
            TEACHERCHANGEPASSWORD = 78,
            MAKEACTIVEDEACTIVE = 79,
            TICKETMESSAGEDETAILSBYSUPPORT = 80,
            PICKERSBYSTUDENTID = 81,
            SCHOOLORGANIZATIONLIST = 82,
            RESETPICKER = 83,
            GETREASSIGNCLASSES = 84,
            GETROLETYPE = 85,
            GETCLASSTYPES = 86,
            SUPPORTINCREMENTNO = 87,
            GETNONTEACHINGROLES = 88,
            NONTEACHERLIST = 89,
            ADDUPDATENONTEACHER = 90,
            GETNONTEACHERBYID = 91,
            HOLIDAYLIST = 92,
            ADDUPDATEHOLIDAY = 93,
            SENDMSGTOTEACHERSTUDENT = 94,
            SENDMSGTOSCHOOLSTUDENT = 95,
            SENDMSGTOTEACHERSCHOOL = 96,
            GETPICKUPMESSAGE = 97,
            GETHOLIDAYBYID = 98,
            DELETEHOLIDAY = 99,
            LOGACTIVITYDATA = 100,
            MARKASABSENT = 101,
            SENDTOAFTERSCHOOL = 102,
            SENDTOOFFICE = 103,
            PICKERTYPELIST = 104,
            PICKERIMAGEUPLOAD = 105,
            STUDENTBYPICKER = 106,
            PAYMENTSTATUS = 107,
            GETSTANDARDCLASS = 108,
            CLEARACTIVITY = 109,
            GETAFTEROFFICECLASS = 110,
            SCHOOLCHANGEPASSWORD = 111,
            SEARCHAFTERSCHOOL = 112,
            SEARCHTEACHERBYSCHOOL = 113,
            PARENTPROFILE = 114,
            PARENTCHANGEPASSWORD = 115,
            ALLPICKERS = 116,
            PICKUPSTATUS = 117,
            GETASSIGNEDPICKERS = 118,
            GETSTUDENTSCHOOLS = 119,
            TEACHERPROFILE = 120,
            TEACHERRESETPASSWORD = 121,
            COMPLETEATTENDENCERUN = 122,
            COMPLETEPICKUPRUN = 123,
            VIEWINVOICEPDF = 124,
            EXPORTTOEXCELATTE = 125,
            EXPORTDAILYCLASSREPORT = 126,
        }
    }
}
