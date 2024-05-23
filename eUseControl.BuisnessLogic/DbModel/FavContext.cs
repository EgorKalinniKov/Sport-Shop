using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BuisnessLogic.DbModel
{
    public class FavContext : DbContext
    {
        public FavContext() : base("name=eUseControl") { }

        public virtual DbSet<FavTable> Favs { get; set; }
    }
}
