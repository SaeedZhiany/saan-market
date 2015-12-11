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

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(AccountingModel model)
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