using System;
using FluentValidation;
using System.Text.RegularExpressions;
using CrudMicroservice.Data.Entities;

namespace CrudMicroservice.Domain.ApiModel.Validators
{
    public class TransportValidator:AbstractValidator<Transport>
    {
        public TransportValidator()
        {
            RuleFor(n => n.ProducedBy).Cascade(CascadeMode.Stop).NotEmpty().
                WithMessage("This fiels is required").Length(2, 25).
                WithMessage("Insufficient number of letters").Must(IsValidProducer).
                WithMessage("Something wrong with this company name");
            RuleFor(n => n.Model).Cascade(CascadeMode.Stop).NotEmpty().
                WithMessage("Model is required").Length(2, 23).Must(IsValidModel).
                WithMessage("Invalid characters in model name");
            RuleFor(n => n.CarCategory).Cascade(CascadeMode.Stop).NotEmpty().
                WithMessage("Category of this car is required").Must(IsValidCategory).
                WithMessage("Category does not exist");
            RuleFor(n => n.VINCode).Cascade(CascadeMode.Stop).NotEmpty().
                WithMessage("VIN code is required").Length(15, 17).WithMessage("Invalid length of VIN").
                Must(IsValidVinCode).WithMessage("Invalid VIN");
            RuleFor(n => n.CarPlate).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Car plate is required").
                Length(8).WithMessage("Invalid or Old format").Must(IsValidCarPlate).WithMessage("Invalid format");
            RuleFor(n => n.Color).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Is not mandatory, but color required").
                Length(3, 20).WithMessage("Invalid length of color name").Must(IsValidColor).WithMessage("Wrong color - visit LOR");
            RuleFor(n => n.YearOfProduction).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Year of production is required").
                NotNull().WithMessage("Cannot be null").Must(IsValidYearProd).WithMessage("Wrong year");
            RuleFor(n => n.InsuaranceNumber).Cascade(CascadeMode.Stop).NotNull().WithMessage("Cannot be null").
                NotEmpty().WithMessage("Cannot be empty insuarance").Must(IsValidInsuarance).WithMessage("Invalid insuarance");
        }
        public static bool IsValidProducer(string producer)
        {
            Regex prodCompany = new Regex(@"^[А-ЩЬЮЯЇІЄҐA-Z]{1}[А-ЩЬЮЯЇІЄҐA-Zа-щьюяїієґa-z]*$");
            
            if (prodCompany.IsMatch(producer))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsValidModel(string model)
        {
            Regex modelCheck = new Regex(@"\w*$");

            if (modelCheck.IsMatch(model))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsValidCategory(TransportCategory category)
        {
            TransportCategoryValidator categoryValid = new TransportCategoryValidator();
            
            if (categoryValid.Validate(category).IsValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsValidVinCode(string vinCode)
        {
            string patternVin = @"^(([a-h,A-H,j-n,J-N,p-z,P-Z,0-9]{9})([a-h,A-H,j-n,J-N,p,P,r-t,R-T,v-z,V-Z,0-9])([a-h,A-H,j-n,J-N,p-z,P-Z,0-9])(\d{6}))$";
            Regex vinCheck = new Regex(patternVin);

            if (vinCheck.IsMatch(vinCode))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsValidCarPlate(string currentPlate)
        {
            string platePattern = @"^[АВСРІ]{1}[АIВСЕНКМТРХОЄ]{1}\d{4}[А-Я]{2}$";
            Regex plateReg = new Regex(platePattern);

            if (plateReg.IsMatch(currentPlate))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsValidColor(string color)
        {
            string colorPattern = @"^[А-ЩЬЮЯЇІЄҐA-Zа-щьюяїієґa-z]{1}[а-щьюяїієґa-z]+$";
            Regex colorCheck = new Regex(colorPattern);

            if (colorCheck.IsMatch(color))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsValidYearProd(int year)
        {
            DateTime now = DateTime.Today;
            if ((year > 1970) && (year <= now.Year))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsValidInsuarance(Insuarance thisOne)
        {
            InsuaranceValidator insValid = new InsuaranceValidator();

            if (insValid.Validate(thisOne).IsValid)
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
