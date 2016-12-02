using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CapstoneProject.Models;
using Microsoft.AspNet.Identity;

namespace CapstoneProject.Controllers
{
    public class AppointmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Appointments
        public ActionResult Index()
        {
            var appointments = db.Appointments.Include(a => a.Advisor).Include(a => a.Student);
            return View(appointments.ToList());
        }

        public ActionResult GetAdvisorAppointments()
        {
            string _advisorId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            List<Appointment> _appointments = (db.Appointments.Include(a => a.Advisor).Include(a => a.Student)).ToList();
            foreach (Appointment item in _appointments)
            {
                if (item.AdvisorId != _advisorId)
                {
                    _appointments.Remove(item);
                }
            }
            return View("Index", _appointments);
        }

        public ActionResult GetStudentAppointments()
        {
            string _studentId= System.Web.HttpContext.Current.User.Identity.GetUserId();
            List<Appointment> _appointments = (db.Appointments.Include(a => a.Advisor).Include(a => a.Student)).ToList();
            foreach(Appointment item in _appointments)
            {
                if (item.StudentId != _studentId)
                {
                    _appointments.Remove(item);
                }
            }
            return View("Index", _appointments);
        }

        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create
        public ActionResult Create()
        {
            ViewBag.AdvisorId = new SelectList(db.Users, "Id", "Email");
            ViewBag.StudentId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,ReasonForAppointment,StudentId,AdvisorId")] Appointment appointment)
        {
            
            if (ModelState.IsValid)
            {
                if (User.IsInRole("Student"))
            {
                appointment.StudentId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("StudentIndex");
            }
            else if (User.IsInRole("Advisor"))
            {
                appointment.AdvisorId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("AdvisorIndex");
            }
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdvisorId = new SelectList(db.Users, "Id", "Email", appointment.AdvisorId);
            ViewBag.StudentId = new SelectList(db.Users, "Id", "Email", appointment.StudentId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdvisorId = new SelectList(db.Users, "Id", "Email", appointment.AdvisorId);
            ViewBag.StudentId = new SelectList(db.Users, "Id", "Email", appointment.StudentId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,ReasonForAppointment,StudentId,AdvisorId")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdvisorId = new SelectList(db.Users, "Id", "Email", appointment.AdvisorId);
            ViewBag.StudentId = new SelectList(db.Users, "Id", "Email", appointment.StudentId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
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
