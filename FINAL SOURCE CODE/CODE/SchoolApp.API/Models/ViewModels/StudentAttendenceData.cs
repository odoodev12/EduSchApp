using Newtonsoft.Json;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static SchoolApp.ClassLibrary.EnumsManagement;

namespace SchoolApp.API.Models.ViewModels
{
    public class StudentAttendenceData
    {
        public string Status { get; set; }
        public string MarkedTime { get; set; }
        public string MarkedDate { get; set; }
        public int StudentID { get; set; }
        public DateTime? Date { get; set; }
        public string StudentName { get; set; }
        public string StudentPic { get; set; }
        public int? ClassID { get; set; }
        public string TeacherName { get; set; }
        public int? TeacherID { get; set; }
    }


    public class AdminDetails
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool? Deleted { get; set; }
        public bool? ISActivated { get; set; }
        public bool? Active { get; set; }
        public string FullName { get; set; }
        public string MemorableQueAnswer { get; set; }
        public USERTYPE USERTYPE { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class TeacherDetails : Teacher
    {
        public string SchoolName { get; set; }

        public string UserRoleName { get; set; }
        public string SchoolTypeName { get; set; }
        public DateTime? TeacherEndDate { get; set; }

        public string ClassName { get; set; }

        public string RoleName { get; set; }

        public string RoleType { get; set; }

        public int? SchoolType { get; set; }
    }


    public class SchoolDetails : SchoolOtherEntity
    {
      
        public string SchoolType { get; set; }
        public string CountryName { get; set; }

        public string IsNotificationPickupStr { get; set; }
        public string NotificationAttendenceStr { get; set; }

        public string BillingCountryName { get; set; }

        public string AccountStatusName { get; set; }

        public string SchoolStatus { get; set; }
    }

    public class StudentParentDetails
    {
        public int ID { get; set; }
        public string ParentEmail { get; set; }

        public bool? Deleted { get; set; }
        public bool? ISActivated { get; set; }
        public bool? Active { get; set; }
        public string StudentName { get; set; }

        public string ParentName { get; set; }

        public string StudentNo { get; set; }
        public string Photo { get; set; }
        public Nullable<int> ClassID { get; set; }

        //public string Password { get; set; }

        public string ParentPhone { get; set; }

        public string ParentRelation { get; set; }

        public Nullable<int> SchoolID { get; set; }

        public string ClassName { get; set; }

        public string SchoolName { get; set; }
    }
}