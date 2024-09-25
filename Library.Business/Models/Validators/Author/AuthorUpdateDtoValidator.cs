using FluentValidation;
using Library.Business.Models.Author;

namespace Library.Business.Models.Validators.Author
{
    public class AuthorUpdateDtoValidator : AbstractValidator<AuthorUpdateDto>, IValidationMarker
    {
        public AuthorUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be specified.")
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name cannot be empty.")
                .MaximumLength(AuthorValidatorConfiguration.NameMaxLength)
                .WithMessage($"Name cannot exceed {AuthorValidatorConfiguration.NameMaxLength} characters.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Surname cannot be empty.")
                .MaximumLength(AuthorValidatorConfiguration.SurnameMaxLength)
                .WithMessage($"Surname cannot exceed {AuthorValidatorConfiguration.SurnameMaxLength} characters.");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("BirthDate cannot be empty.")
                .LessThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage("BirthDate must be in the past.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country cannot be empty.")
                .MaximumLength(AuthorValidatorConfiguration.CountryMaxLength)
                .WithMessage($"Country name cannot exceed {AuthorValidatorConfiguration.CountryMaxLength} characters.");
        }
    }
}
