using FluentValidation;
using Library.Business.Models.User;
using Microsoft.AspNetCore.Identity;
using Library.DataAccess.Entities;

namespace Library.Business.Models.Validators.User
{
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>, IValidationMarker
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserCreateDtoValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username cannot be empty.")
                .MaximumLength(UserValidatorConfiguration.UserNameMaxLength)
                .WithMessage($"Username cannot exceed {UserValidatorConfiguration.UserNameMaxLength} characters.")
                .Must(UsernameIsUnique)
                .WithMessage("Username is not available");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email cannot be empty.")
                .EmailAddress().WithMessage("Email must be a valid email address.")
                .Must(EmailAddressIsUnique)
                .WithMessage("Email address is already in use");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password cannot be empty.")
                .MinimumLength(UserValidatorConfiguration.PasswordMinLength)
                .WithMessage($"Password must be at least {UserValidatorConfiguration.PasswordMinLength} characters long.")
                .MaximumLength(UserValidatorConfiguration.PasswordMaxLength)
                .WithMessage($"Password cannot exceed {UserValidatorConfiguration.PasswordMaxLength} characters.");
        }

        private bool EmailAddressIsUnique(string email)
        {
            var user = _userManager.FindByEmailAsync(email).GetAwaiter().GetResult();

            return user == null;
        }

        private bool UsernameIsUnique(string username)
        {
            var user = _userManager.FindByNameAsync(username).GetAwaiter().GetResult();

            return user == null;
        }
    }
}