using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrungTamAnhNgu.Models
{
    public class Common
    {
        public static string GetToken()
        {
            string token = "";
            Random ran = new Random();
            string tmp = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-";
            for (int i = 0; i < 31; i++)
            {
                token += tmp.Substring(ran.Next(0, 63), 1);
            }
            return token;
        }

        public static string GetMD5(string str)
        {
            string str_md5 = "";
            byte[] mang = System.Text.Encoding.UTF8.GetBytes(str);
            MD5CryptoServiceProvider my_md5 =
                new MD5CryptoServiceProvider();
            mang = my_md5.ComputeHash(mang);
            foreach (byte b in mang)
            {
                str_md5 += b.ToString("x2");
            }
            return str_md5;
        }
    }
}