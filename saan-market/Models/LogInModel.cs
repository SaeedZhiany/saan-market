using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;

namespace saan_market.Models
{
    public class LogInModel
    {
        [Required]
        public String userName { get; set; }

        [Required]
        public String password { get; set; }

        public int logIn()
        {
            try
            {
                using(SaanMarketDBEntities context = new SaanMarketDBEntities())
                {
                    var query = (from u in context.Users
                                 where u.username == userName
                                 select new { u.id, u.password, u.isAdmin });
                    if(query.Count() == 1)
                    {
                        var result = query.First();
                        SHA1 sha = new SHA1CryptoServiceProvider();
                        byte[] passBytes = System.Text.Encoding.UTF8.GetBytes(password);
                        byte[] passHash = sha.ComputeHash(passBytes);
                        if (result.password.Equals(passHash))
                            return result.id;
                        
                    }

                    throw new Exception("نام کاربری یا رمز عبور اشتباه است.");
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}