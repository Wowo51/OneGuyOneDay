namespace PackratParser
{
    public abstract class Expression
    {
        public abstract Result Parse(ParserContext context, int position);
    }
}