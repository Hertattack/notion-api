namespace FluentRest.Util
{
    public class Success : Result
    {
        public override bool IsFailure => false;
    }
}