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
            if (string.IsNullOrWhiteSpace(circumstanceName))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
