using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FileMicroservice.Data;
using FileMicroservice.Domain.ApiModel.ResponseApiModels;
using FileMicroservice.Domain.Errors;
using FileMicroservice.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace FileMicroservice.Domain.Services
{
    public class FileService : IFileService
    {
        private readonly FileDbContext _dbContext;
        private readonly IGridFSBucket _bucket;

        public FileService(FileDbContext dbContext)
        {
            _dbContext = dbContext;
            _bucket = _dbContext.GetFileBucket();
        }

        public async Task<IEnumerable<DownloadFileResponseApiModel>> DownloadFiles(string[] fileIds)
        {
            var fileList = new List<DownloadFileResponseApiModel>();

            foreach (var fileId in fileIds)
            {
                if (!ObjectId.TryParse(fileId, out var id))
                    throw new RestException(HttpStatusCode.BadRequest, "Invalid Id");

                var fileStream = await _bucket.OpenDownloadStreamAsync(id);
                var byteFile = new byte[fileStream.FileInfo.Metadata["Content-Length"].AsInt64];
                fileStream.Read(byteFile, 0, byteFile.Length);

                if (byteFile.Length == 0) 
                    throw new RestException(HttpStatusCode.NotFound, "File not found");

                fileList.Add(new DownloadFileResponseApiModel { File = byteFile, Filename = fileStream.FileInfo.Filename, ContentType = fileStream.FileInfo.Metadata["Content-Type"].AsString });
            }

            return fileList;
        }

        public async Task<ResponseApiModel<HttpStatusCode>> DeleteFile(string fileId)
        {
            if (!ObjectId.TryParse(fileId, out var id))
                throw new RestException(HttpStatusCode.BadRequest, "Invalid file id");

            await _bucket.DeleteAsync(id);

            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "File successfully deleted");
        }

        public async Task<ResponseApiModel<IEnumerable<string>>> UploadFiles(IFormFileCollection files)
        {
            if (files.Count == 0)
                throw new RestException(HttpStatusCode.BadRequest, "Empty request");

            List<string> idList = new();

            foreach (var file in files)
            {
                var stream = file.OpenReadStream();
                var byteArr = new byte[stream.Length];
                stream.Read(byteArr, 0, byteArr.Length);
                GridFSUploadOptions options = new() { Metadata = new BsonDocument { { "Content-Type", file.ContentType }, { "Content-Length", file.Length } } };
                var id = await _bucket.UploadFromBytesAsync(file.FileName, byteArr, options);
                stream.Close();
                idList.Add(id.ToString());
            }

            return new ResponseApiModel<IEnumerable<string>>(idList, true, "Success!");
        }
    }
}
