using BookShelf.API.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class DbModule
{
    public static void AddDbModule(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<DatabaseConfig>(
            builder.Configuration.GetSection(DatabaseConfig.SectionName));

        var dbOptions = builder.Configuration
            .GetSection(DatabaseConfig.SectionName)
            .Get<DatabaseConfig>() ?? throw new InvalidOperationException("Database configuration is missing");

        //Connect database
    }
}