using Books.Domain.Books.Models;
using BookShelf.API.Mappers.Books.Implementation;

namespace Books.UnitTests.Books;

public class BookMapperTests
{
    private readonly BookMapper _bookMapper;

    public BookMapperTests()
    {
        _bookMapper = new BookMapper();
    }

    [Fact]
    public void BookMapper_MapBook_MapsAllFields()
    {
        var book = new Book
        {
            Title = "Domain-Driven Design",
            Author = "Eric Evans",
            CreatedDate = new DateTime(2023, 1, 1)
        };

        var contract = _bookMapper.MapBook(book);

        Assert.Equal(book.Title, contract.Title);
        Assert.Equal(book.Author, contract.Author);
        Assert.Equal(book.CreatedDate, contract.CreatedDate);
    }
}
