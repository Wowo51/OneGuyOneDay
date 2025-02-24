using System;
using System.Collections.Generic;

namespace CykAlgorithm
{
    public class Grammar
    {
        public string StartSymbol { get; set; }
        public List<Production> Productions { get; set; }

        public Grammar(string startSymbol, List<Production> productions)
        {
            StartSymbol = startSymbol ?? "";
            Productions = productions ?? new List<Production>();
        }
    }

    public class Production
    {
        public string Left { get; set; }
        public List<string> Right { get; set; }

        public Production(string left, List<string> right)
        {
            Left = left ?? "";
            Right = right ?? new List<string>();
        }
    }
}