using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrungTamAnhNgu.Models;

namespace TrungTamAnhNgu.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LoginHandler(FormCollection form)
        {
            string username = form["username"];
            string password = form["password"];
            AccountModel model = new AccountModel();
            string token=model.Login(username, password);
            if (token != null)
            {
                HttpCookie cookie = new HttpCookie("login");
                cookie.Values["name"]= username;
                cookie.Values["token"] = token;
                cookie.Expires = DateTime.Now.AddDays(1);
                ControllerContext.HttpContext.Response.Cookies.Remove("login");
                ControllerContext.HttpContext.Response.SetCookie(cookie);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login");
        }
        

    }
}