using Library.BusinessAccess.Services;
using Library.BusinessAccess.Services.Impl;
using Library.BusinessAccess.UseCases.Authors;
using Library.BusinessAccess.UseCases.Authors.Impl;
using Library.BusinessAccess.UseCases.Books;
using Library.BusinessAccess.UseCases.Books.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace Library.BusinessAccess;

public static class BALDependecyInjection
{
    public static IServiceCollection AddBALDependecyInjections(this IServiceCollection services)
    {
        services.AddTransient<IFileService, FileService>();
        services.AddTransient<IBookService, BookService>();
        services.AddTransient<IAuthorService, AuthorService>();
        services.AddTransient<ITokenService, TokenService>();
        services.AddTransient<IUserService, UserService>();
        
        
        services.AddTransient<IAddBookUseCase, AddBookUseCase>();
        services.AddTransient<IDeleteBookUseCase, DeleteBookUseCase>();
        services.AddTransient<IEditBookUseCase, EditBookUseCase>();
        services.AddTransient<IGetBookByIdUseCase, GetBookByIdUseCase>();
        services.AddTransient<IGetBookByIsbnUseCase, GetBookByIsbnUseCase>();
        services.AddTransient<IGetBooksByAuthorUseCase, GetBooksByAuthorUseCase>();
        services.AddTransient<IGetBooksByPageUseCase, GetBooksByPageUseCase>();
        services.AddTransient<IGetBooksUseCase, GetBooksUseCase>();
        services.AddTransient<IReturnBookUseCase, ReturnBookUseCase>();
        services.AddTransient<ITakeBookUseCase, TakeBookUseCase>();
        
        
        services.AddTransient<ICreateAuthorUseCase, CreateAuthorUseCase>();
        services.AddTransient<IDeleteAuthorUseCase, DeleteAuthorUseCase>();
        services.AddTransient<IGetAuthorByIdUseCase, GetAuthorByIdUseCase>();
        services.AddTransient<IGetAuthorsByPageUseCase, GetAuthorsByPageUseCase>();
        services.AddTransient<IGetAuthorsUseCase, GetAuthorsUseCase>();
        services.AddTransient<IUpdateAuthorUseCase, UpdateAuthorUseCase>();
        
        return services; 
    }
}
