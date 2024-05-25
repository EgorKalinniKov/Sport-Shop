using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.Review
{
    public class ReviewData
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public int Rate { get; set; }
        public DateTime DateEdited { get; set; }
    }
}
