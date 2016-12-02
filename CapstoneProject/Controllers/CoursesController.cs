using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CapstoneProject.Models;
using Dropbox.Api;
using System.Threading.Tasks;

namespace CapstoneProject.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Courses
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.Subject);
            return View(courses.ToList());
        }

        public ActionResult Sort(string searchString)
        {
            var courses = db.Courses.Include(c => c.Subject);
            List<Course> courseList = courses.ToList();
            foreach(Course course in courseList)
            {
                if (course.CourseName != searchString || course.Subject.Subject != searchString || !course.Credits.ToString().Equals(searchString))
                {
                    courseList.Remove(course);
                }
            }
            return View("Index", courseList);
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            ViewBag.CourseSubjectId = new SelectList(db.CourseSubjects, "Id", "Subject");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseSubjectId = new SelectList(db.CourseSubjects, "Id", "Subject", course.CourseSubjectId);
            return View(course);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseSubjectId = new SelectList(db.CourseSubjects, "Id", "Subject", course.CourseSubjectId);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CourseName,CourseSubjectId")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseSubjectId = new SelectList(db.CourseSubjects, "Id", "Subject", course.CourseSubjectId);
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        private string RedirectUri
        {
            get
            {
                if (this.Request.Url.Host.ToLowerInvariant() == "localhost")
                {
                    return string.Format("http://{0}:{1}/Home/Auth", this.Request.Url.Host, this.Request.Url.Port);
                }

                var builder = new UriBuilder(
                    Uri.UriSchemeHttps,
                    this.Request.Url.Host);

                builder.Path = "/Home/Auth";

                return builder.ToString();
            }
        }
        // GET: /Home/Connect
        public ActionResult Connect()
        {
            if (string.IsNullOrWhiteSpace(MvcApplication.AppKey) ||
                string.IsNullOrWhiteSpace(MvcApplication.AppSecret))
            {

                return RedirectToAction("Index");
            }

            var redirect = DropboxOAuth2Helper.GetAuthorizeUri(
                OAuthResponseType.Code,
                MvcApplication.AppKey,
                RedirectUri);

            return Redirect(redirect.ToString());
        }

        // GET: /Home/Auth
        public async Task<ActionResult> AuthAsync(string code, string state)
        {
            try
            {
                var response = await DropboxOAuth2Helper.ProcessCodeFlowAsync(
                    code,
                    MvcApplication.AppKey,
                    MvcApplication.AppSecret,
                    this.RedirectUri);

                MvcApplication.AccessToken = response.AccessToken;

                Console.WriteLine("This account has been connected to Dropbox.");
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                var message = string.Format(
                    "code: {0}\nAppKey: {1}\nAppSecret: {2}\nRedirectUri: {3}\nException : {4}",
                    code,
                    MvcApplication.AppKey,
                    MvcApplication.AppSecret,
                    this.RedirectUri,
                    e);
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
