namespace FluentRest.ApiBuilder
{
    public interface IPathOperationBuilder
    {
        TBodyType AddBodySpecification<TBodyType>();
    }
}