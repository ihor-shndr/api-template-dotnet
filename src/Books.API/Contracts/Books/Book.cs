namespace BookShelf.API.Contracts.Books;

public class BookContract
{
    public required string Title { get; init; }
    public required string Author { get; init; }
}