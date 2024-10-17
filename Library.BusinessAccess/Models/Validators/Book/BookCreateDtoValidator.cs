using FluentValidation;
using Library.BusinessAccess.Models.Book;

namespace Library.BusinessAccess.Models.Validators.Book
{
    public class BookCreateDtoValidator : AbstractValidator<BookCreateDto>, IValidationMarker
    {
        public BookCreateDtoValidator()
        {
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
