using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleLrParser
{
    public class SLRParser
    {
        private Grammar _grammar;
        private Dictionary<int, Dictionary<string, string>> _actionTable = new Dictionary<int, Dictionary<string, string>>();
        private Dictionary<int, Dictionary<string, int>> _gotoTable = new Dictionary<int, Dictionary<string, int>>();
        private List<LRItemSet> _states = new List<LRItemSet>();
        public SLRParser(Grammar grammar)
        {
            _grammar = grammar;
            BuildParsingTables();
        }

        public bool Parse(List<string> tokens)
        {
            List<int> stateStack = new List<int>();
            stateStack.Add(0);
            tokens.Add("$");
            int index = 0;
            while (index < tokens.Count)
            {
                int state = stateStack[stateStack.Count - 1];
                string token = tokens[index];
                if (!_actionTable.ContainsKey(state) || !_actionTable[state].ContainsKey(token))
                {
                    return false;
                }

                string action = _actionTable[state][token];
                if (action.StartsWith("S"))
                {
                    int nextState = int.Parse(action.Substring(1));
                    stateStack.Add(nextState);
                    index++;
                }
                else if (action.StartsWith("R"))
                {
                    int prodIndex = int.Parse(action.Substring(1));
                    if (prodIndex < 0 || prodIndex >= _grammar.Productions.Count)
                    {
                        return false;
                    }

                    Production production = _grammar.Productions[prodIndex];
                    int popCount = production.Right.Count;
                    if (popCount > stateStack.Count - 1)
                    {
                        return false;
                    }

                    for (int i = 0; i < popCount; i++)
                    {
                        stateStack.RemoveAt(stateStack.Count - 1);
                    }

                    int topState = stateStack[stateStack.Count - 1];
                    string gotoSymbol = production.Left;
                    if (!_gotoTable.ContainsKey(topState) || !_gotoTable[topState].ContainsKey(gotoSymbol))
                    {
                        return false;
                    }

                    int gotoState = _gotoTable[topState][gotoSymbol];
                    stateStack.Add(gotoState);
                }
                else if (action == "acc")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        private void BuildParsingTables()
        {
            Grammar augmentedGrammar = new Grammar("S'");
            augmentedGrammar.AddProduction("S'", new List<string> { _grammar.StartSymbol });
            foreach (Production prod in _grammar.Productions)
            {
                augmentedGrammar.Productions.Add(prod);
            }

            _grammar = augmentedGrammar;
            HashSet<string> nonTerminals = new HashSet<string>();
            foreach (Production prod in _grammar.Productions)
            {
                if (!nonTerminals.Contains(prod.Left))
                {
                    nonTerminals.Add(prod.Left);
                }
            }

            List<LRItemSet> states = BuildCanonicalCollection(_grammar, nonTerminals);
            _states = states;
            Dictionary<string, HashSet<string>> first = ComputeFirstSets(_grammar, nonTerminals);
            Dictionary<string, HashSet<string>> follow = ComputeFollowSets(_grammar, nonTerminals, first);
            _actionTable = new Dictionary<int, Dictionary<string, string>>();
            _gotoTable = new Dictionary<int, Dictionary<string, int>>();
            for (int i = 0; i < states.Count; i++)
            {
                _actionTable[i] = new Dictionary<string, string>();
                _gotoTable[i] = new Dictionary<string, int>();
                LRItemSet state = states[i];
                HashSet<string> symbols = new HashSet<string>();
                foreach (LRItem item in state.Items)
                {
                    if (!item.IsComplete())
                    {
                        symbols.Add(item.NextSymbol());
                    }
                }

                foreach (string sym in symbols)
                {
                    LRItemSet gotoSet = Goto(state, sym, _grammar, nonTerminals);
                    if (gotoSet.Items.Count > 0)
                    {
                        int j = states.FindIndex(s => s.Equals(gotoSet));
                        if (j != -1)
                        {
                            if (nonTerminals.Contains(sym))
                            {
                                _gotoTable[i][sym] = j;
                            }
                            else
                            {
                                _actionTable[i][sym] = "S" + j.ToString();
                            }
                        }
                    }
                }

                foreach (LRItem item in state.Items)
                {
                    if (item.IsComplete())
                    {
                        if (item.Production.Left == "S'")
                        {
                            _actionTable[i]["$"] = "acc";
                        }
                        else
                        {
                            if (follow.ContainsKey(item.Production.Left))
                            {
                                int prodIndex = _grammar.Productions.IndexOf(item.Production);
                                foreach (string term in follow[item.Production.Left])
                                {
                                    _actionTable[i][term] = "R" + prodIndex.ToString();
                                }
                            }
                        }
                    }
                }
            }
        }

        private LRItemSet Closure(LRItemSet items, Grammar grammar, HashSet<string> nonTerminals)
        {
            LRItemSet closureSet = new LRItemSet();
            foreach (LRItem item in items.Items)
            {
                closureSet.Add(item);
            }

            bool added = true;
            while (added)
            {
                added = false;
                HashSet<LRItem> newItems = new HashSet<LRItem>();
                foreach (LRItem item in closureSet.Items)
                {
                    if (!item.IsComplete())
                    {
                        string symbol = item.NextSymbol();
                        if (nonTerminals.Contains(symbol))
                        {
                            foreach (Production prod in grammar.Productions)
                            {
                                if (prod.Left == symbol)
                                {
                                    LRItem newItem = new LRItem(prod, 0);
                                    if (!closureSet.Items.Contains(newItem))
                                    {
                                        newItems.Add(newItem);
                                    }
                                }
                            }
                        }
                    }
                }

                if (newItems.Count > 0)
                {
                    foreach (LRItem newItem in newItems)
                    {
                        closureSet.Add(newItem);
                    }

                    added = true;
                }
            }

            return closureSet;
        }

        private LRItemSet Goto(LRItemSet items, string symbol, Grammar grammar, HashSet<string> nonTerminals)
        {
            LRItemSet gotoSet = new LRItemSet();
            foreach (LRItem item in items.Items)
            {
                if (!item.IsComplete() && item.NextSymbol() == symbol)
                {
                    LRItem moved = new LRItem(item.Production, item.DotPosition + 1);
                    gotoSet.Add(moved);
                }
            }

            return Closure(gotoSet, grammar, nonTerminals);
        }

        private List<LRItemSet> BuildCanonicalCollection(Grammar grammar, HashSet<string> nonTerminals)
        {
            List<LRItemSet> states = new List<LRItemSet>();
            LRItemSet startSet = new LRItemSet();
            Production startProd = grammar.Productions[0];
            startSet.Add(new LRItem(startProd, 0));
            LRItemSet startState = Closure(startSet, grammar, nonTerminals);
            states.Add(startState);
            bool added = true;
            while (added)
            {
                added = false;
                List<LRItemSet> newStates = new List<LRItemSet>();
                foreach (LRItemSet state in states)
                {
                    HashSet<string> symbols = new HashSet<string>();
                    foreach (LRItem item in state.Items)
                    {
                        if (!item.IsComplete())
                        {
                            symbols.Add(item.NextSymbol());
                        }
                    }

                    foreach (string sym in symbols)
                    {
                        LRItemSet gotoResult = Goto(state, sym, grammar, nonTerminals);
                        if (gotoResult.Items.Count > 0 && !states.Any(s => s.Equals(gotoResult)) && !newStates.Any(s => s.Equals(gotoResult)))
                        {
                            newStates.Add(gotoResult);
                        }
                    }
                }

                if (newStates.Count > 0)
                {
                    states.AddRange(newStates);
                    added = true;
                }
            }

            return states;
        }

        private Dictionary<string, HashSet<string>> ComputeFirstSets(Grammar grammar, HashSet<string> nonTerminals)
        {
            Dictionary<string, HashSet<string>> first = new Dictionary<string, HashSet<string>>();
            foreach (string nt in nonTerminals)
            {
                first[nt] = new HashSet<string>();
            }

            bool changed = true;
            while (changed)
            {
                changed = false;
                foreach (Production prod in grammar.Productions)
                {
                    string A = prod.Left;
                    if (prod.Right.Count == 0)
                    {
                        if (!first[A].Contains(""))
                        {
                            first[A].Add("");
                            changed = true;
                        }
                    }
                    else
                    {
                        bool allNullable = true;
                        foreach (string symbol in prod.Right)
                        {
                            if (nonTerminals.Contains(symbol))
                            {
                                int beforeCount = first[A].Count;
                                foreach (string s in first[symbol])
                                {
                                    if (s != "")
                                    {
                                        first[A].Add(s);
                                    }
                                }

                                if (!first[symbol].Contains(""))
                                {
                                    allNullable = false;
                                    break;
                                }

                                if (first[A].Count > beforeCount)
                                {
                                    changed = true;
                                }
                            }
                            else
                            {
                                if (!first[A].Contains(symbol))
                                {
                                    first[A].Add(symbol);
                                    changed = true;
                                }

                                allNullable = false;
                                break;
                            }
                        }

                        if (allNullable)
                        {
                            if (!first[A].Contains(""))
                            {
                                first[A].Add("");
                                changed = true;
                            }
                        }
                    }
                }
            }

            return first;
        }

        private Dictionary<string, HashSet<string>> ComputeFollowSets(Grammar grammar, HashSet<string> nonTerminals, Dictionary<string, HashSet<string>> first)
        {
            Dictionary<string, HashSet<string>> follow = new Dictionary<string, HashSet<string>>();
            foreach (string nt in nonTerminals)
            {
                follow[nt] = new HashSet<string>();
            }

            follow[grammar.Productions[0].Left].Add("$");
            bool changed = true;
            while (changed)
            {
                changed = false;
                foreach (Production prod in grammar.Productions)
                {
                    string A = prod.Left;
                    for (int i = 0; i < prod.Right.Count; i++)
                    {
                        string B = prod.Right[i];
                        if (nonTerminals.Contains(B))
                        {
                            HashSet<string> firstBeta = new HashSet<string>();
                            bool betaNullable = true;
                            for (int j = i + 1; j < prod.Right.Count; j++)
                            {
                                string symbol = prod.Right[j];
                                if (nonTerminals.Contains(symbol))
                                {
                                    foreach (string s in first[symbol])
                                    {
                                        if (s != "")
                                        {
                                            firstBeta.Add(s);
                                        }
                                    }

                                    if (!first[symbol].Contains(""))
                                    {
                                        betaNullable = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    firstBeta.Add(symbol);
                                    betaNullable = false;
                                    break;
                                }
                            }

                            if (betaNullable)
                            {
                                firstBeta.UnionWith(follow[A]);
                            }

                            int beforeCount = follow[B].Count;
                            follow[B].UnionWith(firstBeta);
                            if (follow[B].Count > beforeCount)
                            {
                                changed = true;
                            }
                        }
                    }
                }
            }

            return follow;
        }
    }

    internal class LRItem
    {
        public Production Production { get; }
        public int DotPosition { get; set; }

        public LRItem(Production production, int dotPosition)
        {
            Production = production;
            DotPosition = dotPosition;
        }

        public bool IsComplete()
        {
            return DotPosition >= Production.Right.Count;
        }

        public string NextSymbol()
        {
            if (DotPosition < Production.Right.Count)
            {
                return Production.Right[DotPosition];
            }

            return "";
        }

        public void Advance()
        {
            DotPosition++;
        }

        public override bool Equals(object? obj)
        {
            if (!(obj is LRItem other))
            {
                return false;
            }

            return Production.Equals(other.Production) && DotPosition == other.DotPosition;
        }

        public override int GetHashCode()
        {
            int hash = Production.GetHashCode();
            hash = (hash * 397) ^ DotPosition;
            return hash;
        }

        public override string ToString()
        {
            string result = Production.Left + " ->";
            int i;
            for (i = 0; i < Production.Right.Count; i++)
            {
                if (i == DotPosition)
                {
                    result += " .";
                }

                result += " " + Production.Right[i];
            }

            if (i == DotPosition)
            {
                result += " .";
            }

            return result;
        }
    }

    internal class LRItemSet
    {
        public HashSet<LRItem> Items { get; }

        public LRItemSet()
        {
            Items = new HashSet<LRItem>();
        }

        public void Add(LRItem item)
        {
            Items.Add(item);
        }

        public override bool Equals(object? obj)
        {
            if (!(obj is LRItemSet other))
            {
                return false;
            }

            return Items.SetEquals(other.Items);
        }

        public override int GetHashCode()
        {
            int hash = 0;
            foreach (LRItem item in Items)
            {
                hash ^= item.GetHashCode();
            }

            return hash;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (LRItem item in Items)
            {
                sb.AppendLine(item.ToString());
            }

            return sb.ToString();
        }
    }
}