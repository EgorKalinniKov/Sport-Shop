using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Enums;

namespace eUseControl.Domain.Entities.Product
{
    public class PDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Product Article")]
        public string Article { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Product Description")]
        public string Description { get; set; }

        [Required]
        public int Price { get; set; }
        public int Discount { get; set; }

        [Required]
        public bool AvailableStatus { get; set; }
        [Required]
        public string Image { get; set; }
        public int TotalRatings { get; set; }
        public double AvarageRating { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        public DateTime DateChanged { get; set; }

        public string Brend { get; set; }
        public string Category { get; set; }
        public string Tag { get; set; }
        public string Directory { get; set; }
    }
}
