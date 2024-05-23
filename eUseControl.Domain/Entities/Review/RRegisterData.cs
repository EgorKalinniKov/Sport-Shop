using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.Review
{
    public class RRegisterData
    {
        public string Article { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public int Rate { get; set; }
    }
}
