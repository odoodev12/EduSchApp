using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.ClassLibrary
{
    public class MErrorFrames { public string FileName { get; set; } public string LineNumber { get; set; } public string ColumnNumber { get; set; } public string Method { get; set; } public string Class { get; set; } }
    public class MISAdminLogin : ISAdminLogin { public string UserTypeName { get; set; } public string CreateDate { get; set; } };
    public class MISStudent : ISStudent { public List<MISSchool1> ObjList { get; set; } public int StudentId { get; set; } public double? StudentPickUpAverage { get; set; }  public string PickStatus { get; set; } public string StudentPic { get; set; } public int TypeID { get; set; } public string SchoolNumber { get; set; } public string SchoolName { get; set; } public string ClassName { get; set; } public string ParentName { get; set; } public string ParentPhone { get; set; } public string ParentEmail { get; set; } public string Password { get; set; } public string ParentRelation { get; set; } public string ParentPassword { get; set; } public string ParentPhoto { get; set; } public int? School_ID { get; set; } public int? PickUpAverage { get; set; } public string StrPickUpAverage { get; set; } public string AttStatus { get; set; } public string AttDate { get; set; } public DateTime? AttDates { get; set; } public int TotalStudent { get; set; } public string CreateDate { get; set; } } // public string PickMessageTime { get; set; } public string PickMessageDate { get; set; } public string LastAttendenceEmailDateStr { get; set; } public string LastAttendenceEmailTimeStr { get; set; } public string LastUnPickAlertSentTimeStr { get; set; } public string UnPickAlertDateStr { get; set; }
    public class MISTeacher : ISTeacher
    {
        public List<ISTeacherClassAssignment> ObjList { get; set; }
        public object ObjString { get; set; }
        public string TeacherEndDate { get; set; }
        public string ClassName { get; set; }
        public string RoleName { get; set; }
        public string RoleType { get; set; }
        public string AssignedClass { get; set; }
        public string AssignedClassYear { get; set; }
        public string ClassYear { get; set; }
        public int? ClassID1 { get; set; }
        public int? ClassID2 { get; set; }
        public int? ClassID3 { get; set; }
        public int? ClassID4 { get; set; }
        public int? ClassID5 { get; set; }
        public string ClassName1 { get; set; }
        public string ClassName2 { get; set; }
        public string ClassName3 { get; set; }
        public string ClassName4 { get; set; }
        public string ClassName5 { get; set; }
        public int? SchoolType { get; set; }
        public string CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string Status { get; set; }
        public string RequestParameter { get; set; }
}
    public class MISPickup : ISPickup
    {
        public int? StudentPickUpAverage { get; set; }
        public int? ParentMessage { get; set; }
        public int? PickUpAverage { get; set; }
        public string StudentName { get; set; }
        public string PickDateStr { get; set; }
        public int? ClassID { get; set; }
        public string Status { get; set; }
        public string StudentPic { get; set; }
        public string PickerName { get; set; }
        public string Pick_Date { get; set; }
        public string Pick_Time { get; set; }
        public string SchoolName { get; set; }
        public string TeacherName { get; set; }
        public string StNo { get; set; }
        public string ClassName { get; set; }
    }

    public class PickerOtherDetail
    {
        public ISPicker ISPicker { get; set; }

        public bool IsImageUploaded { get; set; }

        public bool IsValidPickerFor7Days { get; set; }
    }

    public class MISPICKER : ISPicker { public string PickerName { get; set; } public string ParentName { get; set; } public string SchoolName { get; set; } public string StrPickerType { get; set; } public bool IsPickerCreatedByMe { get; set; } }
    public class MISTeacherClassAssignment : ISTeacherClassAssignment { public int ClassTypeID { get; set; } public string ClassName { get; set; } public string TeacherName { get; set; } public int SchoolID { get; set; } }
    public class MISAttendance : ISAttendance { public int? AttendenceID { get; set; } public string StudentName { get; set; } public string StudentPic { get; set; } public int? ClassID { get; set; } public string TeacherName { get; set; } public string MarkedDate { get; set; } public string MarkedTime { get; set; } public int? SchoolID { get; set; } }

    public class MISClass : ISClass { public string Status { get; set; } public string EndDateString { get; set; } public string YearName { get; set; } public string ClassType { get; set; } public int StudentCount { get; set; } public string TeacherName { get; set; } public string CreateBy { get; set; } }
    public class MISUserRole : ISUserRole { public string Status { get; set; } public string RoleTypes { get; set; } public string strCreateBy { get; set; } public string strCreatedDate { get; set; } public string ActivatedFunctions { get; set; } public bool IsDefaultRole { get; set; } }
    public class MISSchool : ISSchool { public string OpeningTimeStr { get; set; } public string ClosingTimeStr { get; set; } public string SchoolType { get; set; } public string CountryName { get; set; } public string BillingCountryName { get; set; } public string SchoolStatus { get; set; } public string IsNotificationPickupStr { get; set; } public string NotificationAttendenceStr { get; set; } public string StrAccountStatus { get; set; } public string StrActivationDate { get; set; } public string StrModifyDate { get; set; } public string StrLastActivationDate { get; set; } public bool? ActiveStatus { get; set; } public string StrCreatedDate { get; set; } }
    public class MISSchoolInvoice : ISSchoolInvoice { public string SchoolName { get; set; } public string TaxRates { get; set; } public string strTransectionType { get; set; } public string strStatus { get; set; } public string InvoiceNumber { get; set; } public string FromDate { get; set; } public string ToDate { get; set; } public string Description { get; set; } public string Transaction_Amount { get; set; } public string strCreatedDate { get; set; } public string StrStatusUpdatedDate { get; set; } }
    public class Responses
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public object Data1 { get; set; }
    }
    public class MISUserActivity : ISUserActivity { public string SchoolName { get; set; } public string StrDate { get; set; } public string UpdatedBy { get; set; } public string ShortLogText { get; set; } }
    public class MISSupport : ISSupport { public string TicketNumber { get; set; } public string Subject { get; set; } public string SDate { get; set; } public string STime { get; set; } public string Status { get; set; } public string SupportOfficer { get; set; } public string Priorities { get; set; } public string StrAssignDate { get; set; } }
    public class MISOrganisationUser : ISOrganisationUser { public string RoleName { get; set; } public string Status { get; set; } public string CountryName { get; set; } public string StrActivationDate { get; set; } public string UpdatedDate { get; set; } public string StrCreatedDate { get; set; } }
    public class MISClassType : ISClassType { public EnumsManagement.CLASSTYPE ClassType { get; set; } }
    public class MISSchoolType : ISSchoolType { }
    public class MISAfterSchoolType { public int ID { get; set; } public string AfterSchoolType { get; set; } }
    public class MISTeacherTitle { public int ID { get; set; } public string Title { get; set; } }
    public class MISRelation { public int ID { get; set; } public string Relation { get; set; } }
    public class MISRoleType { public int ID { get; set; } public string RoleType { get; set; } }
    public class MISMessage : ISMessage
    {
        public string Receiver { get; set; }
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string STime { get; set; }
        public string Description { get; set; }
    }
    public class MISSupportStatus : ISSupportStatu { }
    public class MISLogType : ISLogType { }
    public class MISPickerAssignment : ISPickerAssignment { public string PickerName { get; set; } public string PickerEmail { get; set; } public string PickerPhone { get; set; } public string AssignBy { get; set; } public string Status { get; set; } public string Photo { get; set; } public string StrDate { get; set; } public string StrStudentPickAssignDate { get; set; } public string CurrentDate { get; set; } public bool? Count { get; set; } public int? PickerType { get; set; } public string CreatedByEmail { get; set; } public string CreatedByName { get; set; } public string BorderColorCode { get; set; } public bool IsRedZone { get; set; } public string CodeRequiredText { get; set; } }
    public class MViewStudentPickUp : ViewStudentPickUp
    {
        public double? StudentPickUpAverage { get; set; }
        public int? ParentMessage { get; set; }
        public string PickUpAverage { get; set; }
        public string PickDateStr { get; set; }
        public string Status { get; set; }
        public string PickerName { get; set; }
        public string Pick_Date { get; set; }
        public string Pick_Time { get; set; }
        public string SchoolName { get; set; }
        public string StudentPic { get; set; }
        public string PickerImage { get; set; }
        public int SchoolType { get; set; }
        public string ParantPhone1 { get; set; }
        public string ParantPhone2 { get; set; }
        public string StartDates { get; set; }
        public String StudentPickUpAverageStr { get; set; }
        public string AfterSchoolName { get; set; }
        public bool AfterSchoolFlag { get; set; }
        public bool OfficeFlag { get; set; }
        public bool ClubFlag { get; set; }
        public bool CompletePickup { get; set; }
        public bool IsCodeRequired { get; set; }

    }
    public class MISSchool1 { public int? SchoolID { get; set; } public string SchoolName { get; set; } public string SchoolPhoneNumber { get; set; } }
    public class MISTicketMessage { public string FileName { get; set; } public DateTime? CreatedDate { get; set; } public string CreateDate { get; set; } public string SName { get; set; } public string Messages { get; set; } }
    public class MISStudentBulk : ISStudent { public string Status { get; set; } public string ClassName { get; set; } public string Command { get; set; } }
    public class MISClassBulk : ISClass { public string Status { get; set; } public string Command { get; set; } }
    public class MISTeacherBulk : ISTeacher { public string Command { get; set; } public string Status { get; set; } public string FirstClass { get; set; } public string SecondClass { get; set; } public string RoleName { get; set; } }
    public class MISHoliday : ISHoliday { public string FromDate { get; set; } public string ToDate { get; set; } public string ActiveStatus { get; set; } }
    public class MISPickUpMessage : ISPickUpMessage { public string StrDate { get; set; } }
    public class MISPickerType { public int ID { get; set; } public string PickerType { get; set; } }
    public class MISTrasectionStatus : ISTrasectionStatu { }
    public class MGetAttendenceData
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
    public class MISPickUpStatus : ISPickUpStatu
    {

    }
    public class MISTeacherPickHistory
    {
        public int? TeacherID { get; set; }
        public string TeacherName { get; set; }
        public DateTime? PickDate { get; set; }
        public string PickDateStr { get; set; }
        public int? ClassID { get; set; }
        public string ClassName { get; set; }
        public int NoOfClassStudent { get; set; }
        public int NoOfProcessedStudent { get; set; }
    }
    public class MISCompletePickupRun : ISCompletePickupRun { public string ClassName { get; set; } public string TeacherName { get; set; } public string CompletePickTime { get; set; } public string DateStr { get; set; } }
    public class MISCompleteAttendanceRun : ISCompleteAttendanceRun
    {
        public string ClassName { get; set; }
        public string TeacherName { get; set; }
        public string A_Date { get; set; }
        public string A_Time { get; set; }
        public string TeacherNo { get; set; }

    }
    public class MISStudentReassignHistory : ISStudentReassignHistory
    {
        public string ClassFrom { get; set; }
        public string ClassTo { get; set; }
        public string ReAssignedBy { get; set; }
        public string StudentNo { get; set; }
        public string StudentName { get; set; }
        public string DateStr { get; set; }
    }
    public class MISTeacherReassignHistory : ISTeacherReassignHistory
    {
        public string ClassFrom { get; set; }
        public string ClassTo { get; set; }
        public string ReAssignedBy { get; set; }
        public string TeacherName { get; set; }
        public string DateStr { get; set; }
    }
    public class MISDataUploadHistory : ISDataUploadHistory
    {
        public string SchoolName { get; set; }
        public string DateStr { get; set; }
    }
}
