using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jewelry.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderID
        {
            get; set;
        }
        [Key]
        public int ProductID
        {
            get; set;
        }
        public int Quantity
        {
            get; set;
        }
        public int Discount
        {
            get; set;
        }
        public bool IsWish
        {
            get; set;
        }

        public int MemberID
        {
            get; set;
        }

        //public virtual Order Order { get; set; }
        //public virtual Product Product { get; set; }
    }
}