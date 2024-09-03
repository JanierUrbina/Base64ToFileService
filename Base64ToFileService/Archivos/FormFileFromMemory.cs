using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Base64ToFileService.Archivos
{
    public class FormFileFromMemory : IFormFile
    {
        private readonly byte[] fileBytes;
        private readonly string fileName;
        private static readonly FileExtensionContentTypeProvider _contentTypeProvider = new FileExtensionContentTypeProvider();

        public FormFileFromMemory(byte[] fileBytes, string fileName)
        {
            this.fileBytes = fileBytes ?? throw new ArgumentNullException(nameof(fileBytes));
            this.fileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
        }

        public string ContentType
        {
            get
            {
                _contentTypeProvider.TryGetContentType(fileName, out var contentType);
                return contentType ?? "application/octet-stream"; // Default MIME type
            }
        }

        public string FileName => fileName;

        public long Length => fileBytes.Length;

        public async Task CopyToAsync(Stream target, System.Threading.CancellationToken cancellationToken = default)
        {
            await using (var stream = new MemoryStream(fileBytes))
            {
                await stream.CopyToAsync(target, cancellationToken);
            }
        }

        public Stream OpenReadStream()
        {
            return new MemoryStream(fileBytes);
        }

        // Implementación adicional para que sea más completa
        public void CopyTo(Stream target)
        {
            using (var sourceStream = new MemoryStream(fileBytes))
            {
                sourceStream.CopyTo(target);
            }
        }

        public IHeaderDictionary Headers => new HeaderDictionary();

        public string Name => "file";

        public string ContentDisposition
        {
            get
            {               
                var contentDisposition = $"form-data; name=\"{Name}\"; filename=\"{FileName}\"";
                return contentDisposition;
            }
        }
    }
}
