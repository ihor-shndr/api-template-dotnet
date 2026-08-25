using Books.Data.Books.DataAccess;
using Books.Domain.Books.DataAccess;
using Books.Domain.Books.Services;
using Books.Domain.Books.Services.Implementation;
using Books.API.Mappers.Books;
using Books.API.Mappers.Books.Implementation;

namespace Microsoft.Extensions.DependencyInjection;

public static class BooksModule
{
    public static void AddBooksModule(this IServiceCollection services)
    {
        services.AddTransient<IBookDao, BookDao>();
        services.AddTransient<IBookService, BookService>();
        services.AddTransient<IBookMapper, BookMapper>();
    }
}