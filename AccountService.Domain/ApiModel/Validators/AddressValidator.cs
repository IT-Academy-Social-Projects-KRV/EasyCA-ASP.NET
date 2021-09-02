using FluentValidation;
using System.Text.RegularExpressions;
using AccountService.Data.Entities;

namespace AccountService.Domain.ApiModel.Validators
{
    public class AddressValidator:AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(n => n.Country).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Country is required").
                NotNull().WithMessage("Country is not null").Must(IsValidCountry).WithMessage("Wrong country");
            RuleFor(n => n.Region).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Region is required").
                NotNull().WithMessage("Region cannot be null").Must(IsValidRegion).WithErrorCode("OMG!!!");
            RuleFor(n => n.City).Cascade(CascadeMode.Stop).NotNull().WithMessage("City cannot be null").
                NotEmpty().WithMessage("City is required").Must(IsValidCity).WithMessage("OMG-2!!!!");
            RuleFor(n => n.District).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("District is required").
                NotNull().WithMessage("District could not be null").Must(IsValidCity).
                WithMessage("Something wrong with district");
            RuleFor(n=>n.Street).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Street is required").
                NotNull().WithMessage("Street could not be null").Must(IsValidCity).
                WithMessage("Something wrong with Street");
            RuleFor(n => n.Building).Cascade(CascadeMode.Stop).NotEmpty().NotNull().Must(IsValidBuilding);
            RuleFor(n => n.Appartament).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Appartament is required").
                NotNull().WithMessage("Appartament could not be null").Must(IsValidAppNumber).
                WithMessage("Something wrong with Appartament");
            RuleFor(n=>n.PostalCode).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("ZIPcode is required").
                NotNull().WithMessage("ZIPcode could not be null").Must(IsValidPostalCode).
                WithMessage("Something wrong with postal code");
        }
        public static bool IsValidCountry(string country)
        {
            string countryPattern = @"^[А-ЩЬЮЯЇІЄҐA-Z]{1}[а-щьюяїієґa-z]*$";

            if (Regex.IsMatch(country, countryPattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsValidRegion(string region)
        {
            string regionPattern = @"(^[А-ЩЬЮЯЇІЄҐA-Z]{1}[а-щьюяїієґa-z]{2,15}$)|(^[А-ЩЬЮЯЇІЄҐA-Z]{1}[а-щьюяїієґa-z]{2,12}[\-]{1}[А-ЩЬЮЯЇІЄҐA-Z]{1}[а-щьюяїієґa-z]{2,15}$)";
            
            if (Regex.IsMatch(region, regionPattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsValidCity(string city)
        {
            string cityPattern = @"(^[А-ЩЬЮЯЇІЄҐA-Z]{1}[а-щьюяїієґa-z]{3,15}$)|(^[А-ЩЬЮЯЇІЄҐA-Z]{1}[а-щьюяїієґa-z]{3,12}[\-]{1}[А-ЩЬЮЯЇІЄҐA-Z]{1}[а-щьюяїієґa-z]{2,15})$";
            
            if (Regex.IsMatch(city, cityPattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsValidBuilding(string buildNumber)
        {
            string buildPattern = @"([0-9]{1,4}$)|([0-9]{1,4}\/[0-9]{1,2}$)|([0-9]{1,4}\/[а-я]{1,2}$)";
            
            if (Regex.IsMatch(buildNumber, buildPattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsValidAppNumber(int appNumber)
        {
            if ((appNumber > 0) && (appNumber < 999))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsValidPostalCode(string postalCode)
        {
            string zipPattern = @"^[0-9]{5}$";

            if (Regex.IsMatch(postalCode, zipPattern))
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
