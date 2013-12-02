using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Physiotech.Models;
using Physiotech.ViewModel;

namespace Physiotech.Controllers
{
    public class ClinicController : Controller
    {
        private readonly UsersContext db = new UsersContext();

        //
        // GET: /Clinic/

        public ActionResult Index()
        {
            return View(db.Clinics.ToList());
        }

        //
        // GET: /Clinic/Details/5

        public ActionResult Details(int id = 0)
        {
            Clinic clinic = db.Clinics.Find(id);

            if (clinic == null)
            {
                return HttpNotFound();
            }
            return View(clinic);
        }

        //
        // GET: /Clinic/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Clinic/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Clinic clinic)
        {
            if (ModelState.IsValid)
            {
                clinic.Staff.Role = "Admin";
                clinic.Staff.ClinicId = new SecurityHandler().HashedClinicId(clinic.Staff.Username,
                                                                             clinic.Staff.Password,
                                                                             clinic.Staff.EmailAddress);
                clinic.Staff.Fullname = clinic.Staff.Fullname;
                clinic.Staff.Password = new SecurityHandler().HashPassword(clinic.Staff.Password);
                db.Clinics.Add(clinic);

                db.SaveChanges();
                //}
                //else
                //{
                //    ModelState.AddModelError("Verification Error", "Error verifiying registration code, pls try again");
                //}


                return RedirectToAction("Index");
            }

            return View(clinic);
        }


        public ActionResult RegisterClinic(string name, string username, string password, string fullname, string mobile,
                                           string email)
        {
            try
            {
                //var guid = new Guid()
                string clinicId = username + Guid.NewGuid();

                var staff = new Staff
                    {
                        Role = "admin",
                        Username = username.ToLower(),
                        Password = new SecurityHandler().HashPassword(password).ToLower(),
                        EmailAddress = email.ToLower(),
                        ClinicId = clinicId.ToLower(),
                        Fullname = fullname.ToLower(),
                        Phonenumber = mobile.ToLower()
                    };

                var clinic = new Clinic {Name = name,  UniqueId = clinicId,};
                clinic.Staffs = new List<Staff>(){staff};

                db.Clinics.Add(clinic);
                db.SaveChanges();
                return Json("200", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return Json("error", JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Clinic/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Clinic clinic = db.Clinics.Find(id);
            if (clinic == null)
            {
                return HttpNotFound();
            }
            return View(clinic);
        }

        //
        // POST: /Clinic/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Clinic clinic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clinic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clinic);
        }

        //
        // GET: /Clinic/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Clinic clinic = db.Clinics.Find(id);
            if (clinic == null)
            {
                return HttpNotFound();
            }
            return View(clinic);
        }

        //
        // POST: /Clinic/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clinic clinic = db.Clinics.Find(id);
            db.Clinics.Remove(clinic);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }


        public ActionResult RegisterPartialView()
        {
            return PartialView("_register");
        }

        public ActionResult SearchResult()
        {
            return PartialView("_search");
        }

        public ActionResult SearchPatient(string parameter)
        {
            try
            {
                int id = int.Parse(Session["ClinicId"].ToString());
                var clinic = db.Clinics.Find(id);

                var patient = (from p in clinic.Patients where p.PatientId == parameter select p).FirstOrDefault();
                return Json(patient, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                TempData["error"] = "Patient does not exist";
            }

            return null;
        }

        public ActionResult Authenticate(string username, string password)
        {
            try
            {
                string hasedPassword = new SecurityHandler().HashPassword(password).ToLower();
                Staff staff = (from staffs in db.Staffs
                               where
                                   (staffs.Username.Equals(username.ToLower()) && staffs.Password.Equals(hasedPassword))
                               select staffs).FirstOrDefault();

                if (staff != null)
                {
                    Session["StaffId"] = staff.Id;
                    Session["StaffName"] = staff.Fullname;
                    Session["ClinicId"] =
                        (from c in db.Clinics where c.UniqueId == staff.ClinicId select c.Id).FirstOrDefault();

                    return Json("success", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }


            return View("Login");
        }

        public ActionResult Admin()
        {
            if (Session["staffID"] != null)
            {
                int staffId = int.Parse(Session["staffID"].ToString());
                Staff staff = (from s in db.Staffs where s.Id.Equals(staffId) select s).FirstOrDefault();
                return View(staff);
            }
            return View("Login");
        }

        //public ActionResult AddStaff(Staff staff)
        //{
        //    db.Staffs.Add(staff);
        //    db.SaveChanges();

        //}
        //public ActionResult Test()
        //{
        //    var from 
        //}

        public ActionResult Staff()
        {
            string sessionId = new HttpCookie("staffID").Value;
            if (sessionId != null)
            {
                int staffId = int.Parse(sessionId);
                Staff staff = (from s in db.Staffs where s.Id.Equals(staffId) select s).FirstOrDefault();
                Clinic clinic =
                    (from c in db.Clinics select c).FirstOrDefault();
                var viewModel = new StaffViewModel
                    {
                        Staff = staff,
                        Clinic = clinic
                    };
                return View(viewModel);
            }
            return View("Login");
        }


        public ActionResult AddPatient(Patient patient)
        {
            int id = int.Parse(Session["ClinicId"].ToString());

            Clinic clinic = db.Clinics.Find(id);
            //var client = new Patient(){Address =  patient.Address, PatientId =  patient.PatientId, EmailAddress = patient.EmailAddress, NextofKinMobile =  patient.NextofKinMobile, Fullname = patient.Fullname, Phonenumber = patient.Phonenumber, Dob = patient.Dob};
            try
            {
                //db.Patients.Add(patient);

                clinic.Patients = new List<Patient>() {patient};
                db.Entry(clinic).State = EntityState.Modified;


                db.SaveChanges();
                return Json("created", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Source + "-" + ex.Message + "-" + ex.StackTrace + "-" + ex.Data);
            }


            return null;
        }

        public ActionResult LoadPatients()
        {
            int id = int.Parse(Session["ClinicId"].ToString());
            var clinic = db.Clinics.Find(id);

            var patient = clinic.Patients;
            

            return Json(patient, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AllPatients()
        {
            //int id = int.Parse(Session["ClinicId"].ToString());
            //var clinic = db.Clinics.Find(id);

            //var patients = clinic.Patients;

            return PartialView("_patient");
        }

        //public ActionResult Portal()
        //{


        //}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}