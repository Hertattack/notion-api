using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using FluentRest.Commands.Builder;

namespace FluentRest.Commands
{
    internal class CommandBuilderFactory
    {
        private IDictionary<Type, Func<Version, ICommandBuilder>> _builders =
            new Dictionary<Type, Func<Version, ICommandBuilder>>();

        public CommandBuilderFactory()
        {
            var builders = GetBuilders(this.GetType().Assembly);

            foreach (var builder in builders)
                _builders.Add(GetBuilderSupportedType(builder), GetBuilderConstructor(builder));
        }

        public ICommandBuilder<T> GetCommandBuilder<T>(Version version) where T : ICommand
        {
            return _builders[typeof(T)].Invoke(version) as ICommandBuilder<T>;
        }

        private static Type GetBuilderSupportedType(Type builder)
        {
            var commandBuilderAttribute = builder.GetCustomAttribute<CommandBuilderAttribute>();

            Debug.Assert(commandBuilderAttribute != null, nameof(commandBuilderAttribute) + " != null");
            return commandBuilderAttribute.Type;
        }

        private static Func<Version, ICommandBuilder> GetBuilderConstructor(Type builder)
        {
            var versionType = typeof(Version);
            var constructor = builder.GetConstructor(new[] {versionType});

            if (constructor == null)
                throw new Exception($"{builder.FullName} does not implement a versioned constructor.");

            return (version) => constructor.Invoke(new object[] {version}) as ICommandBuilder;
        }

        static IEnumerable<Type> GetBuilders(Assembly assembly) =>
            assembly.GetTypes()
                .Where(type => type.GetCustomAttribute<CommandBuilderAttribute>() != null)
                .Where(type => type.IsAssignableFrom(typeof(ICommandBuilder)));
    }
}