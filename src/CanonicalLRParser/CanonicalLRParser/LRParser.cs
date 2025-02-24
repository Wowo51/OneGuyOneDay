using System;
using System.Collections.Generic;
using System.Text;

namespace CanonicalLRParser
{
    public class LRItem
    {
        public Production Production { get; }
        public int Dot { get; }

        public LRItem(Production production, int dot)
        {
            this.Production = production;
            this.Dot = dot;
        }

        public string NextSymbol()
        {
            if (Dot < Production.Right.Count)
                return Production.Right[Dot];
            return null;
        }

        public bool IsComplete()
        {
            return Dot >= Production.Right.Count;
        }

        public LRItem Advance()
        {
            return new LRItem(Production, Dot + 1);
        }

        public override bool Equals(object obj)
        {
            if (obj is LRItem)
            {
                LRItem other = (LRItem)obj;
                return Production.Equals(other.Production) && Dot == other.Dot;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Production.GetHashCode() * 31 + Dot.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Production.Left).Append(" -> ");
            for (int i = 0; i < Production.Right.Count; i++)
            {
                if (i == Dot)
                    sb.Append("• ");
                sb.Append(Production.Right[i]).Append(" ");
            }

            if (Dot == Production.Right.Count)
                sb.Append("•");
            return sb.ToString().Trim();
        }
    }

    public class LRParser
    {
        public Grammar Grammar { get; }
        public List<HashSet<LRItem>> States { get; private set; }
        public Dictionary<Tuple<int, string>, int> Transitions { get; private set; }
        public Dictionary<Tuple<int, string>, string> ActionTable { get; private set; }
        public Dictionary<Tuple<int, string>, int> GotoTable { get; private set; }

        public LRParser(Grammar grammar)
        {
            this.Grammar = grammar.Augment();
            this.States = new List<HashSet<LRItem>>();
            this.Transitions = new Dictionary<Tuple<int, string>, int>();
            BuildCanonicalCollection();
            BuildParsingTable();
        }

        private HashSet<LRItem> Closure(HashSet<LRItem> items)
        {
            HashSet<LRItem> closure = new HashSet<LRItem>(items);
            bool added = true;
            while (added)
            {
                added = false;
                List<LRItem> toAdd = new List<LRItem>();
                foreach (LRItem item in closure)
                {
                    string symbol = item.NextSymbol();
                    if (symbol != null && Grammar.NonTerminals.Contains(symbol))
                    {
                        foreach (Production p in Grammar.Productions)
                        {
                            if (p.Left == symbol)
                            {
                                LRItem newItem = new LRItem(p, 0);
                                if (!closure.Contains(newItem) && !toAdd.Contains(newItem))
                                {
                                    toAdd.Add(newItem);
                                    added = true;
                                }
                            }
                        }
                    }
                }

                foreach (LRItem it in toAdd)
                    closure.Add(it);
            }

            return closure;
        }

        private HashSet<LRItem> Goto(HashSet<LRItem> items, string symbol)
        {
            HashSet<LRItem> gotoSet = new HashSet<LRItem>();
            foreach (LRItem item in items)
            {
                if (item.NextSymbol() == symbol)
                    gotoSet.Add(item.Advance());
            }

            return Closure(gotoSet);
        }

        private bool SetEquals(HashSet<LRItem> a, HashSet<LRItem> b)
        {
            return a.SetEquals(b);
        }

        private void BuildCanonicalCollection()
        {
            HashSet<LRItem> start = new HashSet<LRItem>();
            start.Add(new LRItem(Grammar.Productions[0], 0));
            start = Closure(start);
            States.Add(start);
            bool added = true;
            while (added)
            {
                added = false;
                for (int i = 0; i < States.Count; i++)
                {
                    HashSet<LRItem> state = States[i];
                    HashSet<string> symbols = new HashSet<string>();
                    foreach (LRItem item in state)
                    {
                        string sym = item.NextSymbol();
                        if (sym != null)
                            symbols.Add(sym);
                    }

                    foreach (string sym in symbols)
                    {
                        HashSet<LRItem> next = Goto(state, sym);
                        if (next.Count == 0)
                            continue;
                        int index = -1;
                        for (int j = 0; j < States.Count; j++)
                        {
                            if (SetEquals(States[j], next))
                            {
                                index = j;
                                break;
                            }
                        }

                        if (index == -1)
                        {
                            States.Add(next);
                            index = States.Count - 1;
                            added = true;
                        }

                        Tuple<int, string> key = Tuple.Create(i, sym);
                        if (!Transitions.ContainsKey(key))
                            Transitions[key] = index;
                    }
                }
            }
        }

        private void BuildParsingTable()
        {
            ActionTable = new Dictionary<Tuple<int, string>, string>();
            GotoTable = new Dictionary<Tuple<int, string>, int>();
            HashSet<string> terminals = new HashSet<string>(Grammar.Terminals);
            terminals.Add("$");
            foreach (var kv in Transitions)
            {
                int state = kv.Key.Item1;
                string symbol = kv.Key.Item2;
                int target = kv.Value;
                if (Grammar.Terminals.Contains(symbol))
                    ActionTable[Tuple.Create(state, symbol)] = "s" + target.ToString();
                else if (Grammar.NonTerminals.Contains(symbol))
                    GotoTable[Tuple.Create(state, symbol)] = target;
            }

            Dictionary<string, HashSet<string>> follow = Grammar.ComputeFollowSets();
            for (int i = 0; i < States.Count; i++)
            {
                foreach (LRItem item in States[i])
                {
                    if (item.IsComplete())
                    {
                        if (item.Production.Left == Grammar.StartSymbol)
                        {
                            ActionTable[Tuple.Create(i, "$")] = "acc";
                        }
                        else
                        {
                            int prodIndex = Grammar.Productions.IndexOf(item.Production);
                            if (prodIndex < 0)
                                continue;
                            if (follow.ContainsKey(item.Production.Left))
                            {
                                foreach (string term in follow[item.Production.Left])
                                {
                                    Tuple<int, string> key = Tuple.Create(i, term);
                                    ActionTable[key] = "r" + prodIndex.ToString();
                                }
                            }
                        }
                    }
                }
            }
        }

        public bool Parse(List<string> tokens)
        {
            List<int> stack = new List<int>();
            stack.Add(0);
            tokens.Add("$");
            int index = 0;
            while (index < tokens.Count)
            {
                int state = stack[stack.Count - 1];
                string token = tokens[index];
                Tuple<int, string> actionKey = Tuple.Create(state, token);
                if (ActionTable.ContainsKey(actionKey))
                {
                    string action = ActionTable[actionKey];
                    if (action.StartsWith("s"))
                    {
                        int nextState = int.Parse(action.Substring(1));
                        stack.Add(nextState);
                        index++;
                    }
                    else if (action.StartsWith("r"))
                    {
                        int prodIndex = int.Parse(action.Substring(1));
                        Production prod = Grammar.Productions[prodIndex];
                        int popCount = prod.Right.Count;
                        for (int i = 0; i < popCount; i++)
                        {
                            if (stack.Count > 0)
                                stack.RemoveAt(stack.Count - 1);
                        }

                        int topState = stack[stack.Count - 1];
                        Tuple<int, string> gotoKey = Tuple.Create(topState, prod.Left);
                        if (GotoTable.ContainsKey(gotoKey))
                        {
                            stack.Add(GotoTable[gotoKey]);
                        }
                        else
                        {
                            return false;
                        }
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
                else
                {
                    return false;
                }
            }

            return false;
        }
    }
}