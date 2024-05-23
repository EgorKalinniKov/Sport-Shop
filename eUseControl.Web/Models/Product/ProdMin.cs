using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Web.Models.Product
{
    public class ProdMin//Модель продукта для корзины и избранного
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public string Image { get; set; }
    }
}