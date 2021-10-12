using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConfig.Models.Responses
{
    public class TransportResponseModelRabbitMQ
    {
        public string Id { get; set; }
        public string ProducedBy { get; set; }
        public string Model { get; set; }
        public string CategoryName { get; set; }
        public string VINCode { get; set; }
        public string CarPlate { get; set; }
        public string Color { get; set; }
        public int YearOfProduction { get; set; }
        public InsuaranceModelRabbitMQ InsuaranceNumber { get; set; }    
    }
    public class InsuaranceModelRabbitMQ
    {
        public string CompanyName { get; set; }
        public string SerialNumber { get; set; }
    }
}

