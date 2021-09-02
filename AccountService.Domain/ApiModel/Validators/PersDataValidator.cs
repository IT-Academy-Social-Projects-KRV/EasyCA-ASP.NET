using AccountService.Data.Entities;
using AccountService.Domain.ApiModel.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EasyCA.Core.Domain.ApiModels.Validators
{
    public class PersDataValidator:AbstractValidator<PersonalData>
    {
        public PersDataValidator()
        {
            RuleFor(n => n.IPN).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Identification number of taxpayer is required").
                Must(IsIPN).WithMessage("Wrong IPN format - it should consist of 10 digits").Length(10).
                WithMessage("Insufficient number of digits").
                NotNull().WithMessage("Value is nulled");
            RuleFor(n => n.BirthDay).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Date of birth is required").
                NotNull().Must(IsValidDate).WithMessage("Invalid date");
            RuleFor(n => n.JobPosition).Cascade(CascadeMode.Stop).Must(IsValidJob).
                WithMessage("Put position, organization and organization name");
            RuleFor(n => n.UserAddress).Cascade(CascadeMode.Stop).NotNull().WithMessage("Address is required").
                Must(IsValidAddress).WithMessage("Wrong address object");
            RuleFor(n => n.UserDriverLicense).Cascade(CascadeMode.Stop).NotNull().WithMessage("DL is required").
                Must(IsValidDriverLicense).WithMessage("DL doesnot meet criteria");
        }
        public static bool IsIPN(string taxpayerIPN)
        {
            string patternIPN = @"[0-9]{10}";
            Regex isIPN = new Regex(patternIPN);

            if (isIPN.IsMatch(taxpayerIPN))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsValidDate(DateTime birthday)
        {
            DateTime today = DateTime.Today;
            if (((birthday.Year > (today.Year - 83)) && (birthday.Year < (today.Year - 14)) && ((birthday.Month >= 1) && (birthday.Month <= 12))) && ((birthday.Day >= 1) && (birthday.Day <= 31)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsValidJob(string jobsString)
        {
            string jobPattern = @"^[А-ЩЬЮЯЇІЄҐA-Zа-щьюяїієґa-z]{3,20}$";
            Regex newJob = new Regex(jobPattern);

            if (newJob.IsMatch(jobsString))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsValidAddress(Address userAddress)
        {
            AddressValidator addressValidator = new AddressValidator();
            if (addressValidator.Validate(userAddress).IsValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsValidDriverLicense(DriverLicense userPass)
        {
            DriverLicenseValidator passValidator = new DriverLicenseValidator();

            if (passValidator.Validate(userPass).IsValid)
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
