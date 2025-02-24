namespace PackratParser.Expressions
{
    using PackratParser;

    public class LiteralExpression : Expression
    {
        public string Literal { get; }

        public LiteralExpression(string literal)
        {
            Literal = literal;
        }

        public override Result Parse(ParserContext context, int position)
        {
            return context.Memoize(this, position, () =>
            {
                if (position + Literal.Length > context.Input.Length)
                {
                    return new Result(false, position, null);
                }

                string substring = context.Input.Substring(position, Literal.Length);
                if (substring == Literal)
                {
                    return new Result(true, position + Literal.Length, Literal);
                }

                return new Result(false, position, null);
            });
        }
    }
}