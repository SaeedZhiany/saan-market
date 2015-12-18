using saan_market.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace saan_market.Controllers
{
    public class AccountingController : Controller
    {
        public ActionResult Logout()
        {
            Session["userID"] = null;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(LogInModel model)
        {
            

            if (ModelState.IsValid)
            {
                Tuple<int, bool>  info= model.logIn();
                if(info.Item2==true)
                {
                    ModelState.AddModelError("", "شما یک مدیر هستید. به صفحه ورود مدیر ارجاع داده می شوید .");
                    return RedirectToAction("Administrator", "Accounting");
                }

                using (DatabaseEntities context = new DatabaseEntities())
                {
                    
                        User selectedUser = (from acc in context.Users
                                                   where acc.id == info.Item1
                                                   select acc).First();
                    Session["userID"]=selectedUser.username;

                }

                return RedirectToAction("Index", "Home");
                
            }
            ModelState.AddModelError("", "نام کاربری یا رمز عبور اشتباه است.");
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                { 
                    if (model.register())
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (Exception e)
                {
                    if (e.Message.Equals(""))
                        ModelState.AddModelError("", "خطا در ذخیره سازی محصول در پایگاه داده، لطفا دوباره امتحان کنید.");
                    else
                        ModelState.AddModelError("", e.Message.ToString());
                    return View();
                }
            }
            ModelState.AddModelError("", "لطفا فیلدهای لازم را به درستی وارد کنید.");
            return View();
        }

        public ActionResult Adminstrator()
        {
            return View();
        }

        public ActionResult ProfileView()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Adminstrator(string username, string password)
        {
            try
            {
                using (DatabaseEntities context = new DatabaseEntities())
                {
                    var query = (from user in context.Users
                                 where user.username == username
                                 select new { user.password, user.isAdmin });
                    if (query.Count() == 1)
                    {
                        var result = query.First();
                        SHA1 sha = new SHA1CryptoServiceProvider();
                        byte[] passBytes = System.Text.Encoding.UTF8.GetBytes(password);
                        byte[] passHash = sha.ComputeHash(passBytes);
                        if (result.password.SequenceEqual(passHash))
                        {
                            if (result.isAdmin)
                            {
                                return RedirectToAction("DefineProduct", "Product");
                            }
                            else
                            {
                                ModelState.AddModelError("", "شما یک کاربر عادی هستید. برای دسترسی به حساب خود از طریق لینک ورود در صفحه اصلی اقدام کنید.");
                                return View();
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "نام کاربری یا رمز ورود اشتباه است.");
                            return View();
                        }

                    }

                    throw new Exception("نام کاربری یا رمز عبور اشتباه است.");
                }
            }
            catch (Exception e)
            {
                throw e;
            }

     }

    }
}