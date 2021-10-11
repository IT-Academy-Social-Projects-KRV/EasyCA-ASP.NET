using System.Collections.Generic;

namespace CrudMicroservice.Domain.ApiModel.RequestApiModels
{
    public class EvidenceRequestApiModel
    {
        public string Explanation { get; set; }
        public string PhotoSchema { get; set; }
        public IEnumerable<string> Attachments { get; set; }
    }
}
