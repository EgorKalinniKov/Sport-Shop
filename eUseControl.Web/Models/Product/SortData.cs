using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Web.Models.Product
{
    public class SortData
    {
        public List<ProductIconData> ListP { get; set; }
        public string SortOrder { get; set; }
        public string SortBy { get; set; }
        public string SortByName { get; set; }
        public string LowPrice {  get; set; }
        public string HighPrice { get; set; }
    }
}