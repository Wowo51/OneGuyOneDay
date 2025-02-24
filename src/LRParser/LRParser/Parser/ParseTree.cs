using System.Collections.Generic;

namespace LRParser.Parser
{
    public class ParseTree
    {
        public string Node { get; set; }
        public List<ParseTree> Children { get; set; }

        public ParseTree(string node)
        {
            Node = node;
            Children = new List<ParseTree>();
        }
    }
}