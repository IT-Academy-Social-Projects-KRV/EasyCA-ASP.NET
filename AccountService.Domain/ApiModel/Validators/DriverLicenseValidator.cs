using System;
using FluentValidation;
using System.Text.RegularExpressions;
using AccountService.Data.Entities;
using System.Collections.Generic;
using MongoDB.Bson;

namespace AccountService.Domain.ApiModel.Validators
{
    public class DriverLicenseValidator:AbstractValidator<DriverLicense>
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
            int count = 0;

            foreach (string item in authorities)
            {
                if (item == issuedBy) count++;
            }

            if (count == 1)
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

            if (((expirationDate.ToUniversalTime().Year - today.ToUniversalTime().Year) <= 0) &&
                ((expirationDate.ToUniversalTime().Month - today.ToUniversalTime().Month) <= 0) &&
                ((expirationDate.ToUniversalTime().Day - today.ToUniversalTime().Day) <= 0))
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
            List<string> categories = new List<string>() { "A1", "A", "B1", "B", "C", "C1", "D", "D1", "BE", "CE", "DE", "C1E", "D1E", "T" };
            int count = 0;
            
            foreach (var item in UserCategories)
            {
                foreach (string category in categories)
                {
                    if (item == category)
                    {
                        count++;
                    }
                }
            }

            if (count > 0)
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
