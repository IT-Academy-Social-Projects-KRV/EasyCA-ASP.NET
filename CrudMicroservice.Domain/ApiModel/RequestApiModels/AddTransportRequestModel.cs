using CrudMicroservice.Data.Entities;

namespace CrudMicroservice.Domain.ApiModel.RequestApiModels
{
    public class AddTransportRequestModel
    {
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
