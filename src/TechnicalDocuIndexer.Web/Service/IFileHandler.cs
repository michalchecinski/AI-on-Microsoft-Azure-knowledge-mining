using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalDocuIndexer.Web.Models;

namespace TechnicalDocuIndexer.Web.Service
{
    public interface IFileHandler
    {
        public List<FileModel> Upload(List<IFormFile> files, string description);
        public List<FileModel> GetAll();
    }
}
