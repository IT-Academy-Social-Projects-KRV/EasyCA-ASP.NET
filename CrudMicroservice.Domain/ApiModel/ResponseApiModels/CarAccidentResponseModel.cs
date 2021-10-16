using System;
using System.Collections.Generic;

namespace CrudMicroservice.Domain.ApiModel.ResponseApiModels
{
    public class CarAccidentResponseModel
    {
        public string Id { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public AddressOfAccidentResponseModel Address { get; set; }
        public SideResponseModel SideOfAccident { get; set; }
        public string AccidentCircumstances { get; set; }
        public string TrafficRuleId { get; set; }
        public string DriverExplanation { get; set; }
        public IEnumerable<WitnessResponseModel> Witnesses { get; set; }
        public IEnumerable<EvidenceResponseModel> Evidences { get; set; }
        public DateTime CourtDTG { get; set; }
        public bool IsDocumentTakenOff { get; set; }
    }
}
