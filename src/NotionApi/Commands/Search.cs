using System.Collections.Generic;
using NotionApi.Commands.Builder;
using NotionApi.Commands.Search.BodyParameters;
using NotionApi.Util;

namespace NotionApi.Commands.Search
{
    public class Search
    {
        private static Dictionary<Version, ICommandBuilderFactory> supportedVersions =
            new Dictionary<Version, ICommandBuilderFactory>();

        private readonly Version _forVersion;

        public Search(Version forVersion)
        {
            _forVersion = forVersion;
        }

        static Search()
        {
            // Configure different supported versions.
            var builderFactory = new VersionedCommandBuilderFactory(Version.V20210513);
            builderFactory.Path = "/v1/search";
            builderFactory.Method = HttpMethod.GET;
            builderFactory.AddBodyParameter<string>("query", isRequired: true);
            builderFactory.AddBodyParameter<SearchSort>("sort", isRequired: false);
            builderFactory.AddBodyParameter<int>("start_cursor", isRequired: false);
            builderFactory.AddBodyParameter<int>("page_size", isRequired: false, p => Result.Conditional(p <= 100, "Maximum value is 100."));
        }
    }
}