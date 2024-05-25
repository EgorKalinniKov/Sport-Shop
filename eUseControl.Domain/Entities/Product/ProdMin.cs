using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.Product
{
    public class ProdMin
    {
        public int Id { get; set; }
        public string Article { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public bool AvailableStatus { get; set; }
        public string Image { get; set; }
        public int TotalRatings { get; set; }
        public double AvarageRating { get; set; }
        public string Brend { get; set; }
        public string Category { get; set; }
        public string Tag { get; set; }
        public string Directory { get; set; }
        public List<string> Imgs { get; set; }
    }
}
