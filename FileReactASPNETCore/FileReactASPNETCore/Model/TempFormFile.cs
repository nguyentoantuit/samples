using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FileReactASPNETCore.Model
{
    public class TempFormFile : IFormFile
    {
        private readonly string _tempFilePath;

        public TempFormFile(string fileName, string name, string tempFilePath)
        {
            FileName = fileName;
            Name = name;
            _tempFilePath = tempFilePath;
        }

        public string ContentType => throw new NotImplementedException();

        public string ContentDisposition => throw new NotImplementedException();

        public IHeaderDictionary Headers => throw new NotImplementedException();

        public long Length => throw new NotImplementedException();

        public string Name { get; protected set; }

        public string FileName { get; protected set; }

        public void CopyTo(Stream target)
        {
            using (var readStream = OpenReadStream())
            {
                readStream.CopyTo(target);
            }
        }

        public async Task CopyToAsync(Stream target, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var readStream = OpenReadStream())
            {
                // We MUST await here to make sure CopyToAsync complete in this scope otherwise it will throw exception can't access closed file
                await readStream.CopyToAsync(target);
            }
        }

        public Stream OpenReadStream()
        {
            return File.OpenRead(_tempFilePath);
        }

        Stream IFormFile.OpenReadStream()
        {
            throw new NotImplementedException();
        }
    }
}
