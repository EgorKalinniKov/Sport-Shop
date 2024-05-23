using eUseControl.BuisnessLogic.DbModel;
using eUseControl.Domain.Entities.Administration;
using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.Responces;
using eUseControl.Domain.Entities.Review;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Enums;
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
        internal BaseResponces GenerateUserSession(ULoginData data)
        {
            UDbTable local = null;
            var password = LoginHelper.HashGen(data.Password);

            using (var db = new UserContext())
            {
                local = db.Users.FirstOrDefault(x => x.Email == data.Credential && x.Password == password);
            }

            return local != null ? new BaseResponces { Status = true } : new BaseResponces { Status = false, StatusMessage = "Wrong Username or Passowrd" };
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
                UserName = data.UserName,
                Email = data.Email,
                Password = LoginHelper.HashGen(data.Password),
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
                SessionTable curent;
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(loginCredential))
                {
                    curent = db.Sessions.FirstOrDefault(e => e.Username == loginCredential);
                }
                else
                {
                    curent = db.Sessions.FirstOrDefault(e => e.Username == loginCredential);
                }

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
                        Username = loginCredential,
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
                if (validate.IsValid(session.Username))
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Email == session.Username);
                }
            }

            if (curentUser == null) return null;
            var userminimal = new UserMinimal
            {
                UserName = curentUser.UserName,
                Email = curentUser.Email,
                Password = LoginHelper.HashGen(curentUser.Password),
                DateCreated = DateTime.Now,
                DateEdited = DateTime.Now,
                BanTime = DateTime.Now,
                Level = Domain.Enums.URole.User,
            };

            return userminimal;
        }
        internal BaseResponces RegisterUReviewAction(RRegisterData data)
        {
            if (data.Message == null) { return new BaseResponces { Status = false, StatusMessage = "Review is empty" }; }
            if (data.Rate == 0) { return new BaseResponces { Status = false, StatusMessage = "Rate is empty" }; }

            var review = new RDbTable
            {
                Article = data.Article,
                UserId = data.UserId,
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

            PDbTable local;
            using (var db = new ProductContext())
            {
                local = db.Products.FirstOrDefault(x => x.Article == data.Article);
            }

            if (local == null) { return new BaseResponces { Status = false, StatusMessage = "Product doesn't exist" }; }
            using (var db = new ProductContext())
            {
                {
                    local.AvarageRating = (local.AvarageRating * local.TotalRatings + review.Rate) / (local.TotalRatings) + 1;
                    local.TotalRatings++;
                    db.Entry(local).State = EntityState.Modified;
                    db.SaveChanges();
                }
                db.SaveChanges();
            }
            return new BaseResponces { Status = true };
        }
        internal BaseResponces DeleteUReviewAction(string message)
        {
            RDbTable deleteReview = null;
            using (var db = new ReviewContext())
            {
                deleteReview = db.Reviews.FirstOrDefault(x => x.Message == message);
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
                local.AvarageRating = local.AvarageRating - deleteReview.Rate / local.TotalRatings;
                local.TotalRatings--;
                db.Entry(local).State = EntityState.Modified;
                db.SaveChanges();
            }

            using (var db = new ReviewContext())
            {
                db.Reviews.Remove(deleteReview);
                db.SaveChanges();
            }
            return new BaseResponces { Status = true };
        }
        internal BaseResponces DeleteUserAction(ULoginData data)
        {
            UDbTable deleteUser;
            using (var db = new UserContext())
            {
                deleteUser = db.Users.FirstOrDefault(x => x.Email == data.Credential);
            }
            if (deleteUser == null) { return new BaseResponces { Status = false, StatusMessage = "User doesn't exist" }; }

            List<RDbTable> deleteReviews = new List<RDbTable>();
            using (var db = new ReviewContext())
            {
                deleteReviews = db.Reviews.Where(x => x.UserId == deleteUser.UserId).ToList();
            }
            if ((deleteReviews != null) && (deleteReviews.Count > 0))
            {
                using (var db = new ReviewContext())
                {
                    foreach (var review in deleteReviews)
                    {
                        PDbTable Prod = null;
                        using (var db1 = new ProductContext())
                        {
                            Prod = db1.Products.FirstOrDefault(x => x.Article == review.Article);
                        }
                        if (Prod != null)
                        {
                            using (var db1 = new ProductContext())
                            {
                                Prod.AvarageRating = Prod.AvarageRating - review.Rate / Prod.TotalRatings;
                                Prod.TotalRatings--;
                                db1.Entry(Prod).State = EntityState.Modified;
                                db1.SaveChanges();
                            }
                        }
                        db.Reviews.Remove(review);
                    }
                    db.SaveChanges();
                }

                using (var db = new UserContext())
                {
                    db.Users.Remove(deleteUser);
                    db.SaveChanges();
                }
            }
            return new BaseResponces { Status = true };
        }
        internal BaseResponces AddProdToCartAction(PRegTo data)
        {
            CartTable Prod = null;
            using (var db = new CartContext())
            {
                Prod = db.Carts.FirstOrDefault(x => x.ProdArt == data.ProdArt);
            }
            if (Prod != null) { return new BaseResponces { Status = false, StatusMessage = "Product already in cart" }; }

            PDbTable local = null;
            using (var db = new ProductContext())
            {
                local = db.Products.FirstOrDefault(x => x.Article == data.ProdArt);
            }
            if (local == null) { return new BaseResponces { Status = false, StatusMessage = "Product doesn't exist" }; }
            if (local.AvailableStatus == false) { return new BaseResponces { Status = false, StatusMessage = "Product is out of stock" }; }

            var cart = new CartTable
            {
                UserId = data.UserId,
                ProdArt = data.ProdArt,
                ProdCost = data.ProdCost,
                DateAdded = DateTime.Now,
            };

            using (var db = new CartContext())
            {
                db.Carts.Add(cart);
                db.SaveChanges();
            }

            return new BaseResponces { Status = true };
        }
        internal BaseResponces AddProdToFavAction(PRegTo data)
        {
            FavTable Prod = null;
            using (var db = new FavContext())
            {
                Prod = db.Favs.FirstOrDefault(x => x.ProdArt == data.ProdArt);
            }
            if (Prod != null) { return new BaseResponces { Status = false, StatusMessage = "Product already in favourites" }; }

            PDbTable local = null;
            using (var db = new ProductContext())
            {
                local = db.Products.FirstOrDefault(x => x.Article == data.ProdArt);
            }
            if (local == null) { return new BaseResponces { Status = false, StatusMessage = "Product doesn't exist" }; }

            var Favourite = new FavTable
            {
                UserId = data.UserId,
                ProdArt = data.ProdArt,
                ProdCost = data.ProdCost,
                DateAdded = DateTime.Now,
            };

            using (var db = new FavContext())
            {
                db.Favs.Add(Favourite);
                db.SaveChanges();
            }

            return new BaseResponces { Status = true };
        }
        internal BaseResponces RemoveProdFromCartAction(string pCred)
        {
            CartTable deleteProd = null;
            using (var db = new CartContext())
            {
                deleteProd = db.Carts.FirstOrDefault(x => x.ProdArt == pCred);
            }
            if (deleteProd == null) { return new BaseResponces { Status = false, StatusMessage = "Product doesn't exist" }; }

            using (var db = new CartContext())
            {
                db.Carts.Remove(deleteProd);
                db.SaveChanges();
            }

            return new BaseResponces { Status = true };
        }
        internal BaseResponces RemoveProdFromFavAction(string pCred)
        {
            FavTable deleteProd = null;
            using (var db = new FavContext())
            {
                deleteProd = db.Favs.FirstOrDefault(x => x.ProdArt == pCred);
            }
            if (deleteProd == null) { return new BaseResponces { Status = false, StatusMessage = "Product doesn't exist" }; }

            using (var db = new FavContext())
            {
                db.Favs.Remove(deleteProd);
                db.SaveChanges();
            }

            return new BaseResponces { Status = true };
        }
        internal List<PDbTable> Products()
        {
            List<PDbTable> result = new List<PDbTable>();
            using (var db = new ProductContext())
            {
                var cars = db.Products;
                foreach (var c in cars)
                {
                    result.Add(c);
                }
            }
            return result;
        }
        internal List<RDbTable> Reviews(string pCred)
        {
            List<RDbTable> result = new List<RDbTable>();
            List<RDbTable> reviews = null;
            using (var db = new ReviewContext())
            {
                reviews = db.Reviews.Where(x => x.Article == pCred).ToList();
            }
            foreach (var r in reviews)
            {
                result.Add(r);
            }
            return result;
        }
        //internal BaseResponces EditNameAction(string data)

        //internal BaseResponces EditReviewAction(... data)

    }
}
