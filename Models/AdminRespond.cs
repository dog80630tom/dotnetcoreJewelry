using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jewelry.Models
{
    public class AdminRespond
    {
        [Key]
        public int CustomerQuestionID { get; set; }
        [Key]
        public int AdminID { get; set; }
        public string AdminRespond1 { get; set; }
        public System.DateTime AdminRespondTime { get; set; }
    }
}