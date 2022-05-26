namespace NotionGraphDatabase.QueryEngine.Model;

internal class SelectPathExpression : SelectExpression
{
    public SelectExpression FromExpression { get; }
    public Identifier ViaRole { get; }
    public SelectExpression ToExpression { get; }

    public SelectPathExpression(
        SelectExpression fromExpression,
        Identifier viaRole,
        SelectExpression toExpression)
    {
        FromExpression = fromExpression;
        ViaRole = viaRole;
        ToExpression = toExpression;
    }
}