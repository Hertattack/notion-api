namespace FluentRest.Util
{
    public class Failure : Result
    {
        public string Message { get; }

        public Failure(string message)
        {
            Message = message;
        }

        public override bool IsFailure => true;
    }
}