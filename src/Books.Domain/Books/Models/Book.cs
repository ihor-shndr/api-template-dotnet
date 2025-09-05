namespace Books.Domain.Books.Models;

public class Book
{
    public required string Title { get; init; }
    public required string Author { get; init; }
    public DateTime CreatedDate { get; set; }
}