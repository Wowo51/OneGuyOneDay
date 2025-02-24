namespace PackratParser.Expressions
{
    using System.Collections.Generic;
    using PackratParser;

    public class ChoiceExpression : Expression
    {
        public List<Expression> Alternatives { get; }

        public ChoiceExpression(List<Expression> alternatives)
        {
            Alternatives = alternatives;
        }

        public override Result Parse(ParserContext context, int position)
        {
            return context.Memoize(this, position, () =>
            {
                foreach (Expression alternative in Alternatives)
                {
                    Result result = alternative.Parse(context, position);
                    if (result.Success)
                    {
                        return result;
                    }
                }

                return new Result(false, position, null);
            });
        }
    }
}