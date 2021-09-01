using FluentValidation;
using AccountService.Data.Entities;
using System.Collections.Generic;

namespace AccountService.Domain.ApiModel.Validators
{
    public class TransportCategoryValidator:AbstractValidator<TransportCategory>
    {
        public TransportCategoryValidator()
        {
            RuleFor(n => n.CategoryName).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Could not be empty").
                Must(IsValidCategory).WithMessage("Given transport category does not exist");
        }
        public static bool IsValidCategory(string category)
        {
            List<string> categories = new List<string>() { "A1", "A", "B1", "B", "C", "C1", "D", "D1", "BE","CE","DE", "C1E", "D1E", "T"};
            int count = 0;
            foreach (string item in categories)
            {
                if (category == item) count++;
            }
            if (count > 0) return true;
            else return false;
        }
    }
}
