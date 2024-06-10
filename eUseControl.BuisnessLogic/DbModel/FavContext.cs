using eUseControl.Domain.Entities.Product;
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

        public virtual DbSet<ItemsFav> Favourites { get; set; }
    }
}
