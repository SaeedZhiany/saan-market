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

        public Tuple<int, bool> logIn()
        {
            try
            {
                using(DatabaseEntities context = new DatabaseEntities())
                {
                    var query = (from user in context.Users
                                 where user.username == userName
                                 select new { user.id, user.password, user.isAdmin });
                    if(query.Count() == 1)
                    {
                        var result = query.First();
                        SHA1 sha = new SHA1CryptoServiceProvider();
                        byte[] passBytes = System.Text.Encoding.UTF8.GetBytes(password);
                        byte[] passHash = sha.ComputeHash(passBytes);
                        if (result.password.Equals(passHash))
                            return Tuple.Create(result.id, result.isAdmin);
                        
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