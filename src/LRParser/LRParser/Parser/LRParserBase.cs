using System.Collections.Generic;

namespace LRParser.Parser
{
    public abstract class LRParserBase : ILRParser
    {
        public ParseTree Parse(List<Token> tokens)
        {
            if (tokens == null)
            {
                tokens = new List<Token>();
            }

            return this.DoParse(tokens);
        }

        protected abstract ParseTree DoParse(List<Token> tokens);
    }
}