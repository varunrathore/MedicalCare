using Microsoft.AspNetCore.Mvc;

namespace MedicalCare.Presentation.Controllers.Admin
{
    public class DashboardController : Controller
    {
        [Route("Admin/[controller]/[action]")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
