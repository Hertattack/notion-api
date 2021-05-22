namespace FluentRest.ApiBuilder
{
    public class BodySpecification<TContainer> : IBodySpecification<TContainer>
    {
        private readonly PathOperationBuilder _pathOperationBuilder;

        public BodySpecification(PathOperationBuilder pathOperationBuilder)
        {
            _pathOperationBuilder = pathOperationBuilder;
        }

        public TPropertyType PropertySpecification<TPropertyType>(string propertyName)
        {
            return default;
        }
    }
}