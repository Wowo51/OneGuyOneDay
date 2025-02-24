using System.Collections.Generic;

namespace LRParser.Parser
{
    public interface ILRParser
    {
        ParseTree Parse(List<Token> tokens);
    }
}