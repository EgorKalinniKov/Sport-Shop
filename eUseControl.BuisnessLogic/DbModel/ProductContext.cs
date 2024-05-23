using eUseControl.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BuisnessLogic.DbModel
{
    public class ProductContext:DbContext
    {
        public ProductContext() : base("name=eUseControl") { }

        public virtual DbSet<PDbTable> Products { get; set; }
    }
}
