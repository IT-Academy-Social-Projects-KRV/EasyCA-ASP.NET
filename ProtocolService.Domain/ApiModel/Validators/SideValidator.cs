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
    public class SideValidator : AbstractValidator<Side>
    {
        public SideValidator()
        {
            RuleFor(n => n.DriverLicenseSerial).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Driver license serial is required")
                .Must(IsValidDriverLicenseSerial).WithMessage("Driver license serial format is not valid");
        }

        public static bool IsValidEmail(string email)
        {
            string emailPattern = @"^.+@.+\..+$";
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            if (Regex.IsMatch(email, emailPattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidDriverLicenseSerial(string driverLicenseSerial)
        {
            string driverLicenseSerialPattern = @"[А-ЩЬЮЯЇІЄҐA-Z]{3}[0-9]{6}$";
            Regex driverLicenseSerialValidation = new Regex(driverLicenseSerialPattern);

            if (driverLicenseSerial == null) return false;

            if (driverLicenseSerialValidation.IsMatch(driverLicenseSerial))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Error in PHN");
                return false;
            }
        }
    }
}
