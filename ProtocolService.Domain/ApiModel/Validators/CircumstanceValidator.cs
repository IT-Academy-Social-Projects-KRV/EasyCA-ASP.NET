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
    public class CircumstanceValidator : AbstractValidator<Circumstance>
    {
        public CircumstanceValidator()
        {
            RuleFor(n => n.CircumstanceName).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Circumstance Name is required").
               NotNull().WithMessage("Circumstance Name could not be null").Must(IsValidCircumstanceName).
               WithMessage("Something wrong with circumstance name");
        }

        public static bool IsValidCircumstanceName(string circumstanceName)
        {
            string circumstanceNamePattern = @"^[А-ЩЬЮЯЇІЄҐA-Z]{1}[а-щьюяїієґa-z]{70}$"; //уточнити чи 70 символів досить

            if (Regex.IsMatch(circumstanceName, circumstanceNamePattern))
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
