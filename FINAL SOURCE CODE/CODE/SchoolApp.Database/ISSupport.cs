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
    
    public partial class ISSupport
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ISSupport()
        {
            this.ISTicketMessages = new HashSet<ISTicketMessage>();
        }
    
        public int ID { get; set; }
        public string TicketNo { get; set; }
        public string Request { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<int> LogTypeID { get; set; }
        public Nullable<int> SupportOfficerID { get; set; }
        public Nullable<int> Priority { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDateTime { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDateTime { get; set; }
        public string AssignBy { get; set; }
        public Nullable<System.DateTime> AssignDate { get; set; }
    
        public virtual ISLogType ISLogType { get; set; }
        public virtual ISSupportStatu ISSupportStatu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ISTicketMessage> ISTicketMessages { get; set; }
    }
}