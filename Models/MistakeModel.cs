using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrungTamAnhNgu.Models
{
    public class MistakeModel:Model
    {
        public List<MadeMistake> GetListMistakes()
        {
            return this.dataContext.MadeMistakes.Select(p => p).ToList();
        }

        public MadeMistake GetMadeMistake(string username, string classId, int classNum, string mistakeId)
        {
            return this.dataContext.MadeMistakes.Find(new object[] { mistakeId, username, classId, classNum });
        }

        public List<MadeMistake> GetListMadeMistakeOfRegister(string username,string classId)
        {
            return this.dataContext.MadeMistakes.Where(p => p.StudentUsername.Equals(username) && p.ClassId.Equals(classId)).ToList();
        }

        public List<MadeMistake> GetListMadeMistakeOfLesson(string classId,int classNumber)
        {
            return this.dataContext.MadeMistakes.Where(p => p.ClassNumber== classNumber && p.ClassId.Equals(classId)).ToList();
        }

        public void DeleteMadeMistakeOfRegister(string username, string classId)
        {
            List<MadeMistake> madeMistakes = GetListMadeMistakeOfRegister(username, classId);
            foreach (MadeMistake madeMistake in madeMistakes)
            {
                this.Delete(madeMistake);
            }
        }

        public void DeleteMadeMistakeOfLesson(string classId, int classnumber)
        {
            List<MadeMistake> madeMistakes = GetListMadeMistakeOfLesson(classId, classnumber);
            foreach (MadeMistake madeMistake in madeMistakes)
            {
                this.Delete(madeMistake);
            }
        }

        public void Delete(string username, string classId, int classNum, string mistakeId)
        {
            MadeMistake madeMistake = GetMadeMistake(username, classId, classNum, mistakeId);
            Delete(madeMistake);
        }
        public void Delete(MadeMistake madeMistake)
        {
            this.dataContext.MadeMistakes.Remove(madeMistake);
            this.dataContext.SaveChanges();
        }

        public void Add(MadeMistake mistake)
        {
            this.dataContext.MadeMistakes.Add(mistake);
            this.dataContext.SaveChanges();
        }
    }
}