using System.Collections.Generic;

namespace CrudMicroservice.Domain.ApiModel.ResponseApiModels
{
    public class EuroProtocolFullResponseApiModel
    {
        public EuroProtocolResponseApiModel EuroProtocol { get; set; }
        public UserResponseApiModel UserDataSideA { get; set; }
        public UserResponseApiModel UserDataSideB { get; set; }
        public TransportResponseApiModel TransportSideA { get; set; }
        public TransportResponseApiModel TransportSideB { get; set; }
    }
}
