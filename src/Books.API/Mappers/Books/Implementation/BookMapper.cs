using Books.Domain.Books.Models;
using Books.API.Contracts.Books;

namespace Books.API.Mappers.Books.Implementation;

public class BookMapper : IBookMapper
{
    public BookContract MapBook(Book book) =>
        new(book.Title, book.Author, book.CreatedDate);
}