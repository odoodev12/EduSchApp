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
    
    public partial class ISTicketMessage
    {
        public int ID { get; set; }
        public Nullable<int> SupportID { get; set; }
        public Nullable<int> SenderID { get; set; }
        public string Message { get; set; }
        public string SelectFile { get; set; }
        public Nullable<System.DateTime> CreatedDatetime { get; set; }
        public Nullable<int> UserTypeID { get; set; }
    
        public virtual ISSupport ISSupport { get; set; }
    }
}
