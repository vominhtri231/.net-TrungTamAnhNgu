using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrungTamAnhNgu.Models
{
    public class TeacherModel:Model
    {
        public Teacher GetTeacher(string username)
        {
            return this.dataContext.Teachers.Find(new object[] { username });
        }

        public List<Teacher> GetListTeacher()
        {
           return this.dataContext.Teachers.Select(p => p).ToList();
        }

        public void Add(Teacher teacher)
        {
            teacher.Password = Common.GetMD5(teacher.Password);
            this.dataContext.Teachers.Add(teacher);
            this.dataContext.SaveChanges();
        }
        public void Delete(string username)
        {
            Teacher teacher = GetTeacher(username);
            this.dataContext.Teachers.Remove(teacher);
            this.dataContext.SaveChanges();
        }
        
        public void Update(Teacher newTeacher)
        {
            Teacher teacher = GetTeacher(newTeacher.UserName);
            teacher.Name = newTeacher.Name;
            teacher.Id = newTeacher.Id;
            teacher.Email = newTeacher.Email;
            teacher.PhoneNumber = newTeacher.PhoneNumber;
            teacher.Gender = newTeacher.Gender;
            teacher.DayOfBirth = newTeacher.DayOfBirth;
            this.dataContext.SaveChanges();
        }



        public List<string> GetUsernames()
        {
            return this.dataContext.Teachers.Select(p => p.UserName).ToList();
        } 
    }
}