using System.Collections.Generic;

namespace EarleyParserAlgorithm
{
    public class EarleyParser
    {
        public Grammar Grammar { get; private set; }

        public EarleyParser(Grammar grammar)
        {
            this.Grammar = grammar;
        }

        public bool Parse(IList<string> tokens)
        {
            int n = tokens.Count;
            List<HashSet<EarleyState>> chart = new List<HashSet<EarleyState>>();
            for (int i = 0; i <= n; i++)
            {
                chart.Add(new HashSet<EarleyState>());
            }

            Rule startRule = new Rule("γ", new string[] { this.Grammar.StartSymbol });
            EarleyState startState = new EarleyState(startRule, 0, 0);
            chart[0].Add(startState);
            for (int i = 0; i <= n; i++)
            {
                bool added = true;
                while (added)
                {
                    added = false;
                    List<EarleyState> statesAtI = new List<EarleyState>(chart[i]);
                    foreach (EarleyState state in statesAtI)
                    {
                        if (!state.IsComplete)
                        {
                            string? nextSym = state.NextSymbol;
                            if (nextSym != null && IsNonTerminal(nextSym))
                            {
                                List<Rule> rules = this.Grammar.GetRulesForNonterminal(nextSym);
                                foreach (Rule rule in rules)
                                {
                                    EarleyState newState = new EarleyState(rule, 0, i);
                                    if (!chart[i].Contains(newState))
                                    {
                                        chart[i].Add(newState);
                                        added = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            int origin = state.Origin;
                            List<EarleyState> statesAtOrigin = new List<EarleyState>(chart[origin]);
                            foreach (EarleyState prevState in statesAtOrigin)
                            {
                                string? prevNext = prevState.NextSymbol;
                                if (!prevState.IsComplete && prevNext != null && prevNext == state.Rule.LeftHandSide)
                                {
                                    EarleyState newState = new EarleyState(prevState.Rule, prevState.Dot + 1, prevState.Origin);
                                    if (!chart[i].Contains(newState))
                                    {
                                        chart[i].Add(newState);
                                        added = true;
                                    }
                                }
                            }
                        }
                    }
                }

                if (i < n)
                {
                    foreach (EarleyState state in chart[i])
                    {
                        if (!state.IsComplete)
                        {
                            string? nextSym = state.NextSymbol;
                            if (nextSym != null && !IsNonTerminal(nextSym) && tokens[i] == nextSym)
                            {
                                EarleyState newState = new EarleyState(state.Rule, state.Dot + 1, state.Origin);
                                if (!chart[i + 1].Contains(newState))
                                {
                                    chart[i + 1].Add(newState);
                                }
                            }
                        }
                    }
                }
            }

            foreach (EarleyState state in chart[n])
            {
                if (state.Rule.LeftHandSide == "γ" && state.IsComplete && state.Origin == 0)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsNonTerminal(string symbol)
        {
            List<Rule> rules = this.Grammar.GetRulesForNonterminal(symbol);
            return rules.Count > 0;
        }
    }
}