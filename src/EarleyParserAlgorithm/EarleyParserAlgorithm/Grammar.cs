using System.Collections.Generic;

namespace EarleyParserAlgorithm
{
    public class Grammar
    {
        public string StartSymbol { get; set; }
        public List<Rule> Rules { get; set; }

        public Grammar(string startSymbol)
        {
            this.StartSymbol = startSymbol;
            this.Rules = new List<Rule>();
        }

        public void AddRule(Rule rule)
        {
            this.Rules.Add(rule);
        }

        public List<Rule> GetRulesForNonterminal(string nonterminal)
        {
            List<Rule> result = new List<Rule>();
            foreach (Rule rule in this.Rules)
            {
                if (rule.LeftHandSide == nonterminal)
                {
                    result.Add(rule);
                }
            }

            return result;
        }
    }
}