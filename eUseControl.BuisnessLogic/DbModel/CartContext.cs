using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BuisnessLogic.DbModel
{
    public class CartContext : DbContext
    {
        public CartContext() : base("name=eUseControl") { }

        public virtual DbSet<CartTable> Carts { get; set; }
    }
}
