using System.Diagnostics;
using DotNetWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCare.Presentation.Controllers
{
    public class WebsiteHomeController : Controller
    {
        private readonly ILogger<WebsiteHomeController> _logger;

        public WebsiteHomeController(ILogger<WebsiteHomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
