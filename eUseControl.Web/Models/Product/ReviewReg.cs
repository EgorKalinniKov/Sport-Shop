using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Web.Models.Product
{
    public class ReviewReg
    {
        public string Username { get; set; }
        public string Message { get; set; }
        public int Rate { get; set; }
    }
}