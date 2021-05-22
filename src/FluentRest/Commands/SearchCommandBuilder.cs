using System;
using FluentRest.Commands.Builder;

namespace FluentRest.Commands
{
    [CommandBuilder(typeof(Search))]
    public class SearchCommandBuilder : BaseCommandBuilder, ICommandBuilder<Search>
    {
        public SearchCommandBuilder(Version version) : base(version)
        {
        }

        static SearchCommandBuilder()
        {
            /*
            // Configure different supported versions.
            var builderFactory = new VersionedCommandBuilderFactory(Version.V20210513);
            builderFactory.Path = "/v1/search";
            builderFactory.Method = HttpMethod.GET;
            builderFactory.AddBodyParameter<string>("query", isRequired: false);
            builderFactory.AddBodyParameter<SearchSort>("sort", isRequired: false);
            builderFactory.AddBodyParameter<int>("start_cursor", isRequired: false);
            builderFactory.AddBodyParameter<int>("page_size", isRequired: false,
                p => Result.Conditional(p <= 100, "Maximum value is 100."));
            */
        }
    }
}