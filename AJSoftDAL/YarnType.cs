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
    
    public partial class YarnType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public YarnType()
        {
            this.ShadeCards = new HashSet<ShadeCard>();
        }
    
        public int YarnTypeId { get; set; }
        public string YarnTypeName { get; set; }
        public string YarnColorCode { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShadeCard> ShadeCards { get; set; }
    }
}
