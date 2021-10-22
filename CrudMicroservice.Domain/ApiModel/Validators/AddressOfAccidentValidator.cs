using FluentValidation;
using CrudMicroservice.Data.Entities;
using System.Text.RegularExpressions;

namespace CrudMicroservice.Domain.ApiModel.Validators
{
    public class AddressOfAccidentValidator: AbstractValidator<AddressOfAccident>
    {
        public AddressOfAccidentValidator()
        {
            RuleFor(n => n.City).Cascade(CascadeMode.Stop).NotNull().WithMessage("City cannot be null").
                NotEmpty().WithMessage("City is required").Must(IsValidCity).WithMessage("OMG-2!!!!");
            RuleFor(n => n.District).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("District is required").
               NotNull().WithMessage("District could not be null").Must(IsValidCity).
               WithMessage("Something wrong with district");
            RuleFor(n => n.Street).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Street is required").
                NotNull().WithMessage("Street could not be null").Must(IsValidCity).
                WithMessage("Something wrong with Street");
            RuleFor(n => n.CrossStreet).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Street is required").
                NotNull().WithMessage("Street could not be null").Must(IsValidCity).
                WithMessage("Something wrong with Street");
            RuleFor(n => n.CoordinatesOfLatitude).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Coordinates Of Latitude is required").
                NotNull().WithMessage("Coordinates Of Latitude could not be null").Must(IsValidCoordinate).
                WithMessage("Something wrong with Coordinates Of Latitude");
            RuleFor(n => n.CoordinatesOfLongitude).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Coordinates Of Longitude is required").
                NotNull().WithMessage("Coordinates Of Longitude could not be null").Must(IsValidCoordinate).
                WithMessage("Something wrong with Coordinates Of Longitude");
        }

        public static bool IsValidCity(string city)
        {
            string cityPattern = @"(^[А-ЩЬЮЯЇІЄҐA-Z]{1}[а-щьюяїієґa-z]{3,15}$)||(^[А-ЩЬЮЯЇІЄҐA-Z]{1}[а-щьюяїієґa-z]{3,12}[\-]{1}[А-ЩЬЮЯЇІЄҐA-Z]{1}[а-щьюяїієґa-z]{2,15})$";

            if (Regex.IsMatch(city, cityPattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidCoordinate(string coord)
        {
            string coordPattern = @"^[0-9]{2}\.[0-9]{6}$";

            if (Regex.IsMatch(coord, coordPattern))
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
