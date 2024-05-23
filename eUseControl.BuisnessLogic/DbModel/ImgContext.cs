using eUseControl.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BuisnessLogic.DbModel
{
    public class ImgContext : DbContext
    {
        public ImgContext() : base("name=eUseControl") { }

        public virtual DbSet<PImgTable> Imgs { get; set; }
    }
}
