using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.Product
{
    public class ProdFilter
    {
        public string Tag { get; set; }
        public string Brend { get; set; }
        public int LowPrice { get; set; }
        public int HighPrice { get; set; }
    }
}
