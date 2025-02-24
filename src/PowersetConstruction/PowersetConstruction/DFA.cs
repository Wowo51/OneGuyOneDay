using System;
using System.Collections.Generic;

namespace PowersetConstruction
{
    public class DFA
    {
        public HashSet<string> States { get; }
        public string StartState { get; set; }
        public HashSet<string> AcceptStates { get; }
        public Dictionary<Tuple<string, char>, string> Transitions { get; }

        public DFA()
        {
            States = new HashSet<string>();
            AcceptStates = new HashSet<string>();
            Transitions = new Dictionary<Tuple<string, char>, string>();
            StartState = string.Empty;
        }

        public void AddTransition(string fromState, char symbol, string toState)
        {
            Tuple<string, char> key = new Tuple<string, char>(fromState, symbol);
            if (!Transitions.ContainsKey(key))
            {
                Transitions[key] = toState;
            }
        }
    }
}