using Books.Common.TryResult;
using Books.Domain.Books.DataAccess;
using Books.Domain.Books.Models;

namespace Books.Data.Books.DataAccess;

public class BookDao : IBookDao
{
    public async Task<TryResult<Book>> GetBookAsync(int id)
    {
        var book = BookSeedData.Books.FirstOrDefault(b => b.Id == id);

        if (book is null)
            return new Error(BookErrorCodes.BookNotFound, $"Book with ID {id} not found");

        return await Task.FromResult(new Book
        {
            Title = book.Title,
            Author = book.Author,
            CreatedDate = book.CreatedDate
        });
    }

    public Task<TryResult<IReadOnlyList<Book>>> GetBooksAsync()
    {
        IReadOnlyList<Book> books = BookSeedData.Books
            .Select(b => new Book
            {
                Title = b.Title,
                Author = b.Author,
                CreatedDate = b.CreatedDate
            })
            .ToList();

        return Task.FromResult(TryResult.Success(books));
    }
}
