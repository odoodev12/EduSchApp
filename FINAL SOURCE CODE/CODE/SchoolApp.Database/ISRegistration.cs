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
    
    public partial class ISRegistration
    {
        public int ID { get; set; }
        public string BillingAddress { get; set; }
        public string SupervisorName { get; set; }
        public string SupervisorEmail { get; set; }
        public string SchoolNumber { get; set; }
        public string MobileNumber { get; set; }
        public string SchoolAddress { get; set; }
        public string SchoolWebsite { get; set; }
        public string SchoolOpeningTime { get; set; }
        public string SchoolClosingTime { get; set; }
        public string LastMinutesAfterClosing { get; set; }
        public string ChargeMinutesAfterClosing { get; set; }
        public string ReportMinutesAfterClosing { get; set; }
        public Nullable<bool> IsNotificationAfterAttendance { get; set; }
        public Nullable<bool> IsNotificationAfterPickup { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public string FilePath { get; set; }
    }
}
