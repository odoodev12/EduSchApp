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
    
    public partial class ISOrganisationUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ISOrganisationUser()
        {
            this.ISSchools = new HashSet<ISSchool>();
        }
    
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OrgCode { get; set; }
        public string Photo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Town { get; set; }
        public Nullable<int> CountryID { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> RoleID { get; set; }
        public string Password { get; set; }
        public string StatusID { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDateTime { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDateTime { get; set; }
        public string CreatedByName { get; set; }
        public string ActivationBy { get; set; }
        public Nullable<System.DateTime> ActivationDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<bool> ISActivated { get; set; }
        public string IsActivationID { get; set; }
        public string MemorableQueAnswer { get; set; }
        public Nullable<bool> IsResetPassword { get; set; }
    
        public virtual ISCountry ISCountry { get; set; }
        public virtual ISRole ISRole { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ISSchool> ISSchools { get; set; }
    }
}
