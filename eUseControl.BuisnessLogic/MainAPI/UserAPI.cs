using eUseControl.BuisnessLogic.DbModel;
using eUseControl.Domain.Entities.Administration;
using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.Responces;
using eUseControl.Domain.Entities.Review;
using eUseControl.Domain.Entities.User;
using eUseControl.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace eUseControl.BuisnessLogic.MainAPI
{
    public class UserAPI
    {
        internal BaseResponces CheckUserCredintial(ULoginData data)
        {
            UDbTable local = null;
            using (var db = new UserContext())
            {
                local = db.Users.FirstOrDefault(x => x.Email == data.Credential);
            }
            return local != null ? new BaseResponces { Status = true } : new BaseResponces { Status = false, StatusMessage = "User doesn't exist" };
        }
        internal BaseResponces CheckIfUserBanned(UserMinimal data)
        {
            if(data.Level == Domain.Enums.URole.Banned)
            {
                if(data.BanTime > DateTime.Now)
                {
                    UDbTable local = null;
                    using(var db = new UserContext())
                    {
                        local = db.Users.FirstOrDefault(x => x.Email == data.Email);
                    }

                    if(local == null) { return new BaseResponces { Status = false, StatusMessage = "User doesn't exist" }; }

                    local.Level = Domain.Enums.URole.User;
                    using (var db = new SessionContext())
                    {
                        db.Entry(local).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return new BaseResponces { Status = true };
                }
            }

            return new BaseResponces { Status = false, StatusMessage = "User is still banned" };
        }
        internal BaseResponces GenerateUserSession(ULoginData data)
        {
            UDbTable local = null;
            var password = LoginHelper.HashGen(data.Password);

            using (var db = new UserContext())
            {
                local = db.Users.FirstOrDefault(x => x.Email == data.Credential && x.Password == password);
            }

            if (local == null) { return new BaseResponces { Status = false, StatusMessage = "Wrong Email or Passowrd" }; }

            using (var todo = new UserContext())
            {
                local.LastIp = data.LastIp;
                local.LastLogin = data.LastLogin;
                todo.Entry(local).State = EntityState.Modified;
                todo.SaveChanges();
            }

            return new BaseResponces { Status = true };
        }
        internal BaseResponces RegisterUserAction(URegisterData data)
        {
            UDbTable local = null;
            using (var db = new UserContext())
            {
                local = db.Users.FirstOrDefault(x => x.Email == data.Email);
            }

            if (local != null) { return new BaseResponces { Status = false, StatusMessage = "Email already registered" }; }

            var user = new UDbTable
            {
                Username = data.Username,
                Email = data.Email,
                Password = LoginHelper.HashGen(data.Password),
                LastIp = data.LastIp,
                LastLogin = data.LastLogin,
                DateCreated = DateTime.Now,
                DateEdited = DateTime.Now,
                BanTime = DateTime.Now,
                Level = Domain.Enums.URole.User,
            };

            using (var db = new UserContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }

            return new BaseResponces { Status = true };
        }
        internal HttpCookie Cookie(string loginCredential)
        {
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(loginCredential)
            };

            using (var db = new SessionContext())
            {
                SessionTable curent = null;
                curent = db.Sessions.FirstOrDefault(e => e.Email == loginCredential);

                if (curent != null)
                {
                    curent.CookieString = apiCookie.Value;
                    curent.ExpireTime = DateTime.Now.AddMinutes(60);
                    using (var todo = new SessionContext())
                    {
                        todo.Entry(curent).State = EntityState.Modified;
                        todo.SaveChanges();
                    }
                }
                else
                {
                    db.Sessions.Add(new SessionTable
                    {
                        Email = loginCredential,
                        CookieString = apiCookie.Value,
                        ExpireTime = DateTime.Now.AddMinutes(60)
                    });
                    db.SaveChanges();
                }
            }

            return apiCookie;
        }
        internal UserMinimal UserCookie(string cookie)
        {
            SessionTable session;
            UDbTable curentUser = null;

            using (var db = new SessionContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
            }

            if (session == null) return null;
            using (var db = new UserContext())
            {
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(session.Email))
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Email == session.Email);
                }
            }

            if (curentUser == null) return null;
            var userminimal = new UserMinimal
            {
                Username = curentUser.Username,
                Email = curentUser.Email,
                BanTime = DateTime.Now,
                Level = Domain.Enums.URole.User,
            };

            return userminimal;
        }

    }
}