using System;
using System.Collections.Generic;

namespace CrudMicroservice.Domain.ApiModel.RequestApiModels
{
    public class CarAccidentRequestApiModel
    {
        public CarAccidentRequestApiModel()
        {
            RegistrationDateTime = DateTime.Now;
        }
        public string SerialNumber { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public string InspectorId { get; set; }
        public AddressOfAccidentRequestApiModel Address { get; set; }
        public SideRequestApiModel SideOfAccident { get; set; }
        public string AccidentCircumstances { get; set; }
        public string TrafficRuleId { get; set; }
        public string DriverExplanation { get; set; }
        public IEnumerable<WitnessRequestApiModel> Witnesses { get; set; }
        public IEnumerable<EvidenceRequestApiModel> Evidences { get; set; }
        public DateTime CourtDTG { get; set; }
        public bool IsDocumentTakenOff { get; set; }
        public bool IsClosed { get; set; }
    }
}
