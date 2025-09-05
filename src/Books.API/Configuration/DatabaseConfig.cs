namespace BookShelf.API.Configuration;

public class DatabaseConfig
{
    public const string SectionName = "Database";

    public required string ConnectionString { get; init; }
}