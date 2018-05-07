using System;
using System.Collections.Generic;
using System.Data;
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

        public Class GetClass(string id)
        {
            return this.dataContext.Classes.Find(new object[] { id });
        }


        public void Delete(string id)
        {
            Class c = GetClass(id);
            this.dataContext.Classes.Remove(c);
            this.dataContext.SaveChanges();
        }

        public void Update(Class newClass)
        {
            Class c = GetClass(newClass.Id);
            c.Name = newClass.Name;
            c.TeacherUsername = newClass.TeacherUsername;
            c.Charge = newClass.Charge;
            this.dataContext.SaveChanges();
        }

        public void ReadExel(string id,DataSet ds)
        {
            StudentModel studentModel = new StudentModel();
            RegisterModel registerModel = new RegisterModel();
            int index=1;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];
                string studentUsername;
               
                if (row[1].ToString().Trim().Length == 0)
                {
                    studentUsername = ds.Tables[0].Rows[i][0].ToString();
                }
                else
                {
                    Student student = DataRow2Student(row, id + "-" + index++);
                    try
                    {
                        
                        studentModel.Add(student);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }
                   
                    studentUsername = student.UserName;
                }

                registerModel.Add(new Register(){
                    ClassId = id,
                    StudentUsername = studentUsername,
                    IsPay = false });
                
            }
        }

        private Student DataRow2Student(DataRow row,string username)
        {
            string name = row[0].ToString();
            DateTime dayOfBirth = DateTime.Parse(row[1].ToString());
            bool gender = Convert.ToInt32(row[2].ToString()) > 0;
            string phone = row[3].ToString();
            string email = row[4].ToString();
            string studentId = row[5].ToString();
            string school = row[6].ToString();
            int grade = Convert.ToInt32(row[7].ToString());

            Student student = new Student()
            {
                UserName = username,
                Password = "123",
                Name = name,
                DayOfBirth = dayOfBirth,
                Gender = gender,
                PhoneNumber = phone,
                Email = email,
                Id = studentId,
                School = school,
                Grade = grade
            };
            return student;
        }
    }
}