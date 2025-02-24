using System;
using System.Collections.Generic;

namespace PowersetConstruction
{
    public class NFA
    {
        public HashSet<int> States { get; }
        public int StartState { get; set; }
        public HashSet<int> AcceptStates { get; }
        public Dictionary<Tuple<int, char>, HashSet<int>> Transitions { get; }

        public NFA()
        {
            States = new HashSet<int>();
            AcceptStates = new HashSet<int>();
            Transitions = new Dictionary<Tuple<int, char>, HashSet<int>>();
        }

        public void AddState(int state)
        {
            if (!States.Contains(state))
            {
                States.Add(state);
            }
        }

        public void AddAcceptState(int state)
        {
            AddState(state);
            if (!AcceptStates.Contains(state))
            {
                AcceptStates.Add(state);
            }
        }

        public void AddTransition(int fromState, char symbol, int toState)
        {
            AddState(fromState);
            AddState(toState);
            Tuple<int, char> key = new Tuple<int, char>(fromState, symbol);
            if (!Transitions.ContainsKey(key))
            {
                Transitions[key] = new HashSet<int>();
            }

            Transitions[key].Add(toState);
        }

        public HashSet<int> GetTransitions(int state, char symbol)
        {
            Tuple<int, char> key = new Tuple<int, char>(state, symbol);
            if (Transitions.ContainsKey(key))
            {
                return Transitions[key];
            }
            else
            {
                return new HashSet<int>();
            }
        }
    }
}