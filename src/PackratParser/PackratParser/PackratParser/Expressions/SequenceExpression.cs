namespace PackratParser.Expressions
{
    using System.Collections.Generic;
    using PackratParser;

    public class SequenceExpression : Expression
    {
        public List<Expression> Expressions { get; }

        public SequenceExpression(List<Expression> expressions)
        {
            Expressions = expressions;
        }

        public override Result Parse(ParserContext context, int position)
        {
            return context.Memoize(this, position, () =>
            {
                int currentPosition = position;
                List<object> values = new List<object>();
                foreach (Expression expr in Expressions)
                {
                    Result result = expr.Parse(context, currentPosition);
                    if (!result.Success)
                    {
                        return new Result(false, position, null);
                    }

                    values.Add(result.Value);
                    currentPosition = result.Position;
                }

                return new Result(true, currentPosition, values);
            });
        }
    }
}