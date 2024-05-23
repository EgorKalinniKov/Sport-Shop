using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Web.Models.Authorization
{
    //Данные пользователя
    public class LoginData//Модель для авторизации
    {
        public string Credential { get; set; }
        public string Password { get; set; }
    }
}