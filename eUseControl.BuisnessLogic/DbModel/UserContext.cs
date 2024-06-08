using eUseControl.BuisnessLogic.DbModel.Seed;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BuisnessLogic.DbModel
{
    public class UserContext: DbContext
    {
        public UserContext(): base("name=eUseControl") 
        {   Database.SetInitializer<UserContext>(new UserInit());   }

        public virtual DbSet<UDbTable> Users { get; set; }
    }
}
