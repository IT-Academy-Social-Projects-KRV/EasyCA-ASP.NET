using FluentValidation;
using CrudMicroservice.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CrudMicroservice.Domain.ApiModel.Validators
{
    public class WitnessValidator : AbstractValidator<Witness>
    {
        public WitnessValidator()
        {
            RuleFor(n => n.FirstName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("First Name is Empty").
                Length(2, 20).WithMessage("First Name is Invalid")
                .Must(IsValidFirstName).WithMessage("FirstName contains invalid characters");
            RuleFor(n => n.LastName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Last Name is Empty").
                Length(2, 20).WithMessage("Last Name is Invalid")
                .Must(IsValidFirstName).WithMessage("Last Name contains invalid characters");
            RuleFor(n => n.PhoneNumber).Cascade(CascadeMode.Stop).
                NotEmpty().WithMessage("Phone number is required").Length(10).WithMessage("Length of phone number is invalid")
                .Must(IsPhoneNumber).WithMessage("Phone number format is not valid - try 0*********");
            RuleFor(n => n.WitnessAddress).Cascade(CascadeMode.Stop).
                NotEmpty().WithMessage("Witness address is required")
                .Must(IsPhoneNumber).WithMessage("Witness address format is not valid");

        }

        public static bool IsValidFirstName(string firstName)
        {
            string firstNamePattern = @"^[А-ЩЬЮЯЇІЄҐA-Z][а-щьюяїієґa-z]*$";
            Regex firstNameRegex = new Regex(firstNamePattern);

            if (Regex.IsMatch(firstName, firstNamePattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidAddress(string address) // уточнити
        {
            string cityPattern = @"([А-ЩЬЮЯЇІЄҐA-Z]{1}[а-щьюяїієґa-z]{3,15})|([А-ЩЬЮЯЇІЄҐA-Z]{1}[а-щьюяїієґa-z]{3,12}-[А-ЩЬЮЯЇІЄҐA-Z]{1}[а-щьюяїієґa-z]{2,15})";
            string streetPattern = @"[А-ЩЬЮЯЇІЄҐA-Z]{1}[а-щьюяїієґa-z]{3,30}|([А-ЩЬЮЯЇІЄҐA-Z]{1}[а-щьюяїієґa-z]{2,30}-[А-ЩЬЮЯЇІЄҐA-Z]{1}[а-щьюяїієґa-z]{2,30})|([А-ЩЬЮЯЇІЄҐA-Z]{1}[\.]{1}[А-ЩЬЮЯЇІЄҐA-Z]{1}[а-щьюяїієґa-z]{2,30})";
            string numberOfHousePattern = @"([0-9]{1,4})|([0-9]{4}/[А-ЩЬЮЯЇІЄҐA-Z а-щьюяїієґa-z]{1})|([0-9]{1,4}/[0-9]{1,4})";
            string numberOfApartmentPattern = @"^[0-9]{1,4}";
            string addressPattern = "^" + cityPattern + streetPattern + numberOfHousePattern + numberOfApartmentPattern + "$";
            Regex addressValidation = new Regex(addressPattern);

            if (address == null) return false;

            if (addressValidation.IsMatch(address))
            {
                return true;
            }
            else
            {                
                return false;
            }
        }

        public static bool IsPhoneNumber(string phoneNumber)
        {
            string phoneNumberPattern = @"[0-9]{10}$";
            Regex phoneValidation = new Regex(phoneNumberPattern);

            if (phoneNumber == null) return false;

            if (phoneValidation.IsMatch(phoneNumber))
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
