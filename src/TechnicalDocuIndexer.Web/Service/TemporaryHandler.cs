using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TechnicalDocuIndexer.Web.Models;

namespace TechnicalDocuIndexer.Web.Service
{
    public class TemporaryHandler : IFileHandler
    {

        private List<FileModel> files;

        public TemporaryHandler()
        {
            this.files = new List<FileModel>();
        }

        public List<FileModel> GetAll()
        {
            return files;
        }

        public List<FileModel> Upload(List<IFormFile> files, string description)
        {
            List<FileModel> newFiles = new List<FileModel>();
            foreach (var file in files)
            {
                using var memoryStream = new MemoryStream();
                file.CopyToAsync(memoryStream);

                newFiles.Add(new FileModel
                (
                    Guid.NewGuid().ToString(),
                    Path.GetFileNameWithoutExtension(file.FileName),
                    file.ContentType,
                    Path.GetExtension(file.FileName),
                    description,
                    null,
                    DateTime.UtcNow,
                    memoryStream.ToArray()
                ));
            }
            this.files.AddRange(newFiles);
            return newFiles;
        }
    }
}
            
