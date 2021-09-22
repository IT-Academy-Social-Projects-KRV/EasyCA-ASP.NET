using FluentValidation;
using ProtocolService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProtocolService.Domain.ApiModel.Validators
{
    public class EuroProtocolValidator : AbstractValidator<EuroProtocol>
    {
        public EuroProtocolValidator()
        {

        }

        public static bool IsValidSerialNumber(string serialNumber)
        {
            string serialNumberPattern = @"^[0-9]{8}$";
            if (Regex.IsMatch(serialNumber, serialNumberPattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
