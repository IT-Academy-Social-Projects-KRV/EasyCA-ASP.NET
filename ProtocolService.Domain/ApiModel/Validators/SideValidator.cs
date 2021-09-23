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
            RuleFor(n => n.Email).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email is required").Length(6, 25)
                .Must(IsValidEmail).WithMessage("Wrong format of email").Must(IsValidEmail)
                .WithMessage("Invalid email");
            RuleFor(n => n.DriverLicenseSerial).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Driver license serial is required")
                .Must(IsValidDriverLicenseSerial).WithMessage("Driver license serial format is not valid");
            RuleFor(n => n.Damage).Cascade(CascadeMode).NotEmpty().
                 WithMessage("You must have at least one damage").Must(IsValidDamage).
                 WithMessage("Something went wrong");
            RuleFor(n => n.Circumstances).Cascade(CascadeMode).NotEmpty().
                 WithMessage("You must have at least one circumstance").Must(IsValidCircumstances).
                 WithMessage("Something went wrong");
            
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

        public static bool IsValidDamage(string damage)
        {
            if (damage != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidCircumstances(List<int> Circumstances)
        {
            if (Circumstances.Count() > 0)
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
