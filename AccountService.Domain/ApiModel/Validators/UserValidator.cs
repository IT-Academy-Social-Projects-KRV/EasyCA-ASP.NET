using System;
using FluentValidation;
using System.Text.RegularExpressions;
using System.Net.Mail;
using AccountService.Data.Entities;

namespace EasyCA.Core.Domain.ApiModels.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(n => n.FirstName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("First Name is Empty").
                Length(2, 20).WithMessage("Length ({TotalLength}) of First Name is Invalid")
                .Must(IsMatching).WithMessage("FirstName contains invalid characters");
            RuleFor(n => n.LastName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Last Name is Empty").
                Length(2, 20).WithMessage("Length ({TotalLength}) of Last Name is Invalid")
                .Must(IsMatching).WithMessage("Last Name contains invalid characters");
            RuleFor(n => n.Email).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email is required").Length(6, 25)
                .Must(IsEmail).WithMessage("Wrong format of email").Must(IsValidEmail)
                .WithMessage("Invalid email");
            RuleFor(n => n.PhoneNumber).Cascade(CascadeMode.Stop).
                NotEmpty().WithMessage("Phone number is required").Length(10).WithMessage("Length of phone number is invalid")
                .Must(IsPhoneNumber).WithMessage("Phone number format is not valid - try 0*********");
            RuleFor(n => n.UserName).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("User nickName is required").
                Length(6, 12).WithMessage("Invalid length of nickname").Must(IsUsername).WithMessage("Invalid characters in nickname");
        }
        public static bool IsMatching(string firstName)
        {
            string pattern = @"^[А-ЩЬЮЯЇІЄҐA-Z][а-щьюяїієґa-z]*$";
            Regex firstNameRegex = new Regex(pattern);

            if (firstNameRegex.IsMatch(firstName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public static bool IsValidEmail(string email)
        {
            string theEmailPattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                   + "@"
                                   + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z";

            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            Regex emailValidation = new Regex(theEmailPattern);

            if (emailValidation.IsMatch(email))
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
                    Console.WriteLine("Error in PHN");
                    return false;
              } 
        }
        public static bool IsUsername(string username)
        {
            // start with a letter, allow letter or number, length between 6 to 12.
            string userNamePattern = @"^[a-zA-Z][a-zA-Z0-9]{6,12}$";
            Regex regex = new Regex(userNamePattern);

            if (regex.IsMatch(username))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsValidPersData(PersonalData userData)
        {
            PersDataValidator persValidator = new PersDataValidator();

            if (persValidator.Validate(userData).IsValid)
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
