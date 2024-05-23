using eUseControl.Domain.Entities.Review;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BuisnessLogic.DbModel
{
    public class ReviewContext : DbContext
    {
        public ReviewContext() : base("name=eUseControl") { }

        public virtual DbSet<RDbTable> Reviews { get; set; }
    }
}
