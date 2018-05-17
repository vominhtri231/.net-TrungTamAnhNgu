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
            student.Password = Common.GetMD5(student.Password);
            this.dataContext.Students.Add(student);
            this.dataContext.SaveChanges();
        }

        public void Remove(Student student)
        {
            this.dataContext.Students.Remove(student);
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
            RegisterModel registerModel = new RegisterModel();
            registerModel.DeleteRegisterOfStudent(username);
            Student student = GetStudent(username);
            Delete(student);
        }

        public void Delete(Student student)
        {
            this.dataContext.Students.Remove(student);
            this.dataContext.SaveChanges();
        }

        public void Update(Student student)
        {
            Student oldStudent = GetStudent(student.UserName);
            oldStudent.Name = student.Name;
            oldStudent.Id = student.Id;
            oldStudent.Email = student.Email;
            oldStudent.PhoneNumber = student.PhoneNumber;
            oldStudent.Gender = student.Gender;
            oldStudent.DayOfBirth = student.DayOfBirth;
            if (student.Grade != null) oldStudent.Grade = student.Grade;
            if (student.School != null) oldStudent.School = student.School;
            this.dataContext.SaveChanges();
        }
    }
}