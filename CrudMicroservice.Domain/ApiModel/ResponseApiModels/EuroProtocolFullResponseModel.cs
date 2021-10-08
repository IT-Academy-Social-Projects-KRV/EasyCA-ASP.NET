using CrudMicroservice.Data.Entities;
using System.Collections.Generic;

namespace CrudMicroservice.Domain.ApiModel.ResponseApiModels
{
    public class EuroProtocolFullResponseModel
    {
        public EuroProtocolSimpleResponseModel EuroProtocolSimple { get; set; }
        public UserResponseModel UserDataSideA { get; set; }
        public UserResponseModel UserDataSideB { get; set; }
        public TransportResponseApiModel TransportSideA { get; set; }
        public TransportResponseApiModel TransportSideB { get; set; }
        public IEnumerable<Witness> Witnesses { get; set; }
    }
}
