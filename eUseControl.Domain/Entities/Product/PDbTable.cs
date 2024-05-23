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
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Product Article")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Product article cannot be longer than 30 characters.")]
        public string Article { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Name of product cannot be longer than 30 characters.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Product Description")]
        [StringLength(300, MinimumLength = 5, ErrorMessage = "Product description cannot be longer than 300 characters.")]
        public string Description { get; set; }

        [Required]
        public int Cost { get; set; }
        public int Discount { get; set; }

        [Required]
        public bool AvailableStatus { get; set; }
        public int TotalRatings { get; set; }
        public double AvarageRating { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateChanged { get; set; }

        public string Brend { get; set; }
        public string Category { get; set; }
        public string Tag { get; set; }
        public string Directory { get; set; }
    }
}
