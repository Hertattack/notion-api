namespace FluentRest
{
    public interface IBodySpecification
    {
    }

    public interface IBodySpecification<TBodyType> : IBodySpecification
    {
        T PropertySpecification<T>(string propertyName);
    }
}