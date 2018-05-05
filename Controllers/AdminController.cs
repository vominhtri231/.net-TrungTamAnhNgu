using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrungTamAnhNgu.Models;

namespace TrungTamAnhNgu.Controllers
{
    public class AdminController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListTeachers()
        {
            TeacherModel model = new TeacherModel();
            ViewBag.Teachers = model.GetListTeacher();
            return View();
        }

        public ActionResult AddTeacher()
        {
            return View();
        }
        public ActionResult AddTeacherHandler(FormCollection form)
        {
            TeacherModel model = new TeacherModel();
            Teacher teacher = new Teacher() {
                UserName = form["username"],
                Password = form["password"],
                Name = form["name"],
                Gender = form["gender"] != null,
                Email = form["email"],
                PhoneNumber = form["phone"],
                Id = form["id"],
                DayOfBirth =DateTime.Parse(form["dayOfBirth"])
            };
            model.Add(teacher);
            return RedirectToAction("ListTeachers");
        }

        public ActionResult DeleteTeacher(string username)
        {
            TeacherModel model = new TeacherModel();
            model.Delete(username);
            return RedirectToAction("ListTeachers");
        }

        public ActionResult UpdateTeacher(string username)
        {
            TeacherModel model = new TeacherModel();
            ViewBag.teacher = model.GetTeacher(username);
            return View();
        }

        public ActionResult UpdateTeacherHandler(FormCollection form)
        {
            TeacherModel model = new TeacherModel();
            Teacher teacher = new Teacher()
            {
                UserName = form["username"],
                Password = form["password"],
                Name = form["name"],
                Gender = form["gender"] != null,
                Email = form["email"],
                PhoneNumber = form["phone"],
                Id = form["id"],
                DayOfBirth = DateTime.Parse(form["dayOfBirth"])
            };
            model.Update(teacher);
            return RedirectToAction("ListTeachers");
        }

        public ActionResult ListClasses()
        {
            ClassModel model = new ClassModel();
            ViewBag.Classes = model.GetListClasses();
            return View();
        }

        public ActionResult AddClass()
        {
            TeacherModel modelTeacher = new TeacherModel();
            ViewBag.TeacherUsername = modelTeacher.GetUsernames();
            return View();
        }

        public ActionResult AddClassHandler(FormCollection form)
        {
            ClassModel model = new ClassModel();
            Class c = new Class()
            {
                Id = form["id"],
                Name=form["name"],
                TeacherUsername=form["teacherUsername"],
                Charge=Convert.ToInt32(form["charge"])
            };
            model.Add(c);
            return RedirectToAction("ListClasses");
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

        
    }
}