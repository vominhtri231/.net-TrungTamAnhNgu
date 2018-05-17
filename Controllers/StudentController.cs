using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrungTamAnhNgu.Models;

namespace TrungTamAnhNgu.Controllers
{
    public class StudentController : UserController
    {
        
        public ActionResult Index()
        {
            string username = HttpContext.Request.Cookies["login"]["name"];
            RegisterModel registerModel = new RegisterModel();
            ViewBag.registers = registerModel.ListRegisterOfStudent(username);
            MessageModel messageModel = new MessageModel();
            ViewBag.messages=messageModel.GetMessagesOfStudent(username);
            return View();
            
        }

        public ActionResult ViewProfile()
        {
            string username = HttpContext.Request.Cookies["login"]["name"];
            StudentModel studentModel = new StudentModel();
            ViewBag.student=studentModel.GetStudent(username);
            return View();
        }

        public ActionResult UpdateProfileHandler(FormCollection form)
        {
            Student student = new Student()
            {
                UserName = form["username"],
                Name = form["name"],
                Gender = form["gender"] != null,
                Email = form["email"],
                PhoneNumber = form["phone"],
                Id = form["id"],
                DayOfBirth = DateTime.Parse(form["dayOfBirth"]),
                School = form["school"],
                Grade = Convert.ToInt32(form["grade"])
            };
            StudentModel studentModel = new StudentModel();
            studentModel.Update(student);
            return RedirectToAction("ViewProfile");
            
        }


        public ActionResult RegisterInfo(string classId)
        {
            string username = HttpContext.Request.Cookies["login"]["name"];
            RegisterModel registerModel = new RegisterModel();
            ViewBag.register=registerModel.GetRegister(classId, username);
            return View();
        }

        public ActionResult SubmitHomework(string classId,int classNumber)
        {
            string username = HttpContext.Request.Cookies["login"]["name"];
            HomeworkModel homeworkModel = new HomeworkModel();
            ViewBag.homework=homeworkModel.GetHomework(username, classId, classNumber);
            return View();
        }
        public ActionResult SubmitHomeworkHandler(FormCollection form)
        {
            string username = HttpContext.Request.Cookies["login"]["name"];
            string classId = form["classId"].Trim();
            int classNumber = Convert.ToInt32(form["classNumber"]);
            string homwworkContent=SaveHomework(username, classId, classNumber);

            Homework homework = new Homework()
            {
                StudentUsername=username,
                ClassId=classId,
                ClassNumber=classNumber,
                HomeworkContent=homwworkContent
            };

            HomeworkModel homeworkModel = new HomeworkModel();
            homeworkModel.Submit(homework);

            return RedirectToAction("RegisterInfo", new { classId });
        }

        public string SaveHomework(string username,string classId, int classNumber)
        {
            if (Request.Files["homework"].ContentLength > 0)
            {
                string fileName = "HomeworkStudent/" + username+"-"+ classId + "-" + classNumber + System.IO.Path.GetExtension(Request.Files["homework"].FileName);
                SaveFile(fileName, "homework");
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

        public ActionResult ViewScore(string classId, int classNumber)
        {
            HomeworkModel homeworkModel = new HomeworkModel();
            LessonModel lessonModel = new LessonModel();
            ViewBag.homeworks=homeworkModel.GetListHomeworkOfLesson(classId, classNumber);
            ViewBag.lesson = lessonModel.GetLesson(classId, classNumber);
            return View();
        }

        public ActionResult ViewMadeMistake(string classId)
        {
            string username = HttpContext.Request.Cookies["login"]["name"];
            MistakeModel mistakeModel = new MistakeModel();
            ViewBag.madeMistakes = mistakeModel.GetListMadeMistakeOfRegister(username, classId);
            RegisterModel registerModel = new RegisterModel();
            ViewBag.register = registerModel.GetRegister(classId, username);
            return View();
        }

        #region request

        public ActionResult SendRequest(string classId)
        {
            string username = HttpContext.Request.Cookies["login"]["name"];
            RegisterModel registerModel = new RegisterModel();
            ViewBag.register = registerModel.GetRegister(classId, username);
            return View();
        }

        public ActionResult SendRequestHandler(FormCollection form)
        {
            string username = HttpContext.Request.Cookies["login"]["name"];
            string classId = form["classId"].Trim();
            string content = form["content"].Trim();
            Request request = new Request()
            {
                ClassId = classId,
                Content = content,
                StudentUsername = username,
            };
            RequestModel requestModel = new RequestModel();
            requestModel.Add(request);
            return RedirectToAction("Index");
        }
        #endregion
    }
}