using System;
using System.Collections.Generic;

namespace PowersetConstruction
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NFA nfa = new NFA();
            nfa.StartState = 0;
            nfa.AddAcceptState(2);
            nfa.AddTransition(0, 'a', 0);
            nfa.AddTransition(0, 'a', 1);
            nfa.AddTransition(1, 'b', 2);
            DFA dfa = PowersetConverter.ConvertNfaToDfa(nfa);
            Console.WriteLine("DFA States:");
            foreach (string state in dfa.States)
            {
                Console.WriteLine(state);
            }

            Console.WriteLine("DFA Start State:");
            Console.WriteLine(dfa.StartState);
            Console.WriteLine("DFA Accept States:");
            foreach (string state in dfa.AcceptStates)
            {
                Console.WriteLine(state);
            }

            Console.WriteLine("DFA Transitions:");
            foreach (KeyValuePair<Tuple<string, char>, string> transition in dfa.Transitions)
            {
                Console.WriteLine("From: {0} on '{1}' -> To: {2}", transition.Key.Item1, transition.Key.Item2, transition.Value);
            }
        }
    }
}