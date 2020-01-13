using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Jewelry.Respository.MemberResp
{
    public class Encryption
    {
        public static string EncryptionMethod(string password, string name) {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            encrypt = md5.ComputeHash(encode.GetBytes(password + name));
            return Convert.ToBase64String(encrypt);

        }
    }
}
