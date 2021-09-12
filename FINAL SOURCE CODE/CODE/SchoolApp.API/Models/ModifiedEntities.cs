using Newtonsoft.Json;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolApp.API.Models
{

    public class SchoolType : ISSchoolType
    {
        [JsonIgnore]
        public new virtual ISAccountStatu ISAccountStatu { get; set; }
        [JsonIgnore]
        public new virtual ISCountry ISCountry { get; set; }

        [JsonIgnore]
        public new virtual ICollection<ISHoliday> ISHolidays { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISPicker> ISPickers { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISPicker> ISPickers1 { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISStudent> ISStudents { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISTeacher> ISTeachers { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISUserActivity> ISUserActivities { get; set; }
        [JsonIgnore]
        public new virtual ISSchool ISSchool1 { get; set; }
        [JsonIgnore]
        public new virtual ISSchool ISSchool2 { get; set; }
        [JsonIgnore]
        public new virtual ISSchoolType ISSchoolType { get; set; }
        [JsonIgnore]

        public new virtual ICollection<ISSchoolInvoice> ISSchoolInvoices { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISUserRole> ISUserRoles { get; set; }
        [JsonIgnore]
        public new virtual ISOrganisationUser ISOrganisationUser { get; set; }
    }

    public class School : ISSchool
    {
        [JsonIgnore]
        public new virtual ISAccountStatu ISAccountStatu { get; set; }
        [JsonIgnore]
        public new virtual ISCountry ISCountry { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISHoliday> ISHolidays { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISPicker> ISPickers { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISPicker> ISPickers1 { get; set; }
        [JsonIgnore]
        public new  virtual ICollection<ISStudent> ISStudents { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISTeacher> ISTeachers { get; set; }
        [JsonIgnore]
        public new virtual ISSchool ISSchool1 { get; set; }
        [JsonIgnore]
        public new virtual ISSchool ISSchool2 { get; set; }
        [JsonIgnore]
        public new virtual ISSchoolType ISSchoolType { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISSchoolInvoice> ISSchoolInvoices { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISUserRole> ISUserRoles { get; set; }
        [JsonIgnore]
        public new virtual ISOrganisationUser ISOrganisationUser { get; set; }

    }

    public class Classes : ISClass
    {
        [JsonIgnore]
        public new virtual ICollection<ISCompleteAttendanceRun> ISCompleteAttendanceRuns { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISCompletePickupRun> ISCompletePickupRuns { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISStudent> ISStudents { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISTeacherClassAssignment> ISTeacherClassAssignments { get; set; }
        [JsonIgnore]
        public new virtual ISClassType ISClassType { get; set; }

    }

    public class Teacher : ISTeacher
    {
        [JsonIgnore]
        public new virtual ICollection<ISAttendance> ISAttendances { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISCompleteAttendanceRun> ISCompleteAttendanceRuns { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISCompletePickupRun> ISCompletePickupRuns { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISPickup> ISPickups { get; set; }
        [JsonIgnore]
        public new virtual ISSchool ISSchool { get; set; }
        [JsonIgnore]
        public new virtual ISUserRole ISUserRole { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISTeacherClassAssignment> ISTeacherClassAssignments { get; set; }
    }

    public class SchoolOtherEntity
    {
        public SchoolOtherEntity()
        {
            this.ISHolidays = new HashSet<ISHoliday>();
            this.ISPickers = new HashSet<ISPicker>();
            this.ISPickers1 = new HashSet<ISPicker>();
            this.ISStudents = new HashSet<ISStudent>();
            this.ISTeachers = new HashSet<ISTeacher>();
            this.ISUserActivities = new HashSet<ISUserActivity>();
            this.ISSchoolInvoices = new HashSet<ISSchoolInvoice>();
            this.ISUserRoles = new HashSet<ISUserRole>();
        }
        public int ID { get; set; }
        public string CustomerNumber { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public Nullable<int> TypeID { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Town { get; set; }
        public Nullable<int> CountryID { get; set; }
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
        public string LateMinAfterClosing { get; set; }
        public string ChargeMinutesAfterClosing { get; set; }
        public string ReportableMinutesAfterClosing { get; set; }
        public Nullable<bool> SetupTrainingStatus { get; set; }
        public Nullable<System.DateTime> SetupTrainingDate { get; set; }
        public Nullable<System.DateTime> ActivationDate { get; set; }
        public Nullable<System.DateTime> SchoolEndDate { get; set; }
        public Nullable<bool> isAttendanceModule { get; set; }
        public Nullable<bool> isNotificationPickup { get; set; }
        public Nullable<bool> NotificationAttendance { get; set; }
        public string AttendanceModule { get; set; }
        public string PostCode { get; set; }
        public string BillingAddress { get; set; }
        public string BillingAddress2 { get; set; }
        public string BillingPostCode { get; set; }
        public Nullable<int> BillingCountryID { get; set; }
        public string BillingTown { get; set; }
        public string Classfile { get; set; }
        public string Teacherfile { get; set; }
        public string Studentfile { get; set; }
        public Nullable<bool> Reportable { get; set; }
        public Nullable<bool> PaymentSystems { get; set; }
        public Nullable<bool> CustSigned { get; set; }
        public Nullable<int> AccountStatusID { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDateTime { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDateTime { get; set; }
        public string CreatedByName { get; set; }
        public string LastUpdatedBy { get; set; }
        public string ActivatedBy { get; set; }
        public Nullable<int> AccountManagerId { get; set; }
        public string MemorableQueAnswer { get; set; }
        public Nullable<bool> ISActivated { get; set; }
        public string IsActivationID { get; set; }
        public Nullable<bool> IsEmail_Required_For_Create_Class { get; set; }
        public Nullable<bool> IsEmail_Required_For_Edit_Class { get; set; }
        public Nullable<bool> IsEmail_Required_For_Create_Student { get; set; }
        public Nullable<bool> IsEmail_Required_For_Edit_Student { get; set; }
        public Nullable<bool> IsEmail_Required_For_Create_Teacher { get; set; }
        public Nullable<bool> IsEmail_Required_For_Edit_Teacher { get; set; }
        public Nullable<bool> IsEmail_Required_For_Create_Role { get; set; }
        public Nullable<bool> IsEmail_Required_For_Edit_Role { get; set; }
        public Nullable<bool> IsEmail_Required_For_Create_Holiday { get; set; }
        public Nullable<bool> IsEmail_Required_For_Edit_Holiday { get; set; }
        public Nullable<bool> IsBell { get; set; }
        [JsonIgnore]
        public new  virtual ISAccountStatu ISAccountStatu { get; set; }
        [JsonIgnore]
        public new  virtual ISCountry ISCountry { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISHoliday> ISHolidays { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISPicker> ISPickers { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISPicker> ISPickers1 { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISStudent> ISStudents { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISTeacher> ISTeachers { get; set; }
        [JsonIgnore]
        public new  virtual ICollection<ISUserActivity> ISUserActivities { get; set; }
        [JsonIgnore]
        public new virtual ISSchool ISSchool1 { get; set; }
        [JsonIgnore]
        public new virtual ISSchool ISSchool2 { get; set; }
        [JsonIgnore]
        public new virtual ISSchoolType ISSchoolType { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISSchoolInvoice> ISSchoolInvoices { get; set; }

        [JsonIgnore]
        public new virtual ICollection<ISUserRole> ISUserRoles { get; set; }
        [JsonIgnore]
        public new virtual ISOrganisationUser ISOrganisationUser { get; set; }
        

    }


    public class Students : ISStudent
    {

        [JsonIgnore]
        public new virtual ICollection<ISAttendance> ISAttendances { get; set; }
        [JsonIgnore]
        public new virtual ISClass ISClass { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISPicker> ISPickers { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISPicker> ISPickers1 { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISPickup> ISPickups { get; set; }
        [JsonIgnore]
        public new virtual ISSchool ISSchool { get; set; }
    }



    




}