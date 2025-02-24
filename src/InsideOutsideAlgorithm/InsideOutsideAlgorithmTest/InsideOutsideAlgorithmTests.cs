using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsideOutsideAlgorithm;
using System;

namespace InsideOutsideAlgorithmTest
{
    [TestClass]
    public class InsideOutsideAlgorithmTests
    {
        private const double Tolerance = 1e-6;
        [TestMethod]
        public void TestReestimateBasic()
        {
            // Arrange: Create a grammar equivalent to the one used in Program.cs.
            string[] sentence = new string[]
            {
                "John",
                "runs"
            };
            ProbabilisticGrammar grammar = new ProbabilisticGrammar("S");
            grammar.Rules.Add(new GrammarRule("S", new string[] { "NP", "VP" }, 0.9));
            grammar.Rules.Add(new GrammarRule("NP", new string[] { "John" }, 1.0));
            grammar.Rules.Add(new GrammarRule("VP", new string[] { "runs" }, 1.0));
            // Act: Run the re-estimation algorithm.
            InsideOutsideCalculator.Reestimate(grammar, sentence);
            // Assert: Expect that each rule's probability is updated to 1.0.
            int index = 0;
            foreach (GrammarRule rule in grammar.Rules)
            {
                Assert.AreEqual(1.0, rule.Probability, Tolerance, "Rule " + rule.LHS + " -> " + string.Join(" ", rule.RHS) + " expected probability 1.0 but was " + rule.Probability);
                index = index + 1;
            }
        }

        [TestMethod]
        public void TestReestimateNonParsing()
        {
            // Arrange: Create a grammar that does not parse the provided sentence.
            string[] sentence = new string[]
            {
                "b"
            };
            ProbabilisticGrammar grammar = new ProbabilisticGrammar("S");
            grammar.Rules.Add(new GrammarRule("S", new string[] { "a" }, 1.0));
            // Act: Run re-estimation.
            InsideOutsideCalculator.Reestimate(grammar, sentence);
            // Assert: Since the token "b" does not match "a", the rule's contribution is zero.
            Assert.AreEqual(0.0, grammar.Rules[0].Probability, Tolerance, "Rule S -> a should have probability 0.0 for a non-parsing sentence.");
        }

        [TestMethod]
        public void TestReestimateSyntheticLarge()
        {
            // Arrange: Build a recursive grammar to test scaling with synthetic data.
            // Grammar:
            //   S -> S S   with initial probability 0.5
            //   S -> "x"   with initial probability 0.5
            ProbabilisticGrammar grammar = new ProbabilisticGrammar("S");
            GrammarRule ruleBinary = new GrammarRule("S", new string[] { "S", "S" }, 0.5);
            GrammarRule ruleUnary = new GrammarRule("S", new string[] { "x" }, 0.5);
            grammar.Rules.Add(ruleBinary);
            grammar.Rules.Add(ruleUnary);
            // Generate a synthetic sentence of 10 tokens, all "x".
            string[] sentence = new string[10];
            for (int i = 0; i < sentence.Length; i++)
            {
                sentence[i] = "x";
            }

            // Act: Run the re-estimation algorithm.
            InsideOutsideCalculator.Reestimate(grammar, sentence);
            // Assert: The probabilities of the two rules (with same LHS "S") should sum to 1.
            double sum = ruleBinary.Probability + ruleUnary.Probability;
            Assert.AreEqual(1.0, sum, Tolerance, "Sum of probabilities for S should be 1.0, but was " + sum);
            Assert.IsTrue(ruleBinary.Probability >= 0.0 && ruleBinary.Probability <= 1.0, "Binary rule probability out of range.");
            Assert.IsTrue(ruleUnary.Probability >= 0.0 && ruleUnary.Probability <= 1.0, "Unary rule probability out of range.");
        }
    }
}