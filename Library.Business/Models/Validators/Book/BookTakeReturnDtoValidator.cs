using FluentValidation;
using Library.Business.Models.Book;

namespace Library.Business.Models.Validators.Book
{
    public class BookTakeReturnDtoValidator : AbstractValidator<BookTakeReturnDto>, IValidationMarker
    {
        public BookTakeReturnDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be specified.")
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("User Id must be specified.")
                .NotEmpty().WithMessage("User is required.");
        }
    }
}