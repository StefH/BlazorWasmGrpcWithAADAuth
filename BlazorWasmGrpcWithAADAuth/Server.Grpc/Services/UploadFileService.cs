using System;
using System.Threading.Tasks;
using Fileupload;
using Grpc.Core;

namespace Server.Grpc.Services
{
    public class UploadFileService : FileUpload.FileUploadBase
    {
        public override async Task<UploadResponse> Upload(UploadRequest request, ServerCallContext context)
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
            
            var bytes = request.FileContent;

            return new UploadResponse { FileSize = bytes.Length };
        }
    }
}