using System;
using System.Collections.Generic;

namespace HopcroftMooreBrzozowski.Models
{
    public class DeterministicFiniteAutomaton
    {
        public List<string> States { get; }
        public List<char> Alphabet { get; }
        public Dictionary<string, Dictionary<char, string>> Transitions { get; }
        public string StartState { get; }
        public List<string> AcceptStates { get; }

        public DeterministicFiniteAutomaton(List<string> states, List<char> alphabet, Dictionary<string, Dictionary<char, string>> transitions, string startState, List<string> acceptStates)
        {
            if (states == null)
            {
                states = new List<string>();
            }

            if (alphabet == null)
            {
                alphabet = new List<char>();
            }

            if (transitions == null)
            {
                transitions = new Dictionary<string, Dictionary<char, string>>();
            }

            if (startState == null)
            {
                startState = string.Empty;
            }

            if (acceptStates == null)
            {
                acceptStates = new List<string>();
            }

            States = states;
            Alphabet = alphabet;
            Transitions = transitions;
            StartState = startState;
            AcceptStates = acceptStates;
        }
    }
}