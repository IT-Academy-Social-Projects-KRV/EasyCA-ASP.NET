using CrudMicroservice.Data.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CrudMicroservice.Domain.ApiModel.Validators
{
    class CAProtocolValidator : AbstractValidator<CarAccident>
    {
        public CAProtocolValidator()
        {
            RuleFor(n => n.SerialNumber).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Serial Number is required").
               NotNull().WithMessage("Serial Number could not be null").Must(IsValidSerialNumber).
               WithMessage("Something wrong with Serial Number");
            RuleFor(n => n.Witnesses).Cascade(CascadeMode).NotEmpty().
                 WithMessage("You must have at least one witness").Must(IsValidWitnesses).
                 WithMessage("Something went wrong");
            RuleFor(n => n.Address).Cascade(CascadeMode.Stop).NotNull().WithMessage("Address is required").
                Must(IsValidAddressOfAccident).WithMessage("Wrong address object");
            RuleFor(n => n.DriverExplanation).Cascade(CascadeMode.Stop).NotNull().WithMessage("Driver explanation is required").
                Must(IsValidDriverExplanation).WithMessage("Wrong address object");
            RuleFor(n => n.SideOfAccident).Cascade(CascadeMode.Stop).NotNull().WithMessage("Side is required").
                Must(IsValidSide).WithMessage("Wrong side object");
            RuleFor(n => n.TrafficRuleId).Cascade(CascadeMode.Stop).NotNull().WithMessage("Side is required").
                Must(IsValidTrafficRuleId).WithMessage("Wrong side object");
            RuleFor(n => n.AccidentCircumstances).Cascade(CascadeMode.Stop).NotNull().WithMessage("Side is required").
              Must(IsValidAccidentCircumstances).WithMessage("Wrong side object");
            RuleFor(n => n.RegistrationDateTime).Cascade(CascadeMode.Stop).NotNull().WithMessage("Side is required").
              Must(IsValidRegistrationDateTime).WithMessage("Wrong side object");
        }

        public static bool IsValidSerialNumber(string serialNumber)
        {
            string serialNumberPattern = @"^[0-9]{8}$";

            if (Regex.IsMatch(serialNumber, serialNumberPattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidWitnesses(List<Witness> Witnesses)
        {
            if (Witnesses.Count() >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidAddressOfAccident(AddressOfAccident address)
        {
            if (address != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidSide(SideCA side)
        {
            if (side != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidDriverExplanation(string explanation)
        {
            if (explanation != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidTrafficRuleId(string trafficRuleId)
        {
            if (trafficRuleId != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidAccidentCircumstances(string circumstance)
        {
            if (circumstance != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidRegistrationDateTime(DateTime date)
        {
            if (date < DateTime.Now)
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
