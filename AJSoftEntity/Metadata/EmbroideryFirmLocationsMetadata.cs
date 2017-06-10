using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJSoftEntity
{
    [MetadataType(typeof(EmbroideryFirmLocationsMetadata))]
    public partial class EmbroideryFirmLocation
    {
    }


    public class EmbroideryFirmLocationsMetadata
    {
        [Required(ErrorMessage = "Please enter Contact Person")]
        public string ContactPerson { get; set; }

        [Required(ErrorMessage = "Please enter Phone")]
        public string Phone { get; set; }
    }
}
