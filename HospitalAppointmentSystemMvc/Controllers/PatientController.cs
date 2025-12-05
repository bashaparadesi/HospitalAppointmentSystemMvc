using System;
using System.Web.Mvc;
using HospitalAppointmentSystem.Models;
using HospitalAppointmentSystemMvc.DAL;

namespace HospitalAppointmentSystemMvc.Controllers
{
    public class PatientController : Controller
    {
        private readonly HospitalDAL hospitalDAL = new HospitalDAL();

        // GET: Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Patient patient)
        {
            if (!ModelState.IsValid)
                return View(patient);

            try
            {
                bool result = hospitalDAL.RegisterPatient(patient);
                if (result)
                {
                    TempData["Success"] = "Registration successful. Please login.";
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "Registration failed.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(patient);
        }

        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            try
            {
                var patient = hospitalDAL.Login(email, password);
                if (patient != null)
                {
                    Session["PatientID"] = patient.PatientID;
                    Session["PatientName"] = patient.Name;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View();
        }

        // Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
