using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cookies.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = Request.ApplicationPath;
            return View();
        }

        public ActionResult Create(FormCollection col)
        {
            HttpCookie cookie = new HttpCookie(col["key"]);
            cookie.Value = col["value"];

            if (col["isSecured"] == "Secured")
                cookie.Secure = true;
            else
                cookie.Secure = false;

            if (col["isPersistant"] == "Persistant")
                cookie.Expires = DateTime.Now.AddDays(1);

            cookie.Path = Request.ApplicationPath;
            
            Response.Cookies.Add(cookie);

            ViewBag.Message = "Cookie has been created successfully.";

            return View("Index");
        }

        public ActionResult Get(FormCollection col)
        {
            HttpCookie cookie = Request.Cookies[col["key"]];
            if (cookie == null)
                ViewBag.Cookie = "Not Available";
            else
                ViewBag.Cookie = cookie.Value;

            return View("Index");
        }

        public ActionResult Remove(FormCollection col)
        {
            HttpCookie cookie = new HttpCookie(col["key"]);
            cookie.Expires = DateTime.Now.AddDays(-1);
            cookie.Path = Request.ApplicationPath;

            Response.Cookies.Add(cookie);
            ViewBag.Message = "Cookie has been removed.";
            return View("Index");
        }
        public ActionResult GetAll()
        {
            string s = "";
            foreach (string key in Request.Cookies)
            {
                s += key + ": " + Request.Cookies[key].Value + "<br/>";
            }
            ViewBag.Cookie = s;
            return View("Index");
        }

        public ActionResult About()
        {
            Authenticate("/Home/About/");
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            Authenticate("/Home/Contact");
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private void Authenticate(string returnUrl)
        {
            HttpCookie cookie = Request.Cookies["AuthCookie"];
            if (cookie == null)
            {
                Response.Redirect("/Login/Index?ReturnUrl=" + returnUrl);
            }
        }
    }
}