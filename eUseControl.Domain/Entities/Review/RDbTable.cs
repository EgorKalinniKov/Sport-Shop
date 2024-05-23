using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.Review
{
    public class RDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; }

        [Required]
        [StringLength(30)]
        public string Article { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [StringLength(150)]
        public string Message { get; set; }
        [Required]
        public int Rate { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateEdited { get; set; }
    }
}
