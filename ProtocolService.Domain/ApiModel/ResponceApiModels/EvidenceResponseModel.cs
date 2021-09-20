namespace ProtocolService.Domain.ApiModel.ResponceApiModels
{
    public class EvidenceResponseModel
    {
        public string Explanation { get; set; }
        public string PhotoSchema { get; set; }
        public System.Collections.Generic.IEnumerable<string> Attachments { get; set; }
    }
}