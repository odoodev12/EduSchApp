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
    
    public partial class ISMessage
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Attechment { get; set; }
        public string Desc { get; set; }
        public Nullable<int> ReceiveID { get; set; }
        public Nullable<int> SendID { get; set; }
        public Nullable<int> ReceiverType { get; set; }
        public Nullable<int> SenderType { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public Nullable<System.DateTime> Time { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDateTime { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDateTime { get; set; }
    }
}
