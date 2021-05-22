using System;
using System.Collections.Generic;
using FluentRest.Commands.Builder;

namespace FluentRest.Commands
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