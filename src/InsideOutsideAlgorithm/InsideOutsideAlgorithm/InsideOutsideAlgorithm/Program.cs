using System;

namespace InsideOutsideAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string[] sentence = new string[]
            {
                "John",
                "runs"
            };
            ProbabilisticGrammar grammar = new ProbabilisticGrammar("S");
            grammar.Rules.Add(new GrammarRule("S", new string[] { "NP", "VP" }, 0.9));
            grammar.Rules.Add(new GrammarRule("NP", new string[] { "John" }, 1.0));
            grammar.Rules.Add(new GrammarRule("VP", new string[] { "runs" }, 1.0));
            Console.WriteLine("Before re-estimation:");
            foreach (GrammarRule rule in grammar.Rules)
            {
                Console.WriteLine(rule.LHS + " -> " + string.Join(" ", rule.RHS) + " : " + rule.Probability);
            }

            InsideOutsideCalculator.Reestimate(grammar, sentence);
            Console.WriteLine("After re-estimation:");
            foreach (GrammarRule rule in grammar.Rules)
            {
                Console.WriteLine(rule.LHS + " -> " + string.Join(" ", rule.RHS) + " : " + rule.Probability);
            }
        }
    }
}