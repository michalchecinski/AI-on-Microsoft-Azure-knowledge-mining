using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using TechnicalDocuIndexer.Web.Auth0;
using TechnicalDocuIndexer.Web.Models;
using TechnicalDocuIndexer.Web.Service;

namespace TechnicalDocuIndexer.Web.Controllers
{
    public class FileController : Controller
    {

        private readonly IFileHandler _handler;

        public FileController(IFileHandler handler)
        {
            _handler = handler;
        }

        [Authorize(Roles = Auth0Roles.Reader)]
        public IActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            return View(_handler.GetAll());
        }

        [HttpPost]
        public IActionResult Upload(List<IFormFile> files, string description)
        {
            List<FileModel> result = _handler.Upload(files, description);

            TempData["Message"] = $"Successfully uploaded {result.Count} file(s) to memory.";
            return RedirectToAction("Index");
        }

        public IActionResult Download(string id)
        {
            var file = _handler.GetAll().FirstOrDefault(obj => obj.Id.Equals(id));
            if (file == null) return null;
            return File(file.Data, file.FileType, file.Name + file.Extension);
        }
    }
}
