//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolApp.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class ISPickup
    {
        public int ID { get; set; }
        public Nullable<int> StudentID { get; set; }
        public Nullable<int> ClassID { get; set; }
        public Nullable<int> TeacherID { get; set; }
        public Nullable<int> PickerID { get; set; }
        public Nullable<System.DateTime> PickTime { get; set; }
        public Nullable<System.DateTime> PickDate { get; set; }
        public string PickStatus { get; set; }
        public Nullable<bool> CompletePickup { get; set; }
        public Nullable<bool> OfficeFlag { get; set; }
        public Nullable<bool> AfterSchoolFlag { get; set; }
        public Nullable<bool> ClubFlag { get; set; }
    
        public virtual ISPicker ISPicker { get; set; }
        public virtual ISStudent ISStudent { get; set; }
        public virtual ISTeacher ISTeacher { get; set; }
    }
}