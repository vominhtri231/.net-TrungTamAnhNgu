using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrungTamAnhNgu.Models;

namespace TrungTamAnhNgu.Controllers
{
    public class TeacherController : Controller
    {
        public ActionResult Index()
        {
            string username = HttpContext.Request.Cookies["login"]["name"];
            ClassModel classModel = new ClassModel();
            ViewBag.classes = classModel.GetListClassesOfTeacher(username);
            return View();
        }
        public ActionResult Logout()
        {
            if (HttpContext.Request.Cookies["login"] != null)
            {
                HttpCookie cookie = new HttpCookie("login");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.SetCookie(cookie);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ClassInfo(string classId)
        {
            LessonModel lessonModel = new LessonModel();
            ClassModel classModel = new ClassModel();
            ViewBag.lessons=lessonModel.GetListLessonOfClass(classId);
            ViewBag.c = classModel.GetClass(classId);
            return View();
        }
        public ActionResult AddLesson(string classId)
        {
            ClassModel classModel = new ClassModel();
            ViewBag.c = classModel.GetClass(classId);
            return View();
        }
        public ActionResult AddLessonHandler(FormCollection form)
        {
            string classId = form["classId"].Trim();
            int classNum = Convert.ToInt32(form["classNumber"]);
            string content = form["content"];
            string location = form["location"];
            DateTime day = DateTime.Parse(form["day"]);
            string homework=SaveHomeworkFile(classId, classNum);
            string support = SavaSupportMaterialFile(classId, classNum);
            Lesson lesson = new Lesson()
            {
                ClassId = classId,
                ClassNumber = classNum,
                LessionContent = content,
                Location = location,
                Day = day,
                HomeworkQuestion = homework,
                SupportMaterial = support,
            };
            LessonModel lessonModel = new LessonModel();
            lessonModel.Add(lesson);
            return RedirectToAction("ClassInfo",new { classId });
        }

        public string SaveHomeworkFile(string classId,int classNumber)
        {
            if (Request.Files["homework"].ContentLength > 0)
            {
                string fileName = "Homework/" + classId + "-" + classNumber + System.IO.Path.GetExtension(Request.Files["homework"].FileName);
                SaveFile(fileName, "homework");
                return fileName;
            }
            return null;
        }

        public string SavaSupportMaterialFile(string classId,int classNumber)
        {
            if (Request.Files["support"].ContentLength > 0)
            {
                string fileName = "Support/" + classId + "-" + classNumber + System.IO.Path.GetExtension(Request.Files["support"].FileName);
                SaveFile(fileName, "support");
                return fileName;
            }
            return null;
        }
        
        public void SaveFile(string fileName, string formAtt)
        {           
            string fileLocation = Server.MapPath("~/Content/" + fileName);
            if (System.IO.File.Exists(fileLocation))
            {
                System.IO.File.Delete(fileLocation);
            }
            Request.Files[formAtt].SaveAs(fileLocation);
        }

        public ActionResult UpdateLesson(string classId,int classNumber)
        {
            LessonModel lessonModel = new LessonModel();
            ViewBag.lesson = lessonModel.GetLesson(classId, classNumber);
            return View();
        }

        public ActionResult UpdateLessonHandler(FormCollection form)
        {
            string classId = form["classId"].Trim();
            int classNum = Convert.ToInt32(form["classNumber"]);
            string content = form["content"].Trim();
            string location = form["location"].Trim();
            DateTime day = DateTime.Parse(form["day"].Trim());
            string homework = SaveHomeworkFile(classId, classNum);
            string support = SavaSupportMaterialFile(classId, classNum);
            Lesson lesson = new Lesson()
            {
                ClassId = classId,
                ClassNumber = classNum,
                LessionContent = content,
                Location = location,
                Day = day,
                HomeworkQuestion = homework,
                SupportMaterial = support,
            };
            LessonModel lessonModel = new LessonModel();
            lessonModel.Update(lesson);
            return RedirectToAction("ClassInfo", new { classId });
        }

        public ActionResult DeleteLesson(string classId, int classNumber)
        {
            LessonModel lessonModel = new LessonModel();
            lessonModel.Delete(classId, classNumber);
            return RedirectToAction("ClassInfo", new { classId });
        }
    }
}