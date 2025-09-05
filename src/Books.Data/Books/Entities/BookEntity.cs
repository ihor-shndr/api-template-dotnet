namespace Books.Data.Books.Entities;

public class BookEntity
{
    public required int Id { get; init; }
    public required string Title { get; init; }
    public required string Author { get; init; }
    public required DateTime CreatedDate { get; init; }
}