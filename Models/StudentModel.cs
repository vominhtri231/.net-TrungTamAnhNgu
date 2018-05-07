using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrungTamAnhNgu.Models
{
    public class StudentModel:Model
    {
        public void Add(Student student)
        {
            this.dataContext.Students.Add(student);
            this.dataContext.SaveChanges();
        }

        public Student GetStudent(string username)
        {
            return this.dataContext.Students.Find(new object[] { username });
        }

        public List<Student> GetListStudents()
        {
            return this.dataContext.Students.Select(p => p).ToList();
        }

        public void Delete(string username)
        {
            Student student = GetStudent(username);
            this.dataContext.Students.Remove(student);
            this.dataContext.SaveChanges();
        }

    }
}