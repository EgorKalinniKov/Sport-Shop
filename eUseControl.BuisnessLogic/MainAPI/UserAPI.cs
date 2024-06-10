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
        internal List<UserMinimal> GetAllUsers()
        {
            List<UserMinimal> ListU = new List<UserMinimal>();
            using (var db = new UserContext())
            {
                var users = db.Users;
                foreach (var u in users)
                {
                    var userMin = new UserMinimal()
                    {
                        Id = u.Id,
                        Username = u.Username,
                        Email = u.Email,
                        BanTime = u.BanTime,
                        Level = u.Level,
                        LastIp = u.LastIp,
                        LastLogin = u.LastLogin,
                    };
                    ListU.Add(userMin);
                }
            }
            return ListU;
        }
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
            if (data.Level == Domain.Enums.URole.Banned)
            {
                if (data.BanTime <= DateTime.Now)
                {
                    UDbTable local = null;
                    using (var db = new UserContext())
                    {
                        local = db.Users.FirstOrDefault(x => x.Email == data.Email);
                    }

                    if (local == null) { return new BaseResponces { Status = false, StatusMessage = "User doesn't exist" }; }

                    local.Level = Domain.Enums.URole.User;
                    using (var db = new UserContext())
                    {
                        db.Entry(local).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                return new BaseResponces { Status = false, StatusMessage = "User is still banned" };
            }

            return new BaseResponces { Status = true };
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
        internal void CloseUserSession(string cookie)
        {
            using (var db = new SessionContext())
            {
                var local = db.Sessions.FirstOrDefault(x => x.CookieString == cookie);
                if (local != null)
                {
                    local.ExpireTime = DateTime.Now.AddDays(-1);
                }
                db.Entry(local).State = EntityState.Modified;
                db.SaveChanges();
            }
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
        internal BaseResponces RegisterUReviewAction(RRegisterData data)
        {
            RDbTable local = null;
            using (var db = new ReviewContext())
            {
                local = db.Reviews.FirstOrDefault(x => x.Message == data.Message);
            }
            if (local != null) { return new BaseResponces { Status = false, StatusMessage = "Message already exist" }; }

            var review = new RDbTable
            {
                UserId = data.UserId,
                Username = data.Username,
                Article = data.Article,
                Message = data.Message,
                Rate = data.Rate,
                DateCreated = DateTime.Now,
                DateEdited = DateTime.Now,
            };

            using (var db = new ReviewContext())
            {
                db.Reviews.Add(review);
                db.SaveChanges();
            }

            PDbTable product = null;
            using (var db = new ProductContext())
            {
                product = db.Products.FirstOrDefault(x => x.Article == data.Article);
            }
            if (product != null)
            {
                product.AvarageRating = (product.AvarageRating * product.TotalRatings + data.Rate) / (product.TotalRatings + 1);
                product.TotalRatings++;
            }
            using (var db = new ProductContext())
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            }
            return new BaseResponces { Status = true };
        }
        internal BaseResponces DeleteReviewAction(int? id)
        {
            RDbTable deleteReview = null;
            using (var db = new ReviewContext())
            {
                deleteReview = db.Reviews.FirstOrDefault(x => x.ReviewId == id);
            }
            if (deleteReview == null) { return new BaseResponces { Status = false, StatusMessage = "Review doesn't exist" }; }

            PDbTable local = null;
            using (var db = new ProductContext())
            {
                local = db.Products.FirstOrDefault(x => x.Article == deleteReview.Article);
            }
            if (local == null) { return new BaseResponces { Status = false, StatusMessage = "Product doesn't exist" }; }

            using (var db = new ProductContext())
            {
                if (local.TotalRatings > 1)
                { local.AvarageRating = (local.AvarageRating * local.TotalRatings - deleteReview.Rate) / (local.TotalRatings - 1); }
                else
                { local.AvarageRating = local.AvarageRating - deleteReview.Rate; }
                local.TotalRatings--;
                db.Entry(local).State = EntityState.Modified;
                db.SaveChanges();
            }

            using (var db = new ReviewContext())
            {
                var rev = db.Reviews.FirstOrDefault(x => x.ReviewId == id);
                db.Reviews.Remove(rev);
                db.SaveChanges();
            }
            return new BaseResponces { Status = true };
        }
        internal List<ProdMin> GetUserCartAction(int? id)
        {
            List<ItemsCart> local = null;
            List<ProdMin> ListP = new List<ProdMin>();
            using (var db = new CartContext())
            {
                local = db.Cart.Where(x => x.UserId == id).ToList();
            }
            if (local != null)
            {
                using (var db = new ProductContext())
                {
                    foreach (var i in local)
                    {
                        var p = db.Products.FirstOrDefault(x => x.Article == i.ProductArticle);
                        var prodMin = new ProdMin()
                        {
                            Id = p.Id,
                            Article = p.Article,
                            Name = p.Name,
                            Description = p.Description,
                            Price = p.Price,
                            AvarageRating = p.AvarageRating,
                            TotalRatings = p.TotalRatings,
                            Discount = p.Discount,
                            Directory = p.Directory,
                            AvailableStatus = p.AvailableStatus,
                            Brend = p.Brend,
                            Category = p.Category,
                            Tag = p.Tag,
                            Image = p.Image,
                        };
                        ListP.Add(prodMin);
                    }
                }
            }
            return ListP;
        }
        internal List<ProdMin> GetUserFavAction(int? id)
        {
            List<ItemsFav> local = null;
            List<ProdMin> ListP = new List<ProdMin>();
            using (var db = new FavContext())
            {
                local = db.Favourites.Where(x => x.UserId == id).ToList();
            }
            if (local != null)
            {
                using (var db = new ProductContext())
                {
                    foreach (var i in local)
                    {
                        var p = db.Products.FirstOrDefault(x => x.Article == i.ProductArticle);
                        var prodMin = new ProdMin()
                        {
                            Id = p.Id,
                            Article = p.Article,
                            Name = p.Name,
                            Description = p.Description,
                            Price = p.Price,
                            AvarageRating = p.AvarageRating,
                            TotalRatings = p.TotalRatings,
                            Discount = p.Discount,
                            Directory = p.Directory,
                            AvailableStatus = p.AvailableStatus,
                            Brend = p.Brend,
                            Category = p.Category,
                            Tag = p.Tag,
                            Image = p.Image,
                        };
                        ListP.Add(prodMin);
                    }
                }
            }
            return ListP;
        }
        internal BaseResponces RemoveItemFromFavAction(string Art, int? id)
        {
            ItemsFav local = null;
            using (var db = new FavContext())
            {
                local = db.Favourites.FirstOrDefault(x => x.ProductArticle == Art && x.UserId == id);
            }
            if (local == null) { return new BaseResponces { Status = false, StatusMessage = "There is no product in the favourites" }; }

            using (var db = new FavContext())
            {
                var item = db.Favourites.FirstOrDefault(x => x.ProductArticle == Art && x.UserId == id);
                db.Favourites.Remove(item);
                db.SaveChanges();
            }
            return new BaseResponces { Status = true };
        }
        internal BaseResponces RemoveItemFromCartAction(string Art, int? id)
        {
            ItemsCart local = null;
            using (var db = new CartContext())
            {
                local = db.Cart.FirstOrDefault(x => x.ProductArticle == Art && x.UserId == id);
            }
            if (local == null) { return new BaseResponces { Status = false, StatusMessage = "There is no product in the cart" }; }

            using (var db = new CartContext())
            {
                var item = db.Cart.FirstOrDefault(x => x.ProductArticle == Art && x.UserId == id);
                db.Cart.Remove(item);
                db.SaveChanges();
            }
            return new BaseResponces { Status = true };
        }
        internal BaseResponces AddItemToCartAction(string Art, int id)
        {
            ItemsCart local = null;
            using (var db = new CartContext())
            {
                local = db.Cart.FirstOrDefault(x => x.ProductArticle == Art && x.UserId == id);
            }
            if (local != null) { return new BaseResponces { Status = false, StatusMessage = "Product already in the cart" }; }

            var item = new ItemsCart
            {
                UserId = id,
                ProductArticle = Art,
                DateAdded = DateTime.Now,
            };
            using (var db = new CartContext())
            {
                db.Cart.Add(item);
                db.SaveChanges();
            }
            return new BaseResponces { Status = true };
        }
        internal BaseResponces AddItemToFavAction(string Art, int id)
        {
            ItemsFav local = null;
            using (var db = new FavContext())
            {
                local = db.Favourites.FirstOrDefault(x => x.ProductArticle == Art && x.UserId == id);
            }
            if (local != null) { return new BaseResponces { Status = false, StatusMessage = "Product already in favourites" }; }

            var item = new ItemsFav
            {
                UserId = id,
                ProductArticle = Art,
                DateAdded = DateTime.Now,
            };
            using (var db = new FavContext())
            {
                db.Favourites.Add(item);
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
                    var tmp = new SessionTable
                    {
                        Email = loginCredential,
                        CookieString = apiCookie.Value,
                        ExpireTime = DateTime.Now.AddMinutes(60)
                    };
                    db.Sessions.Add(tmp);
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
                Id = curentUser.Id,
                Username = curentUser.Username,
                Email = curentUser.Email,
                BanTime = curentUser.BanTime,
                Level = curentUser.Level,
            };

            return userminimal;
        }
        internal BaseResponces EditUserAction(UserEdit data)
        {
            UDbTable local = null;
            using (var db = new UserContext())
            {
                local = db.Users.FirstOrDefault(x => x.Id == data.Id);
            }

            if (local == null) { return new BaseResponces { Status = false, StatusMessage = "User doesn't exist" }; }

            switch (data.Form)
            {
                case "Email": local.Email = data.Credential; break;
                case "Username": local.Username = data.Credential; break;
                case "Password": local.Password = LoginHelper.HashGen(data.Credential); break;
                default: return new BaseResponces { Status = false, StatusMessage = "Unregistered action" };
            }
            local.DateEdited = DateTime.Now;

            using (var db = new UserContext())
            {
                db.Entry(local).State = EntityState.Modified;
                db.SaveChanges();
            }

            return new BaseResponces { Status = true };
        }
        internal void DeleteUserAction(int id)
        {
            UDbTable local = null;
            using (var db = new UserContext())
            {
                local = db.Users.FirstOrDefault(x => x.Id == id);
            }

            if (local != null)
            {
                using (var db = new CartContext())
                {
                    var cart = db.Cart.Where(x => x.UserId == id).ToList();
                    db.Cart.RemoveRange(cart);
                    db.SaveChanges();
                }
                using (var db = new FavContext())
                {
                    var fav = db.Favourites.Where(x => x.UserId == id).ToList();
                    db.Favourites.RemoveRange(fav);
                    db.SaveChanges();
                }
                using (var db = new ReviewContext())
                {
                    var rev = db.Reviews.Where(x => x.UserId == id).ToList();
                    using (var db1 = new ProductContext())
                    {
                        foreach (var r in rev)
                        {
                            var prod = db1.Products.FirstOrDefault(x => x.Article == r.Article);
                            if (prod.TotalRatings > 1)
                            { prod.AvarageRating = (prod.AvarageRating * prod.TotalRatings - r.Rate) / (prod.TotalRatings - 1); }
                            else
                            { prod.AvarageRating = prod.AvarageRating - r.Rate; }
                            prod.TotalRatings--;
                            db.Entry(prod).State = EntityState.Modified;
                        }
                        db.SaveChanges();
                    }
                    db.Reviews.RemoveRange(rev);
                    db.SaveChanges();
                }
                using (var db = new UserContext())
                {
                    var user = db.Users.FirstOrDefault(x => x.Id == id);
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
            }
        }

    }
}