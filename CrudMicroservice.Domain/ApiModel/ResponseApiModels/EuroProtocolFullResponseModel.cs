using System.Collections.Generic;

namespace CrudMicroservice.Domain.ApiModel.ResponseApiModels
{
    public class EuroProtocolFullResponseModel
    {
        public EuroProtocolResponseModel EuroProtocol { get; set; }
        public UserResponseModel UserDataSideA { get; set; }
        public UserResponseModel UserDataSideB { get; set; }
        public TransportResponseApiModel TransportSideA { get; set; }
        public TransportResponseApiModel TransportSideB { get; set; }
    }
}
