using System.Globalization;

namespace NotionGraphDatabase.Interface;

public interface IConfigurationProvider
{
    CultureInfo DateTimeConversionCulture { get; }
}