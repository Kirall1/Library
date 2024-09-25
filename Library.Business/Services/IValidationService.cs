namespace Library.Business.Services
{
    public interface IValidationService
    {
        public Task ValidateAsync<T>(T model, CancellationToken cancellationToken);
    }
}