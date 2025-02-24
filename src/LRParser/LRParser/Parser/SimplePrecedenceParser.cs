using System.Collections.Generic;

namespace LRParser.Parser
{
    public class SimplePrecedenceParser : LRParserBase
    {
        protected override ParseTree DoParse(List<Token> tokens)
        {
            ParseTree root = new ParseTree("SimplePrecedence");
            foreach (Token token in tokens)
            {
                root.Children.Add(new ParseTree(token.Value));
            }

            return root;
        }
    }
}