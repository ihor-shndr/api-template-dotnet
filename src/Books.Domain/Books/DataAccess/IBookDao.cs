using Books.Common.TryResult;
using Books.Domain.Books.Models;

namespace Books.Domain.Books.DataAccess;

public interface IBookDao
{
    Task<TryResult<Book>> GetBookAsync(int id);
}