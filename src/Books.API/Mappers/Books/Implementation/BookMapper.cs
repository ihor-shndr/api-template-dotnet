using Books.Domain.Books.Models;
using BookShelf.API.Contracts.Books;

namespace BookShelf.API.Mappers.Books.Implementation;

public class BookMapper : IBookMapper
{
    public BookContract MapBook(Book book) =>
        new()
        {
            Title = book.Title,
            Author = book.Author
        };
}