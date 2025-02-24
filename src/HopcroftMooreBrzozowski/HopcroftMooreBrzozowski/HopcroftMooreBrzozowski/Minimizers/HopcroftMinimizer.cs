using System;
using System.Collections.Generic;
using System.Linq;
using HopcroftMooreBrzozowski.Models;

namespace HopcroftMooreBrzozowski.Minimizers
{
    public class HopcroftMinimizer : IDFAMinimizer
    {
        public DeterministicFiniteAutomaton Minimize(DeterministicFiniteAutomaton dfa)
        {
            List<HashSet<string>> partitions = new List<HashSet<string>>();
            HashSet<string> finalStates = new HashSet<string>(dfa.AcceptStates);
            HashSet<string> nonFinalStates = new HashSet<string>(dfa.States.Except(dfa.AcceptStates));
            if (finalStates.Count > 0)
            {
                partitions.Add(finalStates);
            }

            if (nonFinalStates.Count > 0)
            {
                partitions.Add(nonFinalStates);
            }

            Queue<HashSet<string>> workList = new Queue<HashSet<string>>();
            if (finalStates.Count <= nonFinalStates.Count && finalStates.Count > 0)
            {
                workList.Enqueue(finalStates);
            }
            else if (nonFinalStates.Count > 0)
            {
                workList.Enqueue(nonFinalStates);
            }

            while (workList.Count > 0)
            {
                HashSet<string> A = workList.Dequeue();
                foreach (char symbol in dfa.Alphabet)
                {
                    HashSet<string> X = new HashSet<string>();
                    foreach (string state in dfa.States)
                    {
                        if (dfa.Transitions.ContainsKey(state) && dfa.Transitions[state].ContainsKey(symbol))
                        {
                            string target = dfa.Transitions[state][symbol];
                            if (A.Contains(target))
                            {
                                X.Add(state);
                            }
                        }
                    }

                    List<HashSet<string>> partitionsToUpdate = new List<HashSet<string>>();
                    foreach (HashSet<string> Y in partitions)
                    {
                        HashSet<string> intersection = new HashSet<string>(Y.Intersect(X));
                        if (intersection.Count > 0 && intersection.Count < Y.Count)
                        {
                            partitionsToUpdate.Add(Y);
                        }
                    }

                    foreach (HashSet<string> Y in partitionsToUpdate)
                    {
                        HashSet<string> intersection = new HashSet<string>(Y.Intersect(X));
                        HashSet<string> difference = new HashSet<string>(Y.Except(X));
                        partitions.Remove(Y);
                        partitions.Add(intersection);
                        partitions.Add(difference);
                        Queue<HashSet<string>> newWorkList = new Queue<HashSet<string>>();
                        while (workList.Count > 0)
                        {
                            newWorkList.Enqueue(workList.Dequeue());
                        }

                        if (newWorkList.Any(w => w.SetEquals(Y)))
                        {
                            var updated = new Queue<HashSet<string>>();
                            while (newWorkList.Count > 0)
                            {
                                var item = newWorkList.Dequeue();
                                if (item.SetEquals(Y))
                                {
                                    updated.Enqueue(intersection);
                                    updated.Enqueue(difference);
                                }
                                else
                                {
                                    updated.Enqueue(item);
                                }
                            }

                            newWorkList = updated;
                        }
                        else
                        {
                            if (intersection.Count <= difference.Count)
                            {
                                newWorkList.Enqueue(intersection);
                            }
                            else
                            {
                                newWorkList.Enqueue(difference);
                            }
                        }

                        workList = newWorkList;
                    }
                }
            }

            var stateToPartition = new Dictionary<string, string>();
            var newStates = new List<string>();
            var newTransitions = new Dictionary<string, Dictionary<char, string>>();
            string newStartState = "";
            var newAcceptStates = new List<string>();
            foreach (HashSet<string> part in partitions)
            {
                List<string> listPart = part.ToList();
                listPart.Sort();
                string partName = string.Join(",", listPart);
                newStates.Add(partName);
                foreach (string st in part)
                {
                    stateToPartition[st] = partName;
                }

                if (part.Contains(dfa.StartState))
                {
                    newStartState = partName;
                }

                if (part.Intersect(dfa.AcceptStates).Any())
                {
                    newAcceptStates.Add(partName);
                }
            }

            foreach (string partName in newStates)
            {
                newTransitions[partName] = new Dictionary<char, string>();
                string representative = stateToPartition.First(pair => pair.Value == partName).Key;
                foreach (char symbol in dfa.Alphabet)
                {
                    if (dfa.Transitions.ContainsKey(representative) && dfa.Transitions[representative].ContainsKey(symbol))
                    {
                        string targetState = dfa.Transitions[representative][symbol];
                        if (stateToPartition.ContainsKey(targetState))
                        {
                            newTransitions[partName][symbol] = stateToPartition[targetState];
                        }
                    }
                }
            }

            return new DeterministicFiniteAutomaton(newStates, dfa.Alphabet, newTransitions, newStartState, newAcceptStates);
        }
    }
}