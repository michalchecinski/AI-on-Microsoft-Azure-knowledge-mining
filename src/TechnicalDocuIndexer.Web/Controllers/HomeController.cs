using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechnicalDocuIndexer.Web.Auth0;
using TechnicalDocuIndexer.Web.Models;

namespace TechnicalDocuIndexer.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.IsInRole(Auth0Roles.SearchReader))
            {
                return RedirectToAction("Index", "Search");
            }

            if (User.IsInRole(Auth0Roles.FileUploader))
            {
                return RedirectToAction("Index", "File");
            }

            return RedirectToAction("Login", "Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogError(Activity.Current?.Id);
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}