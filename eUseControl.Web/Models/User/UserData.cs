using System.Collections.Generic;

namespace eUseControl.Web.Models.User
{
    public class UserData//Модель для пользователя
    {
        public string Username { get; set; }
        public List<string> Products { get; set; }
        public string SingleProduct { get; set; }

        public UserData() { Username = "My Account"; }
    }
}