﻿namespace CrudMicroservice.Domain.ApiModel.RequestApiModels
{
    public class WitnessRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WitnessAddress { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsVictim { get; set; }
    }
}
