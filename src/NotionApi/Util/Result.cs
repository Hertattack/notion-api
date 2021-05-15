namespace NotionApi.Util
{
    public abstract class Result
    {
        public abstract bool IsFailure { get; }

        public static Result Conditional(bool b, string message)
        {
            return b ? (Result) new Success() : new Failure(message);
        }
    }
}