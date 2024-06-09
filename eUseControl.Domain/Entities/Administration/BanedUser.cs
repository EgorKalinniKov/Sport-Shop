using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.Administration
{
    public class BanedUser
    {
        public int Id { get; set; }
        public DateTime BanTime { get; set; }
    }
}
