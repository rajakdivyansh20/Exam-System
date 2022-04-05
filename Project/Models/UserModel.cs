using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class UserModel
    {
        [Required(ErrorMessage ="Mandatory")]
        public string Name { get; set; }


        [MaxLength(10)]
        public string Contact { get; set; }


        [Required(ErrorMessage = "Mandatory")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }



        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Mandatory")]
        public string Password { get; set; }


    }
}