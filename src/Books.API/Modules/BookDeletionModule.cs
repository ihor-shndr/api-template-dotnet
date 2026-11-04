using Books.API.Configuration;
using Books.API.Filters;

namespace Microsoft.Extensions.DependencyInjection;

public static class BookDeletionModule
{
    public static void AddBookDeletionModule(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<BookDeletionConfig>(
            builder.Configuration.GetSection(BookDeletionConfig.SectionName));
        builder.Services.AddTransient<BookDeletionGuardFilter>();
    }
}
