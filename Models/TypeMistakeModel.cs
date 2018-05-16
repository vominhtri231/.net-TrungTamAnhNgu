using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrungTamAnhNgu.Models
{
    public class TypeMistakeModel:Model
    {
        public List<Mistake> GetTypeMistakes()
        {
            return this.dataContext.Mistakes.ToList();
        }
    }
}