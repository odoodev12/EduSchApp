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
    
    public partial class ISInvoice
    {
        public int ID { get; set; }
        public Nullable<int> PropertyID { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<System.DateTime> DateFrom { get; set; }
        public Nullable<System.DateTime> DateTo { get; set; }
        public string TransactionDesc { get; set; }
        public Nullable<int> TransactionAmount { get; set; }
        public Nullable<decimal> TaxRate { get; set; }
        public Nullable<int> StatusID { get; set; }
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