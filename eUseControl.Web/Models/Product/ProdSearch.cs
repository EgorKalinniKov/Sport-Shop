using eUseControl.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Web.Models.Product
{
    public class ProdSearch
    {
        public List<ProdMin> Products { get; set; }
        public string SelectedName { get; set; }
        public string Sort { get; set; }
        public ReviewReg ReviewReg { get; set; }
        public ProdSearch()
        {
            Products = new List<ProdMin>();
        }
    }
}