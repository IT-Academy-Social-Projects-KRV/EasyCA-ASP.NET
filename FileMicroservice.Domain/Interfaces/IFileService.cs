using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FileMicroservice.Domain.ApiModel.ResponseApiModels;
using Microsoft.AspNetCore.Http;

namespace FileMicroservice.Domain.Interfaces
{
    public interface IFileService
    {
        Task<ResponseApiModel<IEnumerable<string>>> UploadFiles(IFormFileCollection files);
        Task<IEnumerable<DownloadFileResponseApiModel>> DownloadFiles(string[] fileIds);
        Task<ResponseApiModel<HttpStatusCode>> DeleteFile(string fileId);
    }
}
