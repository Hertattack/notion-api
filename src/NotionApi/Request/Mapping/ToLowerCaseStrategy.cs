namespace NotionApi.Request.Mapping
{
    public class ToLowerCaseStrategy : BaseMappingStrategy
    {
        public ToLowerCaseStrategy(IMapper mapper) : base(mapper)
        {
        }

        public override object GetValue(object propertyValue)
        {
            if (propertyValue == null)
                return "";

            return propertyValue.ToString()?.ToLower() ?? "";
        }
    }
}