using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
            var file = _handler.GetAll().Where(obj => obj.Id.Equals(id)).FirstOrDefault();
            if (file == null) return null;
            return File(file.Data, file.FileType, file.Name + file.Extension);
        }
    }
}
