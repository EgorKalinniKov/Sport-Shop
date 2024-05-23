using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.User
{
    public class UDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Username")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Username cannot be longer than 30 characters.")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Password")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password cannot be shorter than 8 characters.")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Email Address")]
        [StringLength(30)]
        public string Email { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        public DateTime DateEdited { get; set; }
        public URole Level { get; set; }
        public DateTime BanTime { get; set; }
    }
}
