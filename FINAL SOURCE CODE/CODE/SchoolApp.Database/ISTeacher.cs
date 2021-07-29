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
    
    public partial class ISTeacher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ISTeacher()
        {
            this.ISAttendances = new HashSet<ISAttendance>();
            this.ISCompleteAttendanceRuns = new HashSet<ISCompleteAttendanceRun>();
            this.ISCompletePickupRuns = new HashSet<ISCompletePickupRun>();
            this.ISPickups = new HashSet<ISPickup>();
            this.ISTeacherClassAssignments = new HashSet<ISTeacherClassAssignment>();
        }
    
        public int ID { get; set; }
        public int SchoolID { get; set; }
        public Nullable<int> ClassID { get; set; }
        public int RoleID { get; set; }
        public string TeacherNo { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string Photo { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public Nullable<int> CreatedByType { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDateTime { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDateTime { get; set; }
        public Nullable<int> Role { get; set; }
        public Nullable<bool> ISActivated { get; set; }
        public string IsActivationID { get; set; }
        public string MemorableQueAnswer { get; set; }
        public Nullable<bool> IsBell { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ISAttendance> ISAttendances { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ISCompleteAttendanceRun> ISCompleteAttendanceRuns { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ISCompletePickupRun> ISCompletePickupRuns { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ISPickup> ISPickups { get; set; }
        public virtual ISSchool ISSchool { get; set; }
        public virtual ISUserRole ISUserRole { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ISTeacherClassAssignment> ISTeacherClassAssignments { get; set; }
    }
}