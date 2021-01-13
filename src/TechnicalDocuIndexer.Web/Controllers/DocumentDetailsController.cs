using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TechnicalDocuIndexer.Web.Auth0;
using TechnicalDocuIndexer.Web.Models;
using TechnicalDocuIndexer.Web.Service;
using TechnicalDocuIndexer.Web.Service.Utils;

namespace TechnicalDocuIndexer.Web.Controllers
{
    [Authorize(Roles = Auth0Roles.SearchReader)]
    public class DocumentDetailsController : Controller
    {
        private readonly DocumentService _documentService;
        private readonly IFileRepository _fileRepository;

        public DocumentDetailsController(DocumentService documentService,
            IFileRepository fileRepository)
        {
            _documentService = documentService;
            _fileRepository = fileRepository;
        }

        public async Task<IActionResult> Index(string id)
        {
            DocumentDetails document = null;
            if(id != null)
            {
                document = await _documentService.FetchDocument(id);
            }
            return View(document);
        }

        public async Task<ActionResult> File(string id)
        {
            var contentType = "image/png";
            var docDetails = await _documentService.FetchDocument(id);
            var storageUrl = docDetails.StorageUrl;

            var content = await _fileRepository.DownloadFileContent(storageUrl);

            return File(content, contentType, docDetails.metadata_storage_name);
        }
    }
}