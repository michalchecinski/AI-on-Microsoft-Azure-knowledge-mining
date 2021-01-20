using System.IO;
using System.Threading.Tasks;

namespace TechnicalDocuIndexer.Web.Service
{
    public interface IFileRepository
    {
        Task<byte[]> DownloadFileContentAsync(string fileUrl);
        Task<byte[]> DownloadFileContentAsync(string containerName, string fileName);
        Task UploadFileAsync(string containerName, string fileName, Stream fileContent);
    }
}