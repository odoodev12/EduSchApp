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
    
    public partial class ISAdminLogin
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDateTime { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDateTime { get; set; }
        public string FullName { get; set; }
        public Nullable<bool> ISActivated { get; set; }
        public string IsActivationID { get; set; }
        public string MemorableQueAnswer { get; set; }
        public Nullable<bool> IsResetPassword { get; set; }
    }
}