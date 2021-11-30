using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolApp.API.Models.ViewModels
{
    public class AddStudent
    {
        public int Id { get; set; }
        public int adminId { get; set; }
        public string StudentName { get; set; }
        public int SchoolID { get; set; }
        public string Photo { get; set; }
        public DateTime? DOB { get; set; }
        public string ParantName1 { get; set; }
        public string ParantName2 { get; set; }
        public string ParantEmail1 { get; set; }
        public string ParantEmail2 { get; set; }
        public string ParantPassword1 { get; set; }
        public string ParantPassword2 { get; set; }
        public string ParantPhone1 { get; set; }
        public string ParantPhone2 { get; set; }
        public string ParantRelation1 { get; set; }
        public string ParantRelation2 { get; set; }
        public string ParantPhoto1 { get; set; }
        public string ParantPhoto2 { get; set; }
        public string PickupMessageID { get; set; }
        public Nullable<System.TimeSpan> PickupMessageTime { get; set; }
        public Nullable<System.DateTime> PickupMessageDate { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }

    }

    public class AddEditStudentModel : AddStudent
    {
        public int SchoolTypeID { get; set; }
        public string StudentNo { get; set; }
        public int ClassID { get; set; }
        public int LoginUserId { get; set; }
        public int LoginUserTypeId { get; set; }
    }
}