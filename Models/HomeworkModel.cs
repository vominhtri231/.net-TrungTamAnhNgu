using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrungTamAnhNgu.Models
{
    public class HomeworkModel:Model
    {

        public void Submit(Homework homework)
        {
            if (GetHomework(homework.StudentUsername, homework.ClassId, homework.ClassNumber) != null)
            {
                Update(homework);
            }
            else
            {
                Add(homework);
            }
        }

        public void Add(Homework homework)
        {
            this.dataContext.Homework.Add(homework);
            this.dataContext.SaveChanges();
        }

        public void Update(Homework homework)
        {
            Homework oldHomework = GetHomework(homework.StudentUsername, homework.ClassId, homework.ClassNumber);
            if (homework.HomeworkContent != null) oldHomework.HomeworkContent = homework.HomeworkContent;
            oldHomework.Score = homework.Score;
            this.dataContext.SaveChanges();
        }

        public Homework GetHomework(string username, string ClassId, int ClassNumber)
        {
            return this.dataContext.Homework.Find(new object[] { username, ClassId, ClassNumber });
        }

        private List<Homework> GetListHomeworkOfRegister(string username, string classId)
        {
            return this.dataContext.Homework.Where(p => p.StudentUsername.Equals(username) && p.ClassId.Equals(classId)).ToList();
        }

        public List<Homework> GetListHomeworkOfLesson(string classId, int classNumber)
        {
            return this.dataContext.Homework.Where(p => p.ClassNumber == classNumber && p.ClassId.Equals(classId)).ToList();
        }

       

        public void DeleteHomeWorkOfRegister(string username, string classId)
        {
            List<Homework> homeworks = GetListHomeworkOfRegister(username, classId);
            foreach(Homework homework in homeworks)
            {
                Delete(homework);
            }
        }

        public void DeleteHomeWorkOfLesson(string classId, int classnumber)
        {
            List<Homework> homeworks = GetListHomeworkOfLesson(classId, classnumber);
            foreach (Homework homework in homeworks)
            {
                Delete(homework);
            }
        }

        public void Delete(string username,string ClassId,int ClassNumber)
        {
            Homework homework = GetHomework(username, ClassId, ClassNumber);
            Delete(homework);
        }

        public void Delete(Homework homework)
        {
            this.dataContext.Homework.Remove(homework);
            this.dataContext.SaveChanges();
        }
    }
}