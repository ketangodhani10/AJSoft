using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AJSoftEntity.Classes
{
    public class EmbFirmLocationVM
    {
        public int JariCompanyId { get; set; }
        public System.Guid EmbroideryFirmId { get; set; }
        public System.Guid EmbroideryFirmLocationId { get; set; }

        [Required(ErrorMessage = "Please enter Embroidery Firm Name")]
        [Remote("checkEmbroideryFirmName", "EmbroideryFirm", AdditionalFields = "EmbroideryFirmId,JariCompanyId", HttpMethod = "POST", ErrorMessage = "Firm name already used. Please use a different Firm name")]
        public string EmbroideryFirmName { get; set; }

        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Please enter Contact Person")]
        public string ContactPerson { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }

        public bool IsPrimaryLocation { get; set; }
        public bool Status { get; set; }

        [Required(ErrorMessage = "Please enter Phone")]
        public string Phone { get; set; }
        public string Email { get; set; }
        public Nullable<int> BillingTerms { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Longitude { get; set; }

    }
}
