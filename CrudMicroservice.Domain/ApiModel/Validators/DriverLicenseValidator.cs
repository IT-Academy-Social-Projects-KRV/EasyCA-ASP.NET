using System;
using FluentValidation;
using System.Text.RegularExpressions;
using CrudMicroservice.Data.Entities;
using System.Collections.Generic;
using MongoDB.Bson;

namespace CrudMicroservice.Domain.ApiModel.Validators
{
    public class DriverLicenseValidator : AbstractValidator<DriverLicense>
    {
        public DriverLicenseValidator()
        {
            RuleFor(n => n.LicenseSerialNumber).Cascade(CascadeMode.Stop).NotEmpty().
                WithMessage("License serial number is required").Must(IsValidDriverLicense).
                WithMessage("Wrong format of serial number");
            RuleFor(n => n.IssuedBy).Cascade(CascadeMode.Stop).NotEmpty().
                WithMessage("Issued authority is required").Must(IsValidAuthority).
                WithMessage("Invalid authrity");
            RuleFor(n => n.ExpirationDate).Cascade(CascadeMode.Stop).NotEmpty().
                WithMessage("Expiration date is required").Must(IsValidExpDate).
                WithMessage("Your driver license is expired");
            RuleFor(n => n.UserCategories).Cascade(CascadeMode).NotEmpty().
                WithMessage("You must have at least one category").Must(IsValidCategories).
                WithMessage("Something went wrong");

        }
        public static bool IsValidDriverLicense(string driverNumber)
        {
            string patternDr = @"^[А-ЩЬЮЯЇІЄҐA-ZЯ]{3}[0-9]{6}";
            Regex driverSerial = new Regex(patternDr);

            if (driverSerial.IsMatch(driverNumber))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsValidAuthority(string issuedBy)
        {
            List<string> authorities = new List<string>()
            { "0541", "0741", "1242", "1441", "1841",
            "2142", "2341", "2641", "3247", "8041", "8042",
            "3541","4442","4641", "4841","5141", "5154","5341",
            "5641","5946","6141","6341","6541","6841","7141", "7441","7341"
            };

            if (authorities.Contains(issuedBy))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsValidExpDate(DateTime expirationDate)
        {
            DateTime today = DateTime.Today;

            if (expirationDate > today)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsValidCategories(List<string> UserCategories)
        {
            int count = 0;
            foreach (var item in UserCategories)
            {
                if (TransportCategoryValidator.IsValidCategory(item))
                {
                    count++;
                }
                else
                {
                    continue;
                }
            }

            if (count == UserCategories.Count)
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
