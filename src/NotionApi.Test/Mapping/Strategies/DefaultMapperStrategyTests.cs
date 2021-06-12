using NotionApi.Request;
using NotionApi.Request.Mapping;
using NSubstitute;
using NUnit.Framework;

namespace NotionApi.Test.Mapping.Strategies
{
    [TestFixture]
    public class DefaultMapperStrategyTests
    {
        protected IMapper mapper;
        protected DefaultMappingStrategy defaultMappingStrategy;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            mapper = Substitute.For<IMapper>();
            defaultMappingStrategy = new DefaultMappingStrategy(mapper);
        }
    }
}