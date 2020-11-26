using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopHere.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public DateTime OrderPlacedTime { get; set; }
        public Item Item { get; set; }
        public int ItemId { get; set; }
        public bool IsDelivered { get; set; }
    }
}