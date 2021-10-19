﻿using System;
using System.Collections.Generic;

namespace CrudMicroservice.Domain.ApiModel.ResponseApiModels
{
    public class CarAccidentResponseApiModel
    {
        public string Id { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public string InspectorId { get; set; }
        public AddressOfAccidentResponseApiModel Address { get; set; }
        public SideResponseApiModel SideOfAccident { get; set; }
        public string AccidentCircumstances { get; set; }
        public string TrafficRuleId { get; set; }
        public string DriverExplanation { get; set; }
        public IEnumerable<WitnessResponseApiModel> Witnesses { get; set; }
        public IEnumerable<EvidenceResponseApiModel> Evidences { get; set; }
        public DateTime CourtDTG { get; set; }
        public bool IsDocumentTakenOff { get; set; }
        public bool IsClosed { get; set; }
    }
}
