using eUseControl.Domain.Entities.Administration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BuisnessLogic.DbModel
{
    public class SessionContext : DbContext
    {
        public SessionContext() : base("name=eUseControl") { }

        public virtual DbSet<SessionTable> Sessions { get; set; }
    }
}
