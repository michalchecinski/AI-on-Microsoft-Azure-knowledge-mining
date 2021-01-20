using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TechnicalDocuIndexer.Web.Service
{
    public class TemporaryHandler : IFileHandler
    {
        private readonly IFileRepository _fileRepository;

        public TemporaryHandler(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task UploadAsync(List<IFormFile> files)
        {
            foreach (var file in files)
            {
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                await _fileRepository.UploadFileAsync("initial-load", file.FileName, memoryStream);
            }
        }
    }
}
            
