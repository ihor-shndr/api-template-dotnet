using Books.Data.Books.Entities;

namespace Books.Data.Books.DataAccess;

internal static class BookSeedData
{
    internal static readonly BookEntity[] Books =
    [
        new()
        {
            Id = 1,
            Title = "The Waste Land",
            Author = "T. S. Eliot",
            CreatedDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        },
        new()
        {
            Id = 2,
            Title = "The Count of Monte Cristo",
            Author = "Alexandre Dumas",
            CreatedDate = new DateTime(2025, 1, 2, 0, 0, 0, DateTimeKind.Utc)
        },
        new()
        {
            Id = 3,
            Title = "Pride and Prejudice",
            Author = "Jane Austen",
            CreatedDate = new DateTime(2025, 1, 3, 0, 0, 0, DateTimeKind.Utc)
        },
        new()
        {
            Id = 4,
            Title = "Moby-Dick",
            Author = "Herman Melville",
            CreatedDate = new DateTime(2025, 1, 4, 0, 0, 0, DateTimeKind.Utc)
        },
        new()
        {
            Id = 5,
            Title = "The Great Gatsby",
            Author = "F. Scott Fitzgerald",
            CreatedDate = new DateTime(2025, 1, 5, 0, 0, 0, DateTimeKind.Utc)
        },
        new()
        {
            Id = 6,
            Title = "Ivanhoe",
            Author = "Sir Walter Scott",
            CreatedDate = new DateTime(2025, 1, 6, 0, 0, 0, DateTimeKind.Utc)
        },
        new()
        {
            Id = 7,
            Title = "Les Misérables",
            Author = "Victor Hugo",
            CreatedDate = new DateTime(2025, 1, 7, 0, 0, 0, DateTimeKind.Utc)
        },
        new()
        {
            Id = 8,
            Title = "The Catcher in the Rye",
            Author = "J. D. Salinger",
            CreatedDate = new DateTime(2025, 1, 8, 0, 0, 0, DateTimeKind.Utc)
        },
        new()
        {
            Id = 9,
            Title = "1984",
            Author = "George Orwell",
            CreatedDate = new DateTime(2025, 1, 9, 0, 0, 0, DateTimeKind.Utc)
        },
        new()
        {
            Id = 10,
            Title = "Brave New World",
            Author = "Aldous Huxley",
            CreatedDate = new DateTime(2025, 1, 10, 0, 0, 0, DateTimeKind.Utc)
        },
        new()
        {
            Id = 11,
            Title = "To Kill a Mockingbird",
            Author = "Harper Lee",
            CreatedDate = new DateTime(2025, 1, 11, 0, 0, 0, DateTimeKind.Utc)
        },
        new()
        {
            Id = 12,
            Title = "The Hobbit",
            Author = "J. R. R. Tolkien",
            CreatedDate = new DateTime(2025, 1, 12, 0, 0, 0, DateTimeKind.Utc)
        },
        new()
        {
            Id = 13,
            Title = "The Lord of the Rings",
            Author = "J. R. R. Tolkien",
            CreatedDate = new DateTime(2025, 1, 13, 0, 0, 0, DateTimeKind.Utc)
        },
        new()
        {
            Id = 14,
            Title = "Jane Eyre",
            Author = "Charlotte Bronte",
            CreatedDate = new DateTime(2025, 1, 14, 0, 0, 0, DateTimeKind.Utc)
        },
        new()
        {
            Id = 15,
            Title = "Wuthering Heights",
            Author = "Emily Bronte",
            CreatedDate = new DateTime(2025, 1, 15, 0, 0, 0, DateTimeKind.Utc)
        },
        new()
        {
            Id = 16,
            Title = "The Picture of Dorian Gray",
            Author = "Oscar Wilde",
            CreatedDate = new DateTime(2025, 1, 16, 0, 0, 0, DateTimeKind.Utc)
        },
        new()
        {
            Id = 17,
            Title = "One Hundred Years of Solitude",
            Author = "Gabriel Garcia Marquez",
            CreatedDate = new DateTime(2025, 1, 17, 0, 0, 0, DateTimeKind.Utc)
        },
        new()
        {
            Id = 18,
            Title = "The Name of the Rose",
            Author = "Umberto Eco",
            CreatedDate = new DateTime(2025, 1, 18, 0, 0, 0, DateTimeKind.Utc)
        },
        new()
        {
            Id = 19,
            Title = "The Alchemist",
            Author = "Paulo Coelho",
            CreatedDate = new DateTime(2025, 1, 19, 0, 0, 0, DateTimeKind.Utc)
        },
        new()
        {
            Id = 20,
            Title = "Frankenstein",
            Author = "Mary Shelley",
            CreatedDate = new DateTime(2025, 1, 20, 0, 0, 0, DateTimeKind.Utc)
        }
    ];
}
