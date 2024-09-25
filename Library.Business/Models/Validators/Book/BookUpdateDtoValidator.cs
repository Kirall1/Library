using FluentValidation;
using Library.Business.Models.Book;

namespace Library.Business.Models.Validators.Book
{
    public class BookUpdateDtoValidator : AbstractValidator<BookUpdateDto>, IValidationMarker
    {
        public BookUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be specified.")
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.Isbn)
                .NotEmpty().WithMessage("ISBN cannot be empty.")
                .Length(BookValidatorConfiguration.IsbnMinLength, BookValidatorConfiguration.IsbnMaxLength)
                .WithMessage($"ISBN must be between {BookValidatorConfiguration.IsbnMinLength} and " +
                    $"{BookValidatorConfiguration.IsbnMaxLength} characters long.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title cannot be empty.")
                .MaximumLength(BookValidatorConfiguration.TitleMaxLength)
                .WithMessage($"Title cannot exceed {BookValidatorConfiguration.TitleMaxLength} characters.");

            RuleFor(x => x.Genre)
                .NotEmpty().WithMessage("Genre must be selected.");

            RuleFor(x => x.Description)
                .MaximumLength(BookValidatorConfiguration.DescriptionMaxLength)
                .WithMessage($"Description cannot exceed {BookValidatorConfiguration.DescriptionMaxLength} characters.");

            RuleFor(x => x.AuthorId)
                .GreaterThan(0).WithMessage("Author must be selected.");

            RuleFor(x => x.ImageFile)
                .Must(file => file == null || file.Length <= BookValidatorConfiguration.MaxFileSize)
                .WithMessage($"Image file size cannot exceed {BookValidatorConfiguration.MaxFileSize / (1024 * 1024)} MB.");
        }
    }
}