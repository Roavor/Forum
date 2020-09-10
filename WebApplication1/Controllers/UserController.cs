using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        private ListThemes _db = new ListThemes();
        // GET: Home  
        public ActionResult Index()
        {
            var data = (from s in _db.LThemes select s).ToList();
            ViewBag.themes = data;
            ViewBag.title = "MVC5 - Hello World";
            return View();
        }
    }
}