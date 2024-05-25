using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.Administration
{
    public class SessionTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoginId { get; set; }

        [Required]
        [StringLength(30)]
        public string Email { get; set; }
        public int UserId { get; set; }

        [StringLength(30)]
        public string LastIp { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastLogin { get; set; }
        public bool LoginStatus { get; set; }

        [Required]
        public string CookieString { get; set; }

        [Required]
        public DateTime ExpireTime { get; set; }
    }
}
