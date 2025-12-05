using System;
using System.Web.Mvc;
using HospitalAppointmentSystemMvc.DAL;
using HospitalAppointmentSystemMvc.Models;

namespace HospitalAppointmentSystemMvc.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly HospitalDAL dal = new HospitalDAL();

        // GET: Book Appointment
        // GET: Book Appointment
        [HttpGet]
        public ActionResult Book(int id)
        {
            // if patient not logged in → redirect to login
            if (Session["PatientID"] == null)
            {
                TempData["Error"] = "Please login to book an appointment.";
                return RedirectToAction("Login", "Patient");
            }

            // Create a new Appointment model and set DoctorID
            Appointment model = new Appointment
            {
                DoctorID = id   // <-- this sets DoctorID automatically
            };

            return View(model);
        }


        // POST: Book Appointment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Book(Appointment app)
        {
            // check login
            if (Session["PatientID"] == null)
            {
                TempData["Error"] = "Please login to book an appointment.";
                return RedirectToAction("Login", "Patient");
            }

            try
            {
                // assign PatientID from session
                app.PatientID = Convert.ToInt32(Session["PatientID"]);

                bool success = dal.BookAppointment(app);
                if (success)
                {
                    TempData["BookedAppointment"] = app;
                    return RedirectToAction("AppointmentSlip");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to book appointment. Please try again.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(app);
        }

        // View My Appointments
        public ActionResult MyAppointments()
        {
            if (Session["PatientID"] == null)
                return RedirectToAction("Login", "Patient");

            int patientId = Convert.ToInt32(Session["PatientID"]);
            var list = dal.GetAppointments(patientId);
            return View(list);
        }
        public ActionResult AppointmentSlip()
        {
            var appointment = TempData["BookedAppointment"] as Appointment;

            if (appointment == null)
            {
                return RedirectToAction("MyAppointments");
            }

            return View(appointment);
        }

    }
}
