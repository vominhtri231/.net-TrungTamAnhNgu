using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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

        private DataSet HandleExel()
        {
            string fileLocation = Server.MapPath("~/Content/") + Request.Files["studentList"].FileName;
            if (System.IO.File.Exists(fileLocation))
            {
                System.IO.File.Delete(fileLocation);
            }
            Request.Files["studentList"].SaveAs(fileLocation);

            string fileExtension = System.IO.Path.GetExtension(Request.Files["studentList"].FileName);
            string excelConnectionString;
            if (fileExtension == ".xls")
            {
                excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else
            {
                excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }

            string query = "Select * from [students$]";
            DataSet ds = new DataSet();
            using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnectionString))
            {
                dataAdapter.Fill(ds);
            }
            return ds;
        }

        #region teacher actions
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
        #endregion


        #region class actions
        public ActionResult ListClasses()
        {
            ClassModel model = new ClassModel();
            ViewBag.Classes = model.GetListClasses();
            return View();
        }

        public ActionResult AddClass()
        {
            TeacherModel modelTeacher = new TeacherModel();
            ViewBag.teachers = modelTeacher.GetListTeacher();
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
            if (Request.Files["studentList"].ContentLength > 0)
            {
                DataSet ds = HandleExel();
                model.AddRegisterByExel(c.Id,ds);
            }
            return RedirectToAction("ListClasses");
        }

        public ActionResult DeleteClass(string id)
        {
            ClassModel model = new ClassModel();
            model.Delete(id);
            return RedirectToAction("ListClasses");
        }

        public ActionResult UpdateClass(string id)
        {
            TeacherModel modelTeacher = new TeacherModel();
            ViewBag.teachers = modelTeacher.GetListTeacher();
            ClassModel model = new ClassModel();
            ViewBag.c = model.GetClass(id);
            return View();
        }

        public ActionResult UpdateClassHandler(FormCollection form)
        {
            ClassModel model = new ClassModel(); 
            Class c = new Class()
            {
                Id = form["id"],
                Name = form["name"],
                TeacherUsername = form["teacherUsername"],
                Charge = Convert.ToInt32(form["charge"])
            };
            model.Update(c);
            return RedirectToAction("ListClasses");
        }

       
        #endregion


        #region student actions

        public ActionResult ListStudents()
        {
            StudentModel studentModel = new StudentModel();
            ViewBag.students=studentModel.GetListStudents();
            return View();
        }

        public ActionResult AddStudentHandler(FormCollection form)
        {
            Student student = new Student()
            {
                UserName = form["username"],
                Password = form["password"],
                Name = form["name"],
                Gender = form["gender"] != null,
                Email = form["email"],
                PhoneNumber = form["phone"],
                Id = form["id"],
                DayOfBirth = DateTime.Parse(form["dayOfBirth"]),
                School=form["school"],
                Grade= Convert.ToInt32(form["grade"])
            };
            StudentModel studentModel = new StudentModel();
            studentModel.Add(student);
            return RedirectToAction("ListStudents");
        }

        public ActionResult AddStudent()
        {
            return View();
        }

        public ActionResult DeleteStudent(string username)
        {
            StudentModel studentModel = new StudentModel();
            studentModel.Delete(username);
            return RedirectToAction("ListStudents");
        }

        public ActionResult UpdateStudent(string username)
        {
            StudentModel studentModel = new StudentModel();
            ViewBag.student = studentModel.GetStudent(username);
            return View();
        }
        #endregion


        #region register actions

        public ActionResult RegisterOfClass(string classId)
        {
            RegisterModel registerModel = new RegisterModel();
            ViewBag.registers = registerModel.ListRegisterOfClass(classId);
            ViewBag.classId = classId;
            return View();
        }

        public ActionResult DeleteRegister(string classId, string username)
        {
            RegisterModel registerModel = new RegisterModel();
            registerModel.Delete(classId, username);
            return RedirectToAction("RegisterOfClass", new {classId});
        }

        public ActionResult UpdateRegister(string classId, string username)
        {
            RegisterModel registerModel = new RegisterModel();
            ViewBag.register= registerModel.GetRegister(classId, username);
            return View();
        }
        public ActionResult UpdateRegisterHandler(FormCollection form)
        {
            string classId = form["classId"].Trim();
            Register register = new Register()
            {
                ClassId = form["classId"],
                StudentUsername = form["studentUsername"],
                IsPay = form["isPay"] != null
            };
            RegisterModel registerModel = new RegisterModel();
            registerModel.Update(register);
            return RedirectToAction("RegisterOfClass", new { classId });
        }
        public ActionResult AddRegister(string classId)
        {
            ClassModel classModel = new ClassModel();
            ViewBag.c = classModel.GetClass(classId);
            return View();
        }
        public ActionResult AddRegisterHandler(FormCollection form)
        {
            string classId = form["classId"].Trim();
            ClassModel model = new ClassModel();
            if (Request.Files["studentList"].ContentLength > 0)
            {
                DataSet ds = HandleExel();
                model.AddRegisterByExel(classId, ds);
            }
            return RedirectToAction("RegisterOfClass", new {classId});
        }

        #endregion


        #region mistake actions

        public ActionResult ListMistakes()
        {
            MistakeModel model = new MistakeModel();
            ViewBag.mistakes = model.GetListMistakes();
            return View();
        }

        public ActionResult DeleteMistake(string username,string classId,int classNum,string id)
        {
            MistakeModel model = new MistakeModel();
            model.Delete(username, classId, classNum, id);
            return RedirectToAction("ListMistakes");
        }

        #endregion
    }
}