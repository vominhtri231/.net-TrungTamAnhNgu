using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrungTamAnhNgu.Models
{
    public class ClassModel:Model
    {
        public List<Class> GetListClasses()
        {
            return this.dataContext.Classes.Select(p => p).ToList();
        }

        public void Add(Class c)
        {
            this.dataContext.Classes.Add(c);
            this.dataContext.SaveChanges();
        }
    }
}