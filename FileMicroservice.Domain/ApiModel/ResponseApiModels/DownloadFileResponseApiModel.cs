namespace FileMicroservice.Domain.ApiModel.ResponseApiModels
{
    public class DownloadFileResponseApiModel
    {
        public byte[] File { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }
    }
}
