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
    
    public partial class JariCompany
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JariCompany()
        {
            this.FinanceSales = new HashSet<FinanceSale>();
            this.ShadeCards = new HashSet<ShadeCard>();
            this.TranDetails = new HashSet<TranDetail>();
            this.TranHeaders = new HashSet<TranHeader>();
            this.TranJournals = new HashSet<TranJournal>();
            this.Users = new HashSet<User>();
            this.EmbroideryFirmPriceSettings = new HashSet<EmbroideryFirmPriceSetting>();
            this.EmbroideryFirms = new HashSet<EmbroideryFirm>();
            this.EmbroideryFirmLocations = new HashSet<EmbroideryFirmLocation>();
        }
    
        public int JariCompanyId { get; set; }
        public string JariCompanyName { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ActivationEndDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FinanceSale> FinanceSales { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShadeCard> ShadeCards { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TranDetail> TranDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TranHeader> TranHeaders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TranJournal> TranJournals { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmbroideryFirmPriceSetting> EmbroideryFirmPriceSettings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmbroideryFirm> EmbroideryFirms { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmbroideryFirmLocation> EmbroideryFirmLocations { get; set; }
    }
}
