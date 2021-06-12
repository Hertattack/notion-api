namespace NotionApi.Request.Mapping
{
    public class ToJsonDocumentStrategy : BaseMappingStrategy
    {
        public ToJsonDocumentStrategy(IMapper mapper) : base(mapper)
        {
        }

        public override object GetValue(object valueToMap)
        {
            throw new System.NotImplementedException();
        }
    }
}