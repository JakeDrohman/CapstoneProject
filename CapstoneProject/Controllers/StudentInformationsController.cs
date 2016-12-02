using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CapstoneProject.Models;

namespace CapstoneProject.Controllers
{
    public class StudentInformationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentInformations
        public ActionResult Index()
        {
            var studentInformations = db.StudentInformations.Include(s => s.Student);
            return View(studentInformations.ToList());
        }

        // GET: StudentInformations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentInformation studentInformation = db.StudentInformations.Find(id);
            if (studentInformation == null)
            {
                return HttpNotFound();
            }
            return View(studentInformation);
        }

        // GET: StudentInformations/Create
        public ActionResult Create()
        {
            ViewBag.StudentId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: StudentInformations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StudentId,FirstName,LastName,AlternateFirstName,AlternateLastName,Street,City,State,Zipcode,AlternateStreet,AlternateCity,AlternateState,AlternateZipcode,EmailAddress,DateAdmitted,Track,GraduationDate,Graduated,Withdrawn,Dismissed,CreditsNeeded,CreditsCompleted")] StudentInformation studentInformation)
        {
            if (ModelState.IsValid)
            {
                db.StudentInformations.Add(studentInformation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentId = new SelectList(db.Users, "Id", "Email", studentInformation.StudentId);
            return View(studentInformation);
        }

        // GET: StudentInformations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentInformation studentInformation = db.StudentInformations.Find(id);
            if (studentInformation == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentId = new SelectList(db.Users, "Id", "Email", studentInformation.StudentId);
            return View(studentInformation);
        }

        // POST: StudentInformations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StudentId,FirstName,LastName,AlternateFirstName,AlternateLastName,Street,City,State,Zipcode,AlternateStreet,AlternateCity,AlternateState,AlternateZipcode,EmailAddress,DateAdmitted,Track,GraduationDate,Graduated,Withdrawn,Dismissed,CreditsNeeded,CreditsCompleted")] StudentInformation studentInformation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentInformation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentId = new SelectList(db.Users, "Id", "Email", studentInformation.StudentId);
            return View(studentInformation);
        }

        // GET: StudentInformations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentInformation studentInformation = db.StudentInformations.Find(id);
            if (studentInformation == null)
            {
                return HttpNotFound();
            }
            return View(studentInformation);
        }

        // POST: StudentInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentInformation studentInformation = db.StudentInformations.Find(id);
            db.StudentInformations.Remove(studentInformation);
            db.SaveChanges();
            return RedirectToAction("Index");
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
