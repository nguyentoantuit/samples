using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FileReactASPNETCore.Extension;
using FileReactASPNETCore.Filters;
using FileSample.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileReactASPNETCore.Controllers
{
    [Route("api/files")]
    [Produces("application/json")]
    public class FileController : Controller
    {
        public const int AttachmentsMaxSize = 3040870;
        private readonly IProcessUploadFileService _processUploadFileService;

        public FileController(IProcessUploadFileService processUploadFileService)
        {
            _processUploadFileService = processUploadFileService ?? throw new System.ArgumentNullException(nameof(processUploadFileService));
        }

        [HttpPost("streaming")]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> UploadStreamFiles()
        {
            if (!Request.IsMultipartContentType())
            {
                throw new InvalidDataException($"Expected a multipart request, but got {Request.ContentType}");
            }

            List<IFormFile> files = await Request.GetFilesAsync(AttachmentsMaxSize);
            var result = await _processUploadFileService.UploadFileToStorageAsync(files);
            return Ok(result);
        }

        [HttpPost("binding")]
        public async Task<IActionResult> UploadBindingFiles(IList<IFormFile> files)
        {
            var result = await _processUploadFileService.UploadFileToStorageAsync(files);
            return Ok(result);
        }
    }
}
