using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AJSoftEntity
{
    [MetadataType(typeof(UsersMetadata))]
    public partial class User
    {

    }


    public class UsersMetadata
    {
        [Required(ErrorMessage = "Please Select Role")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Please Select Jari Company")]
        public Nullable<int> JariCompanyId { get; set; }

        [Required(ErrorMessage = "Please Enter First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        [Remote("checkUserEmail", "Users", AdditionalFields = "UserId", HttpMethod = "POST", ErrorMessage = "Email address already used. Please use a different e-mail address")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter valid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Mobile")]
        public string Mobile { get; set; }
    }
}
