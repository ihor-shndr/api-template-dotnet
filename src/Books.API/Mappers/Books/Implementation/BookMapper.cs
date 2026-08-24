using Books.Domain.Books.Models;
using BookShelf.API.Contracts.Books;

namespace BookShelf.API.Mappers.Books.Implementation;

public class BookMapper : IBookMapper
{
    public BookContract MapBook(Book book) =>
        new(book.Title, book.Author, book.CreatedDate);
}