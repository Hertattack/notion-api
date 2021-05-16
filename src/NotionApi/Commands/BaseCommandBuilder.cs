using System.Collections.Generic;
using NotionApi.Commands.Builder;

namespace NotionApi.Commands
{
    public abstract class BaseCommandBuilder
    {
        private static Dictionary<Version, VersionedCommandBuilderFactory> supportedVersions =
            new Dictionary<Version, VersionedCommandBuilderFactory>();
        
        public Version Version { get; private set; }

        protected readonly VersionedCommandBuilderFactory _factory;
        
        protected BaseCommandBuilder(Version version)
        {
            Version = version;
            _factory = supportedVersions[version];
        }
    }
}