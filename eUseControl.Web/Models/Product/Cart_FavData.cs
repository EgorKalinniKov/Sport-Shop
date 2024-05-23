using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Web.Models.Product
{
    public class Cart_FavData//Модель корзины и избранного
    {
        public List<ProdMin> Products { get; set; }
    }
}