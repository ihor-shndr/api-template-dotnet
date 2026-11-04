using Books.Common.TryResult;
using Books.Domain.Books.DataAccess;
using Books.Domain.Books.Models;

namespace Books.Data.Books.DataAccess;

public class BookDao : IBookDao
{
    public async Task<TryResult<Book>> GetBookAsync(int id)
    {
        if (!BookStore.Books.TryGetValue(id, out var book))
            return new Error(BookErrorCodes.BookNotFound, $"Book with ID {id} not found");

        return await Task.FromResult(new Book
        {
            Title = book.Title,
            Author = book.Author,
            CreatedDate = book.CreatedDate
        });
    }

    public Task<TryResult> DeleteBookAsync(int id)
    {
        TryResult result = BookStore.Books.TryRemove(id, out _)
            ? TryResult.Success()
            : new Error(BookErrorCodes.BookNotFound, $"Book with ID {id} not found");

        return Task.FromResult(result);
    }
}
