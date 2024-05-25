using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Web.Models.Product
{
    public class ProductIconData//Модель для товара
    {
        public string Article { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public double PriceWithDiscount { get; set; }
        public bool AvailableStatus { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public int TotalRatings { get; set; }
        public double AvarageRating { get; set; }

    }
}