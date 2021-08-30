using AccountService.Data.Entities;

namespace AccountService.Domain.ApiModel.RequestApiModels
{
    public class UpdateTransportRequestModel
    {
        public string Id { get; set; }
        public string ProducedBy { get; set; }
        public string Model { get; set; }
        public string CategoryName { get; set; }
        public string VINCode { get; set; }
        public string CarPlate { get; set; }
        public string Color { get; set; }
        public int YearOfProduction { get; set; }
        public Insuarance InsuaranceNumber { get; set; }
    }
}
