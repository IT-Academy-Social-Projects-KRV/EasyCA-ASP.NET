using System.Collections.Generic;

namespace ProtocolService.Domain.ApiModel.RequestApiModels
{
    public class EvidenceRequestModel
    {
        public string Explanation { get; set; }
        public string PhotoSchema { get; set; }
        public List<string> Attachments { get; set; }
    }
}
