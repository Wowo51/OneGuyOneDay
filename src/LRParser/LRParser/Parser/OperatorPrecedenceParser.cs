using System.Collections.Generic;

namespace LRParser.Parser
{
    public class OperatorPrecedenceParser : LRParserBase
    {
        protected override ParseTree DoParse(List<Token> tokens)
        {
            ParseTree root = new ParseTree("OperatorPrecedence");
            foreach (Token token in tokens)
            {
                root.Children.Add(new ParseTree(token.Value));
            }

            return root;
        }
    }
}