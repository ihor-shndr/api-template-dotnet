using BookShelf.API.Contracts.Books;
using Books.Domain.Books.Models;
namespace BookShelf.API.Mappers.Books;

public interface IBookMapper
{
    BookContract MapBook(Book book);
}