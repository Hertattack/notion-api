using System;
using FluentRest.DynamicTyping;
using NUnit.Framework;

namespace FluentRest.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var dtb = new DynamicTypeBuilder();
            Console.WriteLine(dtb.RoslynBuildDefinition<ITest>());
        }
    }

    public interface ITest
    {
        public IOtherElement Other { get; set; }
    }

    public interface IOtherElement
    {
    }
}