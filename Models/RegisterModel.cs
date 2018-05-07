using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrungTamAnhNgu.Models
{
    public class RegisterModel:Model
    {
        public void Add(Register register)
        {
            this.dataContext.Registers.Add(register);
            this.dataContext.SaveChanges();
        }

        public List<Student> ListStudentsRegisted(string classId)
        {
            return this.dataContext.Registers.Where(p => p.ClassId == classId).Select(p => p.Student).Distinct().ToList();
        }
    }
}