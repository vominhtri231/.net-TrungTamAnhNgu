using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrungTamAnhNgu.Models
{
    public abstract class Model
    {
        public TrungTamAnhNguEntities dataContext;
        public Model()
        {
            dataContext = new TrungTamAnhNguEntities();
        }
    }
}