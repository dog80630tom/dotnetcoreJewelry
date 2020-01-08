﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jewelry.Models
{
    public class Category
    {
         [Key]
         public int CategoryID{get; set;}
         public string CategoryName{get; set;}
         //public string CategoryPic{get; set;}
         public string CategoryDescription{get; set;}
        public int? ParentID { get; set; }
    }
}