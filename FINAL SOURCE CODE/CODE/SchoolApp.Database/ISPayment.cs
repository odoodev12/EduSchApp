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
    
    public partial class ISPayment
    {
        public int ID { get; set; }
        public string SchoolID { get; set; }
        public string InvoiceID { get; set; }
        public string Amount { get; set; }
        public string TransactionTypeID { get; set; }
        public string Description { get; set; }
        public string StatusID { get; set; }
        public System.DateTime FromDate { get; set; }
        public System.DateTime ToDate { get; set; }
    }
}
