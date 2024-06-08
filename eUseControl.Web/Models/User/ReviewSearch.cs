using eUseControl.Domain.Entities.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Web.Models.User
{
    public class ReviewSearch
    {
        public List<ReviewData> Reviews { get; set; }
        public string SelectedName { get; set; }
        public string Sort { get; set; }
        public ReviewSearch()
        {
            Reviews = new List<ReviewData>();
        }
    }
}