using System;
using System.Collections.Generic;
using System.Linq;
using HopcroftMooreBrzozowski.Models;

namespace HopcroftMooreBrzozowski.Minimizers
{
    public class MooreMinimizer : IDFAMinimizer
    {
        public DeterministicFiniteAutomaton Minimize(DeterministicFiniteAutomaton dfa)
        {
            int n = dfa.States.Count;
            var stateIndex = new Dictionary<string, int>();
            for (int i = 0; i < n; i++)
            {
                stateIndex[dfa.States[i]] = i;
            }

            bool[, ] distinguishable = new bool[n, n];
            // Mark pairs where one is accepting and one is not.
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    bool isIAcc = dfa.AcceptStates.Contains(dfa.States[i]);
                    bool isJAcc = dfa.AcceptStates.Contains(dfa.States[j]);
                    if (isIAcc != isJAcc)
                    {
                        distinguishable[i, j] = true;
                    }
                }
            }

            bool changed = true;
            while (changed)
            {
                changed = false;
                for (int i = 0; i < n; i++)
                {
                    for (int j = i + 1; j < n; j++)
                    {
                        if (!distinguishable[i, j])
                        {
                            foreach (char symbol in dfa.Alphabet)
                            {
                                string si = dfa.Transitions[dfa.States[i]][symbol];
                                string sj = dfa.Transitions[dfa.States[j]][symbol];
                                if (si == null || sj == null)
                                    continue;
                                int indexSi = stateIndex.ContainsKey(si) ? stateIndex[si] : -1;
                                int indexSj = stateIndex.ContainsKey(sj) ? stateIndex[sj] : -1;
                                if (indexSi == -1 || indexSj == -1)
                                    continue;
                                int a = Math.Min(indexSi, indexSj);
                                int b = Math.Max(indexSi, indexSj);
                                if (distinguishable[a, b])
                                {
                                    distinguishable[i, j] = true;
                                    changed = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            // Use union-find to build equivalence classes.
            int[] parent = new int[n];
            for (int i = 0; i < n; i++)
            {
                parent[i] = i;
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (!distinguishable[i, j])
                    {
                        Union(parent, i, j);
                    }
                }
            }

            var classes = new Dictionary<int, List<string>>();
            for (int i = 0; i < n; i++)
            {
                int rep = Find(parent, i);
                if (!classes.ContainsKey(rep))
                {
                    classes[rep] = new List<string>();
                }

                classes[rep].Add(dfa.States[i]);
            }

            var newStates = new List<string>();
            var newTransitions = new Dictionary<string, Dictionary<char, string>>();
            string newStartState = "";
            var newAcceptStates = new List<string>();
            var stateToClass = new Dictionary<string, string>();
            foreach (var entry in classes)
            {
                var group = entry.Value;
                group.Sort();
                string className = string.Join(",", group);
                newStates.Add(className);
                foreach (string state in group)
                {
                    stateToClass[state] = className;
                }

                if (group.Contains(dfa.StartState))
                {
                    newStartState = className;
                }

                if (group.Intersect(dfa.AcceptStates).Any())
                {
                    newAcceptStates.Add(className);
                }
            }

            foreach (string className in newStates)
            {
                newTransitions[className] = new Dictionary<char, string>();
                string repState = stateToClass.First(pair => pair.Value == className).Key;
                foreach (char symbol in dfa.Alphabet)
                {
                    if (dfa.Transitions[repState].ContainsKey(symbol))
                    {
                        string target = dfa.Transitions[repState][symbol];
                        if (stateToClass.ContainsKey(target))
                        {
                            newTransitions[className][symbol] = stateToClass[target];
                        }
                    }
                }
            }

            return new DeterministicFiniteAutomaton(newStates, dfa.Alphabet, newTransitions, newStartState, newAcceptStates);
        }

        private int Find(int[] parent, int i)
        {
            if (parent[i] != i)
            {
                parent[i] = Find(parent, parent[i]);
            }

            return parent[i];
        }

        private void Union(int[] parent, int i, int j)
        {
            int ri = Find(parent, i);
            int rj = Find(parent, j);
            if (ri < rj)
            {
                parent[rj] = ri;
            }
            else
            {
                parent[ri] = rj;
            }
        }
    }
}