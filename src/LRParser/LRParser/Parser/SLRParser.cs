using System.Collections.Generic;

namespace LRParser.Parser
{
    public class SLRParser : LRParserBase
    {
        protected override ParseTree DoParse(List<Token> tokens)
        {
            ParseTree root = new ParseTree("SLR");
            foreach (Token token in tokens)
            {
                root.Children.Add(new ParseTree(token.Value));
            }

            return root;
        }
    }
}