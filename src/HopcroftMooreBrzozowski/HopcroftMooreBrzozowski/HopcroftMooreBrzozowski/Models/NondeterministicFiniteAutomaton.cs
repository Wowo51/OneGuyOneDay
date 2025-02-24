using System;
using System.Collections.Generic;

namespace HopcroftMooreBrzozowski.Models
{
    public class NondeterministicFiniteAutomaton
    {
        public List<string> States { get; }
        public List<char> Alphabet { get; }
        public Dictionary<string, Dictionary<char, List<string>>> Transitions { get; }
        public List<string> StartStates { get; }
        public List<string> AcceptStates { get; }

        public NondeterministicFiniteAutomaton(List<string> states, List<char> alphabet, Dictionary<string, Dictionary<char, List<string>>> transitions, List<string> startStates, List<string> acceptStates)
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
                transitions = new Dictionary<string, Dictionary<char, List<string>>>();
            }

            if (startStates == null)
            {
                startStates = new List<string>();
            }

            if (acceptStates == null)
            {
                acceptStates = new List<string>();
            }

            States = states;
            Alphabet = alphabet;
            Transitions = transitions;
            StartStates = startStates;
            AcceptStates = acceptStates;
        }
    }
}