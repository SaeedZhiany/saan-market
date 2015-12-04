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

        [Required]
        public String fullName { get; set; }

        [Required]
        [EmailAddress]
        public String email { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "کد ملی باید ده رقمی باشد")]
        [MinLength(10, ErrorMessage = "کد ملی باید ده رقمی باشد")]
        public int nationalNumber { get; set; }

        [Required]
        public DateTime birthDay { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public String mobileNumber { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public String phoneNumber { get; set; }

        public bool registerUser()
        {
            try
            {
                using (SaanMarketDBEntities context = new SaanMarketDBEntities())
                {
                    User user = new User();
                    user.username = userName;
                    SHA1 sha = new SHA1CryptoServiceProvider();
                    byte[] passBytes = System.Text.Encoding.UTF8.GetBytes(password);
                    byte[] passHash = sha.ComputeHash(passBytes);
                    user.password = passHash;
                    user.fullname = fullName;
                    user.email = email;
                    user.nationalNumber = nationalNumber;
                    user.birthday = birthDay;
                    user.mobileNumber = mobileNumber;
                    user.phoneNumber = phoneNumber;
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