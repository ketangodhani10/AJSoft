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
    
    public partial class vw_ShadeCards
    {
        public int ShadeId { get; set; }
        public int JariCompanyId { get; set; }
        public int YarnTypeId { get; set; }
        public string ShadeName { get; set; }
        public byte[] ShadeImage { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string YarnTypeName { get; set; }
        public string YarnColorCode { get; set; }
        public string JariCompanyName { get; set; }
    }
}