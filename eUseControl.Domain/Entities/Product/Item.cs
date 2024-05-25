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
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }
        public string ContainerId { get; set; }
        public bool InContainer { get; set; }
        public DateTime DateAdded { get; set; }
        public int ProductId { get; set; }
        public virtual PDbTable Product { get; set; }
    }
}
