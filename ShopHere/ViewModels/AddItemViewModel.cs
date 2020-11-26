using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopHere.Models;

namespace ShopHere.ViewModels
{
    public class AddItemViewModel
    {
        public Item Item { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}