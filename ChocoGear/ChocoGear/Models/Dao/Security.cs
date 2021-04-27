using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ChocoGear.Models.Dao
{
    public class Security
    {
        public string Base64(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }

        public bool CheckLogin(string username, string password)
        {
            Models.Entity.ChocoGearEntities db = new Entity.ChocoGearEntities();
            var q = db.Customers.Where(d => d.username.Equals(username) && d.password.Equals(password)).Count();
            if (q > 0)
            {
                return true;
            }
            return false;
        }

        public bool CheckPass(string password) {
            Models.Entity.ChocoGearEntities db = new Entity.ChocoGearEntities();
            var q = db.Customers.Where(d => d.password.Equals(password)).Count();
            if (q>0)
            {
                return true;
            }
            return false;
        }

        public bool Check_email_username(string email, string username)
        {
            Models.Entity.ChocoGearEntities db = new Entity.ChocoGearEntities();
            var q = db.Customers.Where(d => d.email.Equals(email) && d.username.Equals(username)).Count();
            if (q > 0)
            {
                return true;
            }
            return false;
        }

        public bool Check_email(string email)
        {
            Models.Entity.ChocoGearEntities db = new Entity.ChocoGearEntities();
            var q = db.Customers.Where(d => d.email.Equals(email)).Count();
            if (q > 0)
            {
                return true;
            }
            return false;
        }

        public bool Check_username(string username)
        {
            Models.Entity.ChocoGearEntities db = new Entity.ChocoGearEntities();
            var q = db.Customers.Where(d => d.username.Equals(username)).Count();
            if (q > 0)
            {
                return true;
            }
            return false;
        }

    }
}