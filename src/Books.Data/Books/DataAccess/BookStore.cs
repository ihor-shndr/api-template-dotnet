using System.Collections.Concurrent;
using Books.Data.Books.Entities;

namespace Books.Data.Books.DataAccess;

// Process-wide so deletes persist across the transient DAO instances; a restart reloads the seed.
internal static class BookStore
{
    internal static readonly ConcurrentDictionary<int, BookEntity> Books =
        new(BookSeedData.Books.Select(b => KeyValuePair.Create(b.Id, b)));
}
