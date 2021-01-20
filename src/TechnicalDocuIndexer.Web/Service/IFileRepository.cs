using System.IO;
using System.Threading.Tasks;

namespace TechnicalDocuIndexer.Web.Service
{
    public interface IFileRepository
    {
        Task<byte[]> DownloadFileContent(string fileUrl);
        Task<byte[]> DownloadFileContent(string containerName, string fileName);
        Task UploadFileAsync(string containerName, string fileName, Stream fileContent);
    }
}