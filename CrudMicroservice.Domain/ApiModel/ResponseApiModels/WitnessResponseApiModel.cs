namespace CrudMicroservice.Domain.ApiModel.ResponseApiModels
{
    public class WitnessResponseApiModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WitnessAddress { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsVictim { get; set; }
    }
}