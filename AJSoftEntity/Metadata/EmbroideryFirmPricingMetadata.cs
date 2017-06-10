using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJSoftEntity
{
    [MetadataType(typeof(EmbroideryFirmPricingMetadata))]
    public partial class EmbroideryFirmPriceSetting
    {

    }


    public class EmbroideryFirmPricingMetadata
    {
        [Required(ErrorMessage = "Please Enter Price")]
        [DataType(DataType.Currency, ErrorMessage = "Please Enter Valid Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please Select Start Date")]
        public Nullable<System.DateTime> StartDate { get; set; }

    }
}
