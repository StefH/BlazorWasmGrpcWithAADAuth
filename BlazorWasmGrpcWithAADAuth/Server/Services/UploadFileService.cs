using Fileupload;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace Server.Services
{
    public partial class UploadFileService : FileUpload.FileUploadBase
    {
        public override async Task<UploadResponse> Upload(UploadRequest request, ServerCallContext context)
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
            
            var bytes = request.FileContent;

            return new UploadResponse { FileSize = bytes.Length };
        }
    }
}