using eUseControl.Domain.Entities.User;
using eUseControl.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace eUseControl.BuisnessLogic.DbModel.Seed
{
    public class UserInit:DropCreateDatabaseIfModelChanges<UserContext>
    {
        protected override void Seed(UserContext context)
        {
            //Admin
            var admin = new UDbTable
            {
                Id = 1,
                Username = "Admin",
                Email = "SportShopAdmin@gmail.com",
                Password = LoginHelper.HashGen("SportShopAdmin"),
                LastIp = HttpContext.Current.Request.UserHostAddress,
                LastLogin = DateTime.Now,
                DateCreated = DateTime.Now,
                DateEdited = DateTime.Now,
                BanTime = DateTime.Now,
                Level = Domain.Enums.URole.Admin,
            };
            context.Users.Add(admin);
            context.SaveChanges();
        }
    }
}
