using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class QuestionModel
    {
        public int QID { get; set; }
        public string QSubject { get; set; }
        public string Question { get; set; }
        public string Option { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
    }
}