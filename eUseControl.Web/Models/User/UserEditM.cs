using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace eUseControl.Web.Models.User
{
    public class UserEditM
    {
        public string Username   { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FormName { get; set; }
    }
}