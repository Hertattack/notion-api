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

    public class bla
    {
        public bla(string input)
        {
            
        }

        public IOtherElement Other
        {
            get
            {
                // spec.SetContext(this, "Other", "Get")

                return null;
            }
        }
    }
    

    public interface ITest
    {
        //public IOtherElement Other { get; set; }
    }

    public interface IOtherElement
    {
    }
}