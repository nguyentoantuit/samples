using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FileReactASPNETCore.Exceptions;
using FileReactASPNETCore.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace FileReactASPNETCore.Extension
{
    public static class RequestExtension
    {
        private const char DoubleQuoteChar = '"';
        private static readonly FormOptions _defaultFormOptions = new FormOptions();

        public static bool IsMultipartContentType(this HttpRequest request)
        {
            return !string.IsNullOrEmpty(request.ContentType)
                   && request.ContentType.IndexOf("multipart/", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static string GetBoundary(this HttpRequest request, int lengthLimit)
        {
            var boundary = HeaderUtilities.RemoveQuotes(MediaTypeHeaderValue.Parse(request.ContentType).Boundary);

            if (StringSegment.IsNullOrEmpty(boundary))
            {
                throw new InvalidDataException("Missing content-type boundary.");
            }

            if (boundary.Length > lengthLimit)
            {
                throw new InvalidDataException(
                    $"Multipart boundary length limit {lengthLimit} exceeded.");
            }

            return boundary.ToString();
        }

        public static bool HasFileContentDisposition(ContentDispositionHeaderValue contentDisposition)
        {
            // Content-Disposition: form-data; name="myfile1"; filename="Misc 002.jpg"
            return contentDisposition != null
                   && contentDisposition.DispositionType.Equals("form-data")
                   && (!StringSegment.IsNullOrEmpty(contentDisposition.FileName)
                       || !StringSegment.IsNullOrEmpty(contentDisposition.FileNameStar));
        }

        /// <summary>
        /// Get all files from request
        /// </summary>
        /// <param name="maxSizeInBytes">Maximum bytes that we want support, default is 10485760 ~ 10MB</param>
        /// <returns></returns>
        public static async Task<List<IFormFile>> GetFilesAsync(this HttpRequest Request, long maxSizeInBytes = 10485760)
        {
            long fileSize = 0;
            var uploadTemplateInfo = new List<IFormFile>();
            var boundary = Request.GetBoundary(_defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, Request.Body);
            var section = await reader.ReadNextSectionAsync();
            while (section != null)
            {
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out ContentDispositionHeaderValue contentDisposition);
                if (hasContentDispositionHeader && HasFileContentDisposition(contentDisposition))
                {
                    var targetFilePath = Path.GetTempFileName();
                    using (var targetStream = File.Create(targetFilePath))
                    {
                        await section.Body.CopyToAsync(targetStream);
                        fileSize += targetStream.Length;
                        if (fileSize > maxSizeInBytes)
                        {
                            throw new FileMaxSizeException(maxSizeInBytes, fileSize);
                        }

                        TempFormFile tempFormFile = new TempFormFile(contentDisposition.FileName.ToString().Trim(DoubleQuoteChar),
                            contentDisposition.Name.ToString().Trim(DoubleQuoteChar), targetFilePath);
                        uploadTemplateInfo.Add(tempFormFile);
                    }
                }
                section = await reader.ReadNextSectionAsync();
            }

            return uploadTemplateInfo;
        }
    }
}
