using Books.API.Contracts.Books;
using Books.Domain.Books.Models;
namespace Books.API.Mappers.Books;

public interface IBookMapper
{
    BookContract MapBook(Book book);
}