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
            RequestModel requestModel = new RequestModel();
            ViewBag.requests=requestModel.GetRequestsOfClass(classId);
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
            DateTime deadLine = DateTime.Parse(form["deadLine"]);
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
                DeadLine=deadLine
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
            DateTime deadLine = DateTime.Parse(form["deadLine"]);
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
                DeadLine = deadLine
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

        public ActionResult ViewHomework(string classId, int classNumber)
        {
            HomeworkModel homeworkModel = new HomeworkModel();
            ViewBag.homeworks=homeworkModel.GetListHomeworkOfLesson(classId, classNumber);
            LessonModel lessonModel = new LessonModel();
            ViewBag.lesson = lessonModel.GetLesson(classId, classNumber);
            return View();
        }

        public ActionResult Mark(string username,string classId, int classNumber)
        {
            HomeworkModel homeworkModel = new HomeworkModel();
            ViewBag.homework = homeworkModel.GetHomework(username, classId, classNumber);
            return View();
        }

        public ActionResult MarkHandler(FormCollection form)
        {
            string username = form["username"].Trim();
            string classId = form["classId"].Trim();
            int classNumber = Convert.ToInt32(form["classNumber"]);
            int score = Convert.ToInt32(form["score"]);

            Homework homework = new Homework()
            {
                StudentUsername = username,
                ClassId = classId,
                ClassNumber = classNumber,
                Score=score
            };

            HomeworkModel homeworkModel = new HomeworkModel();
            homeworkModel.Update(homework);
            return RedirectToAction("ViewHomework", new  { classId , classNumber });
        }

        public  ActionResult ListStudent(string classId)
        {
            RegisterModel registerModel = new RegisterModel();
            ViewBag.students=registerModel.GetStudentOfClass(classId);
            ClassModel classModel = new ClassModel();
            ViewBag.c = classModel.GetClass(classId);
            return View();
        }

        

        #region mistake 

        public ActionResult ViewMistake(string classId,int classNumber)
        {
            MistakeModel mistakeModel = new MistakeModel();
            ViewBag.mistakes=mistakeModel.GetListMadeMistakeOfLesson(classId, classNumber);
            LessonModel lessonModel = new LessonModel();
            ViewBag.lesson = lessonModel.GetLesson(classId, classNumber);
            return View();
        }

        public ActionResult AddMistake(string classId, int classNumber)
        {
            LessonModel lessonModel = new LessonModel();
            ViewBag.lesson = lessonModel.GetLesson(classId, classNumber);
            RegisterModel registerModel = new RegisterModel();
            ViewBag.students=registerModel.GetStudentOfClass(classId);
            TypeMistakeModel typeMistakeModel = new TypeMistakeModel();
            ViewBag.typeMistakes = typeMistakeModel.GetTypeMistakes();
            return View();
        }

        public ActionResult AddMistakeHandler(FormCollection form)
        {
            string classId = form["classId"].Trim();
            int classNumber = Convert.ToInt32(form["classNumber"]);
            string username = form["username"].Trim();
            string id = form["id"].Trim();
            MadeMistake mistake = new MadeMistake()
            {
                ClassId=classId,
                ClassNumber=classNumber,
                StudentUsername=username,
                MistakeId=id,
            };
            MistakeModel mistakeModel = new MistakeModel();
            mistakeModel.Add(mistake);
            return RedirectToAction("ViewMistake", new { classId, classNumber });
        }

        public ActionResult DeleteMistake(string username,string classId, int classNumber,string id)
        {
            MistakeModel mistakeModel = new MistakeModel();
            mistakeModel.Delete(username, classId, classNumber, id);
            return RedirectToAction("ViewMistake", new { classId, classNumber });
        }



        #endregion

        #region message

        public ActionResult ViewMessage(string classId)
        {
            ClassModel classModel = new ClassModel();
            ViewBag.c=classModel.GetClass(classId);
            return View();
        }

        public ActionResult AddMessage(string classId)
        {
            ClassModel classModel = new ClassModel();
            ViewBag.c = classModel.GetClass(classId);
            return View();
        }

        public ActionResult AddMessageHandler(FormCollection form)
        {
            string classId = form["classId"].Trim();
            string content = form["content"].Trim();
            Message message = new Message()
            {
                ClassId=classId,
                Content=content
            };
            MessageModel messageModel = new MessageModel();
            messageModel.Add(message);
            return RedirectToAction("ViewMessage", new { classId });
        }

        public ActionResult DeleteMessage(int id)
        {
            MessageModel messageModel = new MessageModel();
            string classId = messageModel.GetMessage(id).ClassId.Trim();
            messageModel.Delete(id);
            return RedirectToAction("ViewMessage", new { classId});
        }

        public ActionResult UpdateMessage(int id)
        {
            MessageModel messageModel = new MessageModel();
            ViewBag.message = messageModel.GetMessage(id);
            return View();
        }

        public ActionResult UpdateMessageHandler(FormCollection form)
        {
            string classId = form["classId"].Trim();
            string content = form["content"].Trim();
            int id = Convert.ToInt32(form["id"].Trim());
            Message message = new Message()
            {
                Id=id,
                Content = content
            };
            MessageModel messageModel = new MessageModel();
            messageModel.Update(message);
            return RedirectToAction("ViewMessage", new { classId });
        }

        #endregion
    }
}