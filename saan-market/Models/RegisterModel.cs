using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace saan_market.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="نام کاربری نیاز است")]
        public String userName { get; set; }

        [Required(ErrorMessage ="رمز ورود نیاز است")]
        [DataType(DataType.Password)]
        public String password { get; set; }

        [Required(ErrorMessage = "رمز ورود نیاز است")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "کلمه عبور با تکرار آن یکسان نیست. لطفا دوباره امتحان کنید.")]
        public String confirmPassword { get; set; }

        [EmailAddress(ErrorMessage = "ایمیل را به درستی وارد نکردید")]
        public string email { get; set; }

        public bool register()
        {
            try
            {
                using (DatabaseEntities context = new DatabaseEntities())
                {
                    var query = (from u in context.Users
                                 where u.username == userName
                                 select new { u.username});
                    if(query.Count() > 0)
                    {
                        throw new Exception("این نام کاربری قبلا استفاده شده، کلمه دیگری را امتحان کنید.");
                    }
                    User user = new User();
                    user.username = userName;
                    SHA1 sha = new SHA1CryptoServiceProvider();
                    byte[] passBytes = System.Text.Encoding.UTF8.GetBytes(password);
                    byte[] passHash = sha.ComputeHash(passBytes);
                    user.password = passHash;
                    user.isAdmin = false;
                    context.Users.Add(user);
                    context.SaveChanges();
                }
                return true;
            }catch (Exception e)
            {
                throw e;
            }
        }


    }
}