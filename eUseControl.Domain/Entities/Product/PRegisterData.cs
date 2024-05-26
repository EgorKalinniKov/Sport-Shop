using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.Product
{
    public class PRegisterData
    {
        public string Name { get; set; }
        public string Article { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public string Brend { get; set; }
        public string Category { get; set; }
        public string Tag { get; set; }
        public List<String> Images { get; set; }
        public string Directory { get; set; }
        public bool AvailableStatus { get; set; }
    }
}
