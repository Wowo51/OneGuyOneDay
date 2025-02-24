using System.Collections.Generic;

namespace LRParser.Parser
{
    public static class ParserFactory
    {
        public static ILRParser CreateParser(string parserType)
        {
            if (parserType == "CanonicalLR")
            {
                return new CanonicalLRParser();
            }
            else if (parserType == "LALR")
            {
                return new LALRParser();
            }
            else if (parserType == "SLR")
            {
                return new SLRParser();
            }
            else if (parserType == "OperatorPrecedence")
            {
                return new OperatorPrecedenceParser();
            }
            else if (parserType == "SimplePrecedence")
            {
                return new SimplePrecedenceParser();
            }
            else
            {
                return new SLRParser();
            }
        }
    }
}