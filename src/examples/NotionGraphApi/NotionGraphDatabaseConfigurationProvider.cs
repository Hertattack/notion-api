using System.Globalization;
using Microsoft.Extensions.Options;
using IConfigurationProvider = NotionGraphDatabase.Interface.IConfigurationProvider;

namespace NotionGraphApi;

public class NotionGraphDatabaseConfigurationProvider : IConfigurationProvider
{
    public CultureInfo DateTimeConversionCulture { get; }

    public NotionGraphDatabaseConfigurationProvider(
        IOptions<NotionGraphApiOptions> options,
        ILogger<NotionGraphDatabaseConfigurationProvider> logger)
    {
        var apiOptions = options.Value;

        var cultureCode = string.IsNullOrEmpty(apiOptions.Culture) ? "en-US" : apiOptions.Culture;
        logger.LogInformation("Using culture code '{CultureCode}' for date-time conversion", cultureCode);
        DateTimeConversionCulture = new CultureInfo(cultureCode);
    }
}