using Microsoft.AspNetCore.Mvc;

namespace MedicalCare.Presentation.Controllers.Admin
{
    [Route("Admin/[controller]/[action]")]
    public class TestsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
