using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using TechnicalDocuIndexer.Web.Auth0;
using TechnicalDocuIndexer.Web.Service;
using System.Threading.Tasks;

namespace TechnicalDocuIndexer.Web.Controllers
{
    [Authorize(Roles = Auth0Roles.FileUploader)]
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(List<IFormFile> files)
        {
            await _handler.UploadAsync(files);

            TempData["Message"] = $"Successfully uploaded {files.Count} file(s).";
            return RedirectToAction("Index");
        }
    }
}
