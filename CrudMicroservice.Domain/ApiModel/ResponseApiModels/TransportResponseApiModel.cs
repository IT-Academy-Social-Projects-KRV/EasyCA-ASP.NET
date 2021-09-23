using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrudMicroservice.Data.Entities;

namespace CrudMicroservice.Domain.ApiModel.ResponseApiModels
{
    public class TransportResponseApiModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
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
