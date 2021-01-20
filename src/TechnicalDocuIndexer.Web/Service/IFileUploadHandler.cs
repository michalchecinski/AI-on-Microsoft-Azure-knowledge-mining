using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TechnicalDocuIndexer.Web.Service
{
    public interface IFileUploadHandler
    {
        Task UploadAsync(List<IFormFile> files);
    }
}
