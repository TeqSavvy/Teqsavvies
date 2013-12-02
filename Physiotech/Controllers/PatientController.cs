using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Physiotech.Models;

namespace Physiotech.Controllers
{
    public class PatientController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Patient/

        public ActionResult Index()
        {
            return View(db.Patients.ToList());
        }

        //
        // GET: /Patient/Details/5

        public ActionResult Details(int id = 0)
        {
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        //
        // GET: /Patient/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Patient/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        public ActionResult CreatePatient(Patient patient)
        {
            db.Patients.Add(patient);
            db.SaveChanges();
            return Json("created", JsonRequestBehavior.AllowGet);
        }

        //next appointemnt 
        public ActionResult NextAppointment(int patientId, string nextAppointment, string comment, string staffId)
        {
            var patient = db.Patients.Find(patientId);
            patient.Appointments.Add(new Appointment(){Date = DateTime.Parse(nextAppointment), Comment = comment});
            db.SaveChanges();
            return null;
        }

        //get all appointments
        public ActionResult AllPatientAppointments(int id)
        {
            var patient = db.Patients.Find(id);
            var allAppointment = patient.Appointments.ToList();

            return Json(allAppointment, JsonRequestBehavior.AllowGet);
        }


        //get a particular appointment
        public ActionResult GetAppointment(int patientId, string date)
        {
            var patient = db.Patients.Find(patientId);
             var allAppointment = patient.Appointments.ToList();
            var particular = (from p in allAppointment
                             where p.Date == DateTime.Parse(date)
                             select p).FirstOrDefault() ;
            return Json(particular, JsonRequestBehavior.AllowGet);
        }
       
        //add diagnosis for a patient
        public ActionResult AddDiagnosis(int patientId, string comment)
        {
            var patient = db.Patients.Find(patientId);
            var diagnosis = new Diagnosis(){Comment = comment, Date = DateTime.Now};
            patient.Diagnosis.Add(diagnosis);
            db.SaveChanges();
            return null;
        }

        public ActionResult AllDiagnosis(int patientId)
        {
            var patient = db.Patients.Find(patientId);
            var allDiagnosis = patient.Diagnosis.ToList();
            return Json(allDiagnosis, JsonRequestBehavior.AllowGet);

        }

        



        // GET: /Patient/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        //
        // POST: /Patient/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        //
        // GET: /Patient/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        //
        // POST: /Patient/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}