using System;
using System.Collections.Generic;
using System.Text;

namespace CanonicalLRParser
{
    public class Production
    {
        public string Left { get; }
        public List<string> Right { get; }

        public Production(string left, List<string> right)
        {
            this.Left = left;
            this.Right = right;
        }

        public override string ToString()
        {
            return Left + " -> " + string.Join(" ", Right);
        }

        public override bool Equals(object obj)
        {
            if (obj is Production)
            {
                Production other = (Production)obj;
                if (Left != other.Left || Right.Count != other.Right.Count)
                    return false;
                for (int i = 0; i < Right.Count; i++)
                {
                    if (Right[i] != other.Right[i])
                        return false;
                }

                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            int hash = Left.GetHashCode();
            foreach (string s in Right)
                hash = hash * 31 + s.GetHashCode();
            return hash;
        }
    }

    public class Grammar
    {
        public List<Production> Productions { get; }
        public string StartSymbol { get; }
        public HashSet<string> NonTerminals { get; }
        public HashSet<string> Terminals { get; }

        public Grammar(List<Production> productions, string startSymbol)
        {
            this.Productions = productions;
            this.StartSymbol = startSymbol;
            this.NonTerminals = new HashSet<string>();
            foreach (var p in productions)
                this.NonTerminals.Add(p.Left);
            this.Terminals = new HashSet<string>();
            foreach (var p in productions)
            {
                foreach (string sym in p.Right)
                {
                    if (!NonTerminals.Contains(sym) && sym != "ε")
                        this.Terminals.Add(sym);
                }
            }
        }

        public Grammar Augment()
        {
            List<Production> aug = new List<Production>();
            string newStart = StartSymbol + "'";
            aug.Add(new Production(newStart, new List<string> { StartSymbol }));
            foreach (var p in Productions)
                aug.Add(p);
            return new Grammar(aug, newStart);
        }

        public Dictionary<string, HashSet<string>> ComputeFirstSets()
        {
            Dictionary<string, HashSet<string>> first = new Dictionary<string, HashSet<string>>();
            foreach (string nt in NonTerminals)
                first[nt] = new HashSet<string>();
            bool changed = true;
            while (changed)
            {
                changed = false;
                foreach (var p in Productions)
                {
                    HashSet<string> leftSet = first.ContainsKey(p.Left) ? first[p.Left] : new HashSet<string>();
                    if (p.Right.Count == 0)
                    {
                        if (leftSet.Add("ε"))
                            changed = true;
                    }
                    else
                    {
                        bool allEpsilon = true;
                        foreach (string sym in p.Right)
                        {
                            if (NonTerminals.Contains(sym))
                            {
                                int countBefore = leftSet.Count;
                                foreach (string s in first[sym])
                                {
                                    if (s != "ε")
                                        leftSet.Add(s);
                                }

                                if (!first[sym].Contains("ε"))
                                {
                                    allEpsilon = false;
                                    break;
                                }
                            }
                            else
                            {
                                if (leftSet.Add(sym))
                                    changed = true;
                                allEpsilon = false;
                                break;
                            }
                        }

                        if (allEpsilon)
                        {
                            if (leftSet.Add("ε"))
                                changed = true;
                        }
                    }
                }
            }

            return first;
        }

        public Dictionary<string, HashSet<string>> ComputeFollowSets()
        {
            Dictionary<string, HashSet<string>> follow = new Dictionary<string, HashSet<string>>();
            foreach (string nt in NonTerminals)
                follow[nt] = new HashSet<string>();
            follow[StartSymbol].Add("$");
            Dictionary<string, HashSet<string>> first = ComputeFirstSets();
            bool changed = true;
            while (changed)
            {
                changed = false;
                foreach (var p in Productions)
                {
                    for (int i = 0; i < p.Right.Count; i++)
                    {
                        string sym = p.Right[i];
                        if (NonTerminals.Contains(sym))
                        {
                            int before = follow[sym].Count;
                            bool allEpsilon = true;
                            for (int j = i + 1; j < p.Right.Count; j++)
                            {
                                string next = p.Right[j];
                                if (NonTerminals.Contains(next))
                                {
                                    foreach (string s in first[next])
                                    {
                                        if (s != "ε")
                                            follow[sym].Add(s);
                                    }

                                    if (!first[next].Contains("ε"))
                                    {
                                        allEpsilon = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    follow[sym].Add(next);
                                    allEpsilon = false;
                                    break;
                                }
                            }

                            if (allEpsilon)
                            {
                                foreach (string s in follow[p.Left])
                                    follow[sym].Add(s);
                            }

                            if (follow[sym].Count > before)
                                changed = true;
                        }
                    }
                }
            }

            return follow;
        }
    }
}