using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Web.Models.Authorization
{
    public class UActionRegister//Модель для регистрации
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }
}