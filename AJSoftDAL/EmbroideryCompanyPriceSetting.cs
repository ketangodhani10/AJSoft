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
    
    public partial class EmbroideryCompanyPriceSetting
    {
        public int CompanyPriceSettingsId { get; set; }
        public int JariCompanyId { get; set; }
        public int ShadeId { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    
        public virtual JariCompany JariCompany { get; set; }
        public virtual ShadeCard ShadeCard { get; set; }
    }
}