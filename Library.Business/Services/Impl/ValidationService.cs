using FluentValidation;

namespace Library.Business.Services.Impl
{
    public class ValidationService : IValidationService
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task ValidateAsync<T>(T model, CancellationToken cancellationToken)
        {
            var validator = (IValidator<T>)_serviceProvider.GetService(typeof(IValidator<T>));

            if (validator != null)
            {
                var validationResult = await validator.ValidateAsync(model, cancellationToken);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }
            }
        }
    }
}