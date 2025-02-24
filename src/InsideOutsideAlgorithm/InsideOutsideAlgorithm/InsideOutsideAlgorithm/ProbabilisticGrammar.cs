using System;
using System.Collections.Generic;

namespace InsideOutsideAlgorithm
{
    public class ProbabilisticGrammar
    {
        public string StartSymbol { get; set; }
        public List<GrammarRule> Rules { get; set; }

        public ProbabilisticGrammar(string startSymbol)
        {
            StartSymbol = startSymbol;
            Rules = new List<GrammarRule>();
        }
    }
}