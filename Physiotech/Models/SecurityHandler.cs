using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Physiotech.Models
{
    public class SecurityHandler
    {

        public string HashPassword(string password)
        {
            SHA1Managed sha1 = new SHA1Managed();
            byte[] bytes = sha1.ComputeHash(new UnicodeEncoding().GetBytes(password));

            return Convert.ToBase64String(bytes);
        }

        public string HashedClinicId(string username, string password, string email)
        {
            string all = username + password + email;
            SHA1Managed sha1 = new SHA1Managed();
            byte[] bytes = sha1.ComputeHash(new UnicodeEncoding().GetBytes(all));

            return Convert.ToBase64String(bytes);
        }
    }
}