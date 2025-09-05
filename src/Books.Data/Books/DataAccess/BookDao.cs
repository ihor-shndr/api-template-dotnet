using Books.Common.TryResult;
using Books.Data.Books.Entities;
using Books.Domain.Books.DataAccess;
using Books.Domain.Books.Models;

namespace Books.Data.Books.DataAccess;

public class BookDao : IBookDao
{
    private static readonly BookEntity[] Books =
    [
        new()
        {
            Id = 1,
            Title = "Don Quixote",
            Author = "Miguel de Cervantes",
            CreatedDate = DateTime.Now
        }
    ];

    public async Task<TryResult<Book>> GetBookAsync(int id)
    {
        var book = Books.FirstOrDefault(b => b.Id == id);

        if (book is null)
            return new Error(BookErrorCodes.BookNotFound, $"Book with ID {id} not found");

        return await Task.FromResult(new Book
        {
            Title = book.Title,
            Author = book.Author,
            CreatedDate = book.CreatedDate
        });
    }
}