using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cookies.Models;

namespace Cookies.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            ViewBag.ReturnUrl = Request.QueryString["ReturnUrl"];
            UserProfile user = new UserProfile();
            return View(user);
        }

        [HttpPost]
        public ActionResult Index(UserProfile user)
        {
            if (user.Username == user.Password)
            {
                // Authenticated
                HttpCookie cookie = new HttpCookie("AuthCookie");
                cookie.Value = user.Username;
                if (user.RememberMe)
                {
                    cookie.Expires = DateTime.MaxValue;
                }
                cookie.Path = Request.ApplicationPath;
                Response.Cookies.Add(cookie);

                string return_url = Request.QueryString["ReturnUrl"];
                if (string.IsNullOrEmpty(return_url))
                {
                    return Redirect("/");
                }
                else
                { 
                    return Redirect(return_url);
                }
                
            }
            else
            {
                //Not-Authenticated
                ViewBag.Error = "Invalid Username or Password";
            }
            return View(user);
        }
    }
}