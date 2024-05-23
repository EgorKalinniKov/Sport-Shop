using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Web.Models.Product
{
    public class ReviewData//Модель отзыва для товара
    {
        public string UserName { get; set; }
        public string Review { get; set;}
        public DateTime DateCreated { get; set; }
    }
}