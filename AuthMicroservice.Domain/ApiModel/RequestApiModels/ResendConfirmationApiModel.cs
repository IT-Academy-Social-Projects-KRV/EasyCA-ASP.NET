using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthMicroservice.Domain.ApiModel.RequestApiModels
{
    public class ResendConfirmationApiModel
    {
        public string Email { get; set; }

        public string ResendConfirmationURI { get; set; }
    }
}
