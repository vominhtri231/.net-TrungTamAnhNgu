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

        public List<Register> ListRegisterOfClass(string classId)
        {
            return this.dataContext.Registers.Where(p => p.ClassId.Equals(classId)).Select(p => p).ToList();
        }

        public List<Register> ListRegisterOfStudent(string username)
        {
            return this.dataContext.Registers.Where(p => p.StudentUsername.Equals(username)).Select(p => p).ToList();
        }

        public List<Student> GetStudentOfClass(string classId)
        {
            return this.dataContext.Registers.Where(p => p.ClassId.Equals(classId)).Select(p => p.Student).ToList();
        }

        public Register GetRegister(string classId, string username)
        {
            return this.dataContext.Registers.Find(new object[] { classId , username });
        }

        public void Delete(string classId, string username)
        {
            Register register = GetRegister(classId, username);
            this.Delete(register);
        }

        public void Delete(Register register)
        {
            MistakeModel mistakeModel = new MistakeModel();
            HomeworkModel homeworkModel = new HomeworkModel();
            mistakeModel.DeleteMadeMistakeOfRegister(register.StudentUsername, register.ClassId);
            homeworkModel.DeleteHomeWorkOfRegister(register.StudentUsername, register.ClassId);
            this.dataContext.Registers.Remove(register);
            this.dataContext.SaveChanges();
        }

        public void DeleteRegisterOfClass(string classId)
        {
            List<Register> registers = ListRegisterOfClass(classId);           
            foreach(Register register in registers)
            {
                this.Delete(register);
            }
        }

        public void DeleteRegisterOfStudent(string username)
        {
            List<Register> registers = ListRegisterOfStudent(username);
            foreach (Register register in registers)
            {
                this.Delete(register);
            }
        }

        public void Update(Register register)
        {
            Register oldRegister = GetRegister(register.ClassId, register.StudentUsername);
            oldRegister.IsPay = register.IsPay;
            this.dataContext.SaveChanges();
        }
       
    }
}