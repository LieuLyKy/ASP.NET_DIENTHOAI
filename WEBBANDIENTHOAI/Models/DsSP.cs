using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBBANDIENTHOAI.Models
{
    public class DsSP
    {
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal? PriceSale { get; set; }
        public DateTime? NgayDat { get; set; }
    }
}