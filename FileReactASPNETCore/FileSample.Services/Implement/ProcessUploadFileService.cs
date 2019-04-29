using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FileSample.Services.Implement
{
    public class ProcessUploadFileService : IProcessUploadFileService
    {
        public async Task<string> UploadFileToStorageAsync(IList<IFormFile> formFiles)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (IFormFile file in formFiles)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    stringBuilder.AppendLine($"File's size of {file.FileName} is {memoryStream.Length}");
                }
            }

            return stringBuilder.ToString();
        }
    }
}
