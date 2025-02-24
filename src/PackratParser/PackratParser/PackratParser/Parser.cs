namespace PackratParser
{
    public static class Parser
    {
        public static Result Parse(Expression expression, string input)
        {
            if (expression == null)
            {
                return new Result(false, 0, null);
            }

            if (input == null)
            {
                input = string.Empty;
            }

            ParserContext context = new ParserContext(input);
            Result result = expression.Parse(context, 0);
            return result;
        }
    }
}