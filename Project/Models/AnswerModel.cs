using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class AnswerModel
    {
        [Required]
        public string subject { get; set; }
        [Required]
        public string AnswerofQuestion1 { get; set; }
        [Required]
        public string AnswerofQuestion2 { get; set; }
        [Required]
        public string AnswerofQuestion3 { get; set; }
        [Required]
        public string AnswerofQuestion4 { get; set; }
    }

}