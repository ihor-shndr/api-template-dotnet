using Books.Common.TryResult;
using Books.Domain.Books.DataAccess;
using Books.Domain.Books.Models;
using Microsoft.Extensions.Logging;

namespace Books.Domain.Books.Services.Implementation;

public class BookService(ILogger<BookService> logger, IBookDao bookDao) : IBookService
{
    public async Task<TryResult<Book>> GetBookAsync(int id)
    {
        var result = await bookDao.GetBookAsync(id);

        if(!result.IsSuccess)
        {
            logger.LogError(result.Error.Message);

            return result.Error;
        }

        return result.Value;
    }
}