using System;
using System.Collections.Generic;

namespace CrudMicroservice.Domain.ApiModel.ResponseApiModels
{
    public class CarAccidentResponseApiModel
    {
        public string Id { get; set; }
        public string SerialNumber { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public string InspectorId { get; set; }
        public AddressOfAccidentResponseApiModel Address { get; set; }
        public SideCAResponseApiModel SideOfAccident { get; set; }
        public string AccidentCircumstances { get; set; }
        public string TrafficRuleId { get; set; }
        public string DriverExplanation { get; set; }
        public IEnumerable<WitnessResponseApiModel> Witnesses { get; set; }
        public IEnumerable<EvidenceResponseApiModel> Evidences { get; set; }
        public DateTime CourtDTG { get; set; }
        public bool IsDocumentTakenOff { get; set; }
    }
}
