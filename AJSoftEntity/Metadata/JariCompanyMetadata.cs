using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJSoftEntity
{

    [MetadataType(typeof(JariCompanyMetadata))]
    public partial class JariCompany
    {

    }


    public class JariCompanyMetadata
    {
        [Required(ErrorMessage = "Please Enter Jari Company Name")]
        public string JariCompanyName { get; set; }

        [Required(ErrorMessage = "Please Select Create Date")]
        public System.DateTime CreateDate { get; set; }

        [Required(ErrorMessage = "Please select Activation End Date")]
        public System.DateTime ActivationEndDate { get; set; }
    }
}
