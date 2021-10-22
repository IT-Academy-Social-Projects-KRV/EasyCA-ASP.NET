using CrudMicroservice.Data.Entities;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CrudMicroservice.Domain.ApiModel.Validators
{
    public class SideCAValidator:AbstractValidator<SideCA>
    {
        public SideCAValidator()
        {
            RuleFor(n => n.Email).Cascade(CascadeMode.Stop)
               .NotEmpty().WithMessage("Email is required").Length(6, 25)
               .Must(IsValidEmail).WithMessage("Wrong format of email").Must(IsValidEmail)
               .WithMessage("Invalid email");
            RuleFor(n => n.DriverLicenseSerial).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Driver license serial is required")
                .Must(IsValidDriverLicenseSerial).WithMessage("Driver license serial format is not valid");
        }

        public static bool IsValidEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9_.><=$#*&%!+-]+@[a-zA-Z0-9-]+\.[a-z0-9-.]+$";
            
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
            Regex driverLicenseSerialValidation = new Regex(@"[А-ЩЬЮЯЇІЄҐA-Z]{3}[0-9]{6}$");

            if (driverLicenseSerial == null) return false;

            if (driverLicenseSerialValidation.IsMatch(driverLicenseSerial))
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
