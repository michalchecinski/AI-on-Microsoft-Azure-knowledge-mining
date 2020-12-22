using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalDocuIndexer.Web.Controllers
{
    public class DocumentDetailsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
