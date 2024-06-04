using eUseControl.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Web.Models.Product
{
    public class ProdStore
    {
        public string Brend { get; set; }
        public string Tag { get; set; }
        public int LowPrice { get; set; }
        public int HighPrice { get; set; }
    }
}