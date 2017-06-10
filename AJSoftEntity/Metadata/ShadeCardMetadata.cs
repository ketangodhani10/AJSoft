using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJSoftEntity
{

    [MetadataType(typeof(ShadeCardMetadata))]
    public partial class ShadeCard
    {

    }


    public class ShadeCardMetadata
    {
        [Required(ErrorMessage = "Please Enter Shade Name")]
        public string ShadeName { get; set; }

        [Required(ErrorMessage = "Please select Yarn Type")]
        public int YarnTypeId { get; set; }

        [Required(ErrorMessage = "Please Enter Price")]
        [DataType(DataType.Currency, ErrorMessage = "Please Enter Valid Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please Select Start Date")]
        public Nullable<System.DateTime> StartDate { get; set; }

        [RegularExpression(@"[0-9]+", ErrorMessage = "Please Enter Number Only.")]
        public Nullable<int> DisplayOrder { get; set; }
    }
}
