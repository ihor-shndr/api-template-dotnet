namespace Books.API.Contracts.Books;

public record BookContract(
    string Title,
    string Author,
    DateTime CreatedDate
);
