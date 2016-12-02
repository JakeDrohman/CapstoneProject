using CapstoneProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapstoneProject.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            try
            {
                if (userManager.IsInRole(System.Web.HttpContext.Current.User.Identity.GetUserId(),"Admin"))
                {
                    return View("AdminIndex");
                }
                else if (userManager.IsInRole(System.Web.HttpContext.Current.User.Identity.GetUserId(), "Advisor"))
                {
                    return View("AdvisorIndex");
                }
                else if (userManager.IsInRole(System.Web.HttpContext.Current.User.Identity.GetUserId(), "Teacher"))
                {
                    return View("TeacherIndex");
                }
                else if (userManager.IsInRole(System.Web.HttpContext.Current.User.Identity.GetUserId(), "Student"))
                {
                    return View("StudentIndex");
                }
            }
            catch
            {
                return View("Index");
            }
            return View("Index");
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}