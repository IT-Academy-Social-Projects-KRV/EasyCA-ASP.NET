using System;
using System.Collections.Generic;

namespace CrudMicroservice.Domain.ApiModel.RequestApiModels
{
    public class CarAccidentRequestModel
    {
        public CarAccidentRequestModel()
        {
            RegistrationDateTime = DateTime.Now;
        }
        public string SerialNumber { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public AddressOfAccidentRequestModel Address { get; set; }
        public SideRequestModel SideOfAccident { get; set; }
        public string AccidentCircumstances { get; set; }
        public string TrafficRuleId { get; set; }
        public string DriverExplanation { get; set; }
        public IEnumerable<WitnessRequestModel> Witnesses { get; set; }
        public IEnumerable<EvidenceRequestModel> Evidences { get; set; }
        public DateTime CourtDTG { get; set; }
        public bool IsDocumentTakenOff { get; set; }
    }
}
