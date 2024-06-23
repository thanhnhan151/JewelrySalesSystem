using FluentValidation;
using JewelrySalesSystem.BAL.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelrySalesSystem.BAL.Validators.User
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserValidator()
        {
            RuleFor(user => user.UserName)
                .NotEmpty()
                .WithMessage("User name is required.")
                .Matches("^[a-zA-Z0-9]+$")
                .Length(2, 50);

            RuleFor(user => user.FullName)
                .NotEmpty().Length(2, 50)
                .Matches("^[a-zA-Z ]+$")
                .WithMessage("Full name cannot contain special characters.");

            RuleFor(user => user.PhoneNumber).NotEmpty()
                .Length(10, 10)
                .Matches(@"\d{10}$")
                .WithMessage("Phone number must be a valid 10-digit number.");

            RuleFor(user => user.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Email must be a valid email address."); ;

            RuleFor(user => user.Password)
                .NotEmpty();

            RuleFor(user => user.Address)
                .Matches("^[a-zA-Z0-9/ ]+$")
                .WithMessage("Address cannot contain special characters.");

            RuleFor(user => user.RoleId)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Role ID must be greater than 0.");

        }

    }
}
