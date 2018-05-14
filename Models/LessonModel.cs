using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrungTamAnhNgu.Models
{
    public class LessonModel:Model
    {
        public List<Lesson> GetListLessonOfClass(string classId)
        {
            return this.dataContext.Lessons.Where(p => p.ClassId.Equals(classId)).ToList();
        }

        public void Add(Lesson lesson)
        {
            this.dataContext.Lessons.Add(lesson);
            this.dataContext.SaveChanges();
        }

        public void Update(Lesson lesson)
        {
            Lesson oldLesson = GetLesson(lesson.ClassId, lesson.ClassNumber);
            if (lesson.HomeworkQuestion != null) oldLesson.HomeworkQuestion = lesson.HomeworkQuestion;
            if (lesson.SupportMaterial != null) oldLesson.SupportMaterial = lesson.SupportMaterial;
            oldLesson.LessionContent = lesson.LessionContent;
            oldLesson.Location = lesson.Location;
            oldLesson.Day = lesson.Day;
            this.dataContext.SaveChanges();
        }

        public Lesson GetLesson(string classId, int classNumber)
        {
            return this.dataContext.Lessons.Find(new object[] { classId, classNumber });
        }

        public void Delete(string classId, int classNumber)
        {
            Lesson lesson = GetLesson(classId, classNumber);
            this.Delete(lesson);
        }

        public void Delete(Lesson lesson)
        {
            MistakeModel mistakeModel = new MistakeModel();
            HomeworkModel homeworkModel = new HomeworkModel();
            mistakeModel.DeleteMadeMistakeOfLesson(lesson.ClassId,lesson.ClassNumber);
            homeworkModel.DeleteHomeWorkOfLesson(lesson.ClassId, lesson.ClassNumber);
            this.dataContext.Lessons.Remove(lesson);
            this.dataContext.SaveChanges();
        }

        public void DeleteLessonOfClass(string classId)
        {
            List<Lesson> lessons = ListLessonOfClass(classId);
            foreach (Lesson lesson in lessons)
            {
                this.Delete(lesson);
                this.dataContext.SaveChanges();
            }
        }

        private List<Lesson> ListLessonOfClass(string classId)
        {
            return this.dataContext.Lessons.Where(p => p.ClassId.Equals(classId)).ToList();
        }
    }
}