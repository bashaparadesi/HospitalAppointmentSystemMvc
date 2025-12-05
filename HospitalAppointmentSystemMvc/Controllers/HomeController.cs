using System.Web.Mvc;
using HospitalAppointmentSystemMvc.DAL;

namespace HospitalAppointmentSystemMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly HospitalDAL dal = new HospitalDAL();

        public ActionResult Index()
        {
            var doctors = dal.GetDoctors();
            return View(doctors);
        }
    }
}
