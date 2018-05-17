using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrungTamAnhNgu.Models
{
    public class AdminModel:Model
    {
        public Admin GetAdmin(string username)
        {
            return this.dataContext.Admins.Find(new object[] { username });
        }
    }
}