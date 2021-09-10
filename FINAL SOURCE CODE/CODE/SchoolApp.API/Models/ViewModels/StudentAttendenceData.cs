using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public string Email { get; set; }
        public bool? Deleted { get; set; }
        public bool? ISActivated { get; set; }
        public bool? Active { get; set; }
        public string FullName { get; set; }
        public string MemorableQueAnswer { get; set; }
    }

    public class TeacherDetails
    {
        public string Email { get; set; }
        public bool? Deleted { get; set; }
        public bool? ISActivated { get; set; }
        public bool? Active { get; set; }
        public string FullName { get; set; }
        public string MemorableQueAnswer { get; set; }

        public int ID { get; set; }
        public string Name { get; set; }

        public string TeacherNo { get; set; }
        public string Title { get; set; }
        public string Photo { get; set; }
        public string PhoneNo { get; set; }

        public string TeacherEndDate { get; set; }

        public int SchoolID { get; set; }

        public int RoleID { get; set; }

        public int? ClassID { get; set; }

        public string ClassName { get; set; }

        public string RoleName { get; set; }

        public int? Role { get; set; }

        public string RoleType { get; set; }

        public int? SchoolType { get; set; }

        //public string IsActivationID { get; set; }

    }


    public class SchoolDetails
    {
        public string Email { get; set; }
        public bool? Deleted { get; set; }
        public bool? ISActivated { get; set; }
        public bool? Active { get; set; }
        public string FullName { get; set; }
        public string MemorableQueAnswer { get; set; }

        public int ID { get; set; }
        public string CustomerNumber { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public int? TypeID { get; set; }

        public string SchoolType { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Town { get; set; }

        public int? CountryID { get; set; }

        public string CountryName { get; set; }

        public string Logo { get; set; }
        public string AdminFirstName { get; set; }
        public string AdminLastName { get; set; }
        public string AdminEmail { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public string SupervisorFirstname { get; set; }
        public string SupervisorLastname { get; set; }
        public string SupervisorEmail { get; set; }

        public Nullable<System.DateTime> OpningTime { get; set; }
        public Nullable<System.DateTime> ClosingTime { get; set; }

        public string OpeningTimeStr { get; set; }
        public string ClosingTimeStr { get; set; }

        public string LateMinAfterClosing { get; set; }
        public string ChargeMinutesAfterClosing { get; set; }
        public string ReportableMinutesAfterClosing { get; set; }


        public Nullable<bool> SetupTrainingStatus { get; set; }
        public Nullable<System.DateTime> SetupTrainingDate { get; set; }

        public Nullable<System.DateTime> ActivationDate { get; set; }
        public Nullable<System.DateTime> SchoolEndDate { get; set; }


        public Nullable<bool> isAttendanceModule { get; set; }
        public Nullable<bool> isNotificationPickup { get; set; }


        public string IsNotificationPickupStr { get; set; }

        public Nullable<bool> NotificationAttendance { get; set; }

        public string NotificationAttendenceStr { get; set; }

        public string AttendanceModule { get; set; }


        public string PostCode { get; set; }
        public string BillingAddress { get; set; }
        public string BillingAddress2 { get; set; }
        public string BillingPostCode { get; set; }
        public Nullable<int> BillingCountryID { get; set; }


        public string BillingCountryName { get; set; }

        public string BillingTown { get; set; }
        public string Classfile { get; set; }
        public string Teacherfile { get; set; }
        public string Studentfile { get; set; }
        public Nullable<bool> Reportable { get; set; }
        public Nullable<bool> PaymentSystems { get; set; }
        public Nullable<bool> CustSigned { get; set; }
        public Nullable<int> AccountStatusID { get; set; }

        public string SchoolStatus { get; set; }

        public Nullable<int> CreatedBy { get; set; }

        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDateTime { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDateTime { get; set; }

        public string IsActivationID { get; set; }
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

        public string Password { get; set; }

        public string ParentPhone { get; set; }

        public string ParentRelation { get; set; }

        public Nullable<int> SchoolID { get; set; }

    }
}