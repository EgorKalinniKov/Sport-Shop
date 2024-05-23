using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Web.Models.Product
{
    public class ProductData//Модель для товара
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Categories { get; set; }
        public List<string> SubCategories { get; set; }
        public int Cost { get; set; }
        public List<ReviewData> Reviews { get; set; }
        public int TotalRatings { get; set; }
        public int Rating { get; set; }
        public double AvarageRating { get; set; }
        public bool AvailableStatus { get; set; }
    }
}