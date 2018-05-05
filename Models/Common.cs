using System;
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
    }
}