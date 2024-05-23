using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Web.Models
{
    public class RatingData//Модель рейтинга товара
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public int TotalRatings { get; set; }
        public double AverageRating { get; set; }
    }
}