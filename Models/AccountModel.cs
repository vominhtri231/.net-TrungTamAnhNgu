using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrungTamAnhNgu.Models
{
    public class AccountModel:Model
    {
        public string Login(string username,string password)
        {
            password = Common.GetMD5(password);
            string role = null;
            if(this.dataContext.Admins.Where(p => p.UserName.Equals(username) && p.Password.Equals(password)).Count() == 1)
            {
                role = "1";
            }
            else{
                if (this.dataContext.Teachers.Where(p => p.UserName.Equals(username) && p.Password.Equals(password)).Count() == 1){
                    role = "2";
                }
                else{
                    if(this.dataContext.Students.Where(p => p.UserName.Equals(username) && p.Password.Equals(password)).Count() == 1)
                    {
                        role = "3";
                    }
                }
            }
            if (role == null) return null;
            return Common.GetToken()+role;
        }
    }
}