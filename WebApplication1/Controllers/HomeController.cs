using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
    private ListThemes _db = new ListThemes();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Themes()
        {
            var data = (from s in _db.LThemes select s).ToList();
            ViewBag.themes = data;
            //ViewBag.title = "MVC5 - Hello World";
            return View();

        }
       
        
        public async Task<ActionResult> AddTheme()
        {
        
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddTheme(Themes model)
        {
            try
            {
                _db.LThemes.Add(model);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
           
            return RedirectToAction("Index");
        }

    }
}