using System.Collections.Generic;

namespace CrudMicroservice.Domain.ApiModel.ResponseApiModels
{
    public class EvidenceResponseApiModel
    {
        public string Explanation { get; set; }
        public string PhotoSchema { get; set; }
        public IEnumerable<string> Attachments { get; set; }
    }
}