using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace saan_market.Models
{
    public class RegisterModel
    {
        [Required]
        public String userName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "کلمه عبور با تکرار آن یکسان نیست. لطفا دوباره امتحان کنید.")]
        public String confirmPassword { get; set; }

        public bool registerUser()
        {
            try
            {
                using (DatabaseEntities context = new DatabaseEntities())
                {
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