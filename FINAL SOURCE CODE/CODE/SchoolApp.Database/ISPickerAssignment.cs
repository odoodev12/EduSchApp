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
    
    public partial class ISPickerAssignment
    {
        public int ID { get; set; }
        public Nullable<int> PickerId { get; set; }
        public Nullable<int> StudentID { get; set; }
        public string PickerCode { get; set; }
        public Nullable<System.DateTime> PickCodeExDate { get; set; }
        public Nullable<System.DateTime> PickCodeLastUpdateDate { get; set; }
        public Nullable<System.DateTime> PickCodayExDate { get; set; }
        public Nullable<System.DateTime> PickTodayLastUpdateDate { get; set; }
        public string PickTodayLastUpdateParent { get; set; }
        public Nullable<int> RemoveChildStatus { get; set; }
        public Nullable<System.DateTime> RemoveChildLastUpdateDate { get; set; }
        public string RemoveChildLastupdateParent { get; set; }
        public Nullable<int> StudentPickAssignFlag { get; set; }
        public string StudentPickAssignLastUpdateParent { get; set; }
        public Nullable<System.DateTime> StudentPickAssignDate { get; set; }
        public string StudentAssignBy { get; set; }
    }
}