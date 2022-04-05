using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class SubjectModel
    {
        [Required]
        public int SubjectID { get; set; }
        [Required]
        public string SubjectName { get; set; }
    }
}