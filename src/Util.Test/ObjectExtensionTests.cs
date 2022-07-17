using System;
using FluentAssertions;
using NUnit.Framework;
using Util.Extensions;

namespace Util.Test;

[TestFixture]
public class ObjectExtensionTests
{
    public class ThrowIfNullTests : ObjectExtensionTests
    {
        [Test]
        public void When_a_variable_is_null_it_throws()
        {
            // Arrange
            string x = null;
            
            // Act + Assert
            var thrownException = Try<ArgumentNullException>(() => x.ThrowIfNull());

            // Assert
            thrownException.HasValue.Should().BeTrue();
        }
    }
    
    protected Option<TExpectedException> Try<TExpectedException>(Action action) where TExpectedException : Exception
    {
        try
        {
            action();
        }
        catch (TExpectedException expectedException)
        {
            return expectedException;
        }

        return Option.None;
    }
}