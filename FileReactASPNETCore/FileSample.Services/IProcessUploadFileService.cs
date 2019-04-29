using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FileSample.Services
{
    public interface IProcessUploadFileService
    {
        Task<string> UploadFileToStorageAsync(IList<IFormFile> formFiles);
    }
}
