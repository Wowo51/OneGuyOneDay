namespace PackratParser.Expressions
{
    using System.Collections.Generic;
    using PackratParser;

    public class ZeroOrMoreExpression : Expression
    {
        public Expression SubExpression { get; }

        public ZeroOrMoreExpression(Expression subExpression)
        {
            SubExpression = subExpression;
        }

        public override Result Parse(ParserContext context, int position)
        {
            return context.Memoize(this, position, () =>
            {
                int currentPosition = position;
                List<object> values = new List<object>();
                while (true)
                {
                    Result result = SubExpression.Parse(context, currentPosition);
                    if (!result.Success || result.Position == currentPosition)
                    {
                        break;
                    }

                    values.Add(result.Value);
                    currentPosition = result.Position;
                }

                return new Result(true, currentPosition, values);
            });
        }
    }
}