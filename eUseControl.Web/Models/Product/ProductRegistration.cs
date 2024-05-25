using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Web.Models.Product
{
    public class ProductRegistration//Модель для регистрации товара
    {
        public string Name { get; set; }
        public string Article { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Brend { get; set; }
        public string Category { get; set; }
        public string Tag { get; set; }
        public List<HttpPostedFileBase> Imgs { get; set; }
        public string Directory { get; set; }
        public bool AvailableStatus { get; set; }
    }
}