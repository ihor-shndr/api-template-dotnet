using BookShelf.API.Mappers.Books.Implementation;
using Books.Domain.Books.Models;

namespace Books.UnitTests.Books;

public class BookMapperTests
{
    private BookMapper _bookMapper;

    [SetUp]
    public void Setup()
    {
        _bookMapper = new BookMapper();
    }

    [Test]
    public void BookMapper_MapBook_MapsAllFields()
    {
        var book = new Book
        {
            Title = "Domain-Driven Design",
            Author = "Eric Evans",
            CreatedDate = new DateTime(2023, 1, 1)
        };

        var contract = _bookMapper.MapBook(book);

        Assert.Multiple(() =>
        {
            Assert.That(contract.Title, Is.EqualTo(book.Title));
            Assert.That(contract.Author, Is.EqualTo(book.Author));
        });
    }
}
