namespace PackratParser
{
    public class Result
    {
        public bool Success { get; }
        public int Position { get; }
        public object? Value { get; }

        public Result(bool success, int position, object? value)
        {
            Success = success;
            Position = position;
            Value = value;
        }

        public static Result Failure { get; } = new Result(false, 0, null);
    }
}