//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AJSoftEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class vw_Companies
    {
        public System.Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<System.Guid> UserId { get; set; }
        public Nullable<int> RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public Nullable<System.Guid> UserCompanyId { get; set; }
        public string Status { get; set; }
        public Nullable<System.Guid> CompanyLocationId { get; set; }
        public string LocationName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public Nullable<bool> IsPrimaryLocation { get; set; }
        public Nullable<int> NoOfLocation { get; set; }
        public Nullable<int> NoOfUser { get; set; }
        public Nullable<decimal> Revenue { get; set; }
        public Nullable<int> BillingTerms { get; set; }
    }
}