﻿using FluentValidation;
using CrudMicroservice.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CrudMicroservice.Domain.ApiModel.Validators
{
    public class EuroProtocolValidator : AbstractValidator<EuroProtocol>
    {
        public EuroProtocolValidator()
        {
            RuleFor(n => n.SerialNumber).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Serial Number is required").
                NotNull().WithMessage("Serial Number could not be null").Must(IsValidSerialNumber).
                WithMessage("Something wrong with Serial Number");
            RuleFor(n => n.Witnesses).Cascade(CascadeMode).NotEmpty().
                 WithMessage("You must have at least one witness").Must(IsValidWitnesses).
                 WithMessage("Something went wrong");
            RuleFor(n => n.Address).Cascade(CascadeMode.Stop).NotNull().WithMessage("Address is required").
                Must(IsValidAddressOfAccident).WithMessage("Wrong address object");
            RuleFor(n => n.SideA).Cascade(CascadeMode.Stop).NotNull().WithMessage("Side is required").
                Must(IsValidSide).WithMessage("Wrong side object");
            RuleFor(n => n.SideB).Cascade(CascadeMode.Stop).NotNull().WithMessage("Side is required").
                Must(IsValidSide).WithMessage("Wrong side object");
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

        public static bool IsValidAddressOfAccident (AddressOfAccident address)
        {
            if(address != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidSide(Side side)
        {
            if(side != null)
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
