using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrungTamAnhNgu.Controllers
{
    public abstract class UserController : Controller
    {
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