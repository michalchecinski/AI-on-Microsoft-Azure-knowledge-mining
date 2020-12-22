using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TechnicalDocuIndexer.Web.Models;
using TechnicalDocuIndexer.Web.Service.Utils;

namespace TechnicalDocuIndexer.Web.Controllers
{
    public class DocumentDetailsController : Controller
    {

        private readonly DocumentService _documentService;

        public DocumentDetailsController(DocumentService documentService)
        {
            _documentService = documentService;
        }

        public IActionResult Index(string id)
        {
            DocumentDetails document = null;
            if(id != null)
            {
                document = _documentService.FetchDocument(id);
            }
            return View(document);
        }

        public DocumentDetails Details(string id)
        {
            return _documentService.FetchDocument(id);
        }

        public ActionResult File(string id)
        {
            //Will be replaced with BlobService call to download and return binary data of file
            var url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTPQEilGdtC0M-LvN_9otOWT0hauBrKfAy_Xg&usqp=CAU";
            var contentType = "image/png";
            byte[] data;
            using (WebClient client = new WebClient())
            {
                data = client.DownloadData(url);
                contentType = client.ResponseHeaders["Content-Type"];
            }
            MemoryStream ms = new MemoryStream(data);
            return File(ms.ToArray(), contentType, id);
        }
    }
}
