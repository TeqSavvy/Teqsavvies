using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Physiotech.Models;
using Physiotech.ViewModel;

namespace Physiotech.Controllers
{
    public class StaffController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Staff/

        public ActionResult Index()
        {
            return View(db.Staffs.ToList());
        }

        //
        // GET: /Staff/Details/5

        public ActionResult Details(int id = 0)
        {
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        //
        // GET: /Staff/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Staff/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Staff staff)
        {
            if (ModelState.IsValid)
            {
                string hash = new SecurityHandler().HashPassword(staff.Password);
                staff.Password = hash;
                db.Staffs.Add(staff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(staff);
        }

        //
        // GET: /Staff/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        //
        // POST: /Staff/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Staff staff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staff);
        }

        //
        // GET: /Staff/Delete/5

        public ActionResult Delete(int id = 0)
        {
            
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        //
        // POST: /Staff/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Staff staff = db.Staffs.Find(id);
            db.Staffs.Remove(staff);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //staff portal action
        public ActionResult Portal()
        {
          if (Session["StaffId"] != null)
          {
              var staffId = int.Parse(Session["StaffId"].ToString());
              var staff = db.Staffs.Find(staffId);
              var clinic = (from c in db.Clinics where c.UniqueId == staff.ClinicId select c).FirstOrDefault();

              var view = new StaffViewModel() { Staff = staff, Clinic = clinic };
              return View(view);
          }

            return RedirectToAction("Login", "Clinic");

        }

        public ActionResult Logout()
        {
            Session["StaffId"] = null;
            Session["StaffName"] = null;
            Response.Redirect("/Clinic/Login");
            return null;
            
        }

        public ActionResult CreatePatient()
        {
            return PartialView("_create_patient");
        }

        public ActionResult Home()
        {
            return PartialView("_home");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}