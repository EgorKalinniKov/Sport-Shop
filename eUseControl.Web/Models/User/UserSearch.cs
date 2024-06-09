using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Web.Models.User
{
    public class UserSearch
    {
        public List<UserMinimal> Users { get; set; }
        public int Id { get; set; }
        public DateTime BanTime { get; set; }

        public string SelectedName { get; set; }
        public string Sort { get; set; }
        public UserSearch()
        {
            Users = new List<UserMinimal>();
        }
    }
}