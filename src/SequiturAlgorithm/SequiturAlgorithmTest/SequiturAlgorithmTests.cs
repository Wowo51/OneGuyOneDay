using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SequiturAlgorithm;

namespace SequiturAlgorithmTest
{
    [TestClass]
    public class SequiturAlgorithmTests
    {
        [TestMethod]
        public void TestEmptyInput()
        {
            Sequitur sequitur = new Sequitur();
            Grammar grammar = sequitur.Compress("");
            Assert.AreEqual(0, grammar.MainRule.Definition.Count, "Empty input should result in no tokens.");
            Assert.AreEqual(0, grammar.Rules.Count, "Empty input should have no extra rules.");
        }

        [TestMethod]
        public void TestNoRepetition()
        {
            Sequitur sequitur = new Sequitur();
            string input = "abc";
            Grammar grammar = sequitur.Compress(input);
            CollectionAssert.AreEqual(new List<string> { "a", "b", "c" }, grammar.MainRule.Definition, "Main rule tokens should match the input characters.");
            Assert.AreEqual(0, grammar.Rules.Count, "There should be no rules for input with no repetition.");
        }

        [TestMethod]
        public void TestSimpleRepetition()
        {
            Sequitur sequitur = new Sequitur();
            string input = "abab";
            Grammar grammar = sequitur.Compress(input);
            Assert.AreEqual(2, grammar.MainRule.Definition.Count, "Main rule should have two tokens after compression.");
            Assert.AreEqual(1, grammar.Rules.Count, "There should be one rule created for a repeated pair.");
            Assert.AreEqual("R1", grammar.MainRule.Definition[0], "First token in main rule should be rule name R1.");
            Assert.AreEqual("R1", grammar.MainRule.Definition[1], "Second token in main rule should be rule name R1.");
            CollectionAssert.AreEqual(new List<string> { "a", "b" }, grammar.Rules[0].Definition, "Rule R1 should be defined as ['a', 'b'].");
        }

        [TestMethod]
        public void TestTripleRepetition()
        {
            Sequitur sequitur = new Sequitur();
            string input = "aaa";
            Grammar grammar = sequitur.Compress(input);
            Assert.AreEqual(2, grammar.MainRule.Definition.Count, "Main rule should contain two tokens after compression.");
            CollectionAssert.AreEqual(new List<string> { "a", "a" }, grammar.Rules[0].Definition, "Rule R1 should be defined as ['a', 'a'].");
        }

        [TestMethod]
        public void TestGrammarToString()
        {
            Sequitur sequitur = new Sequitur();
            string input = "abab";
            Grammar grammar = sequitur.Compress(input);
            string output = grammar.ToString();
            Assert.IsTrue(output.Contains("S ->"), "Output should contain 'S ->'.");
            Assert.IsTrue(output.Contains("R1 ->"), "Output should contain 'R1 ->'.");
        }

        [TestMethod]
        public void TestLargeInput()
        {
            StringBuilder builder = new StringBuilder();
            // Generate a long input by repeating a simple pattern.
            for (int i = 0; i < 50; i++)
            {
                builder.Append("abab");
            }

            string input = builder.ToString();
            Sequitur sequitur = new Sequitur();
            Grammar grammar = sequitur.Compress(input);
            int originalLength = input.Length;
            int tokenCount = grammar.MainRule.Definition.Count;
            foreach (Rule rule in grammar.Rules)
            {
                tokenCount += rule.Definition.Count;
            }

            Assert.IsTrue(tokenCount < originalLength, "Compressed grammar token count should be less than the original length for repeating patterns.");
        }
    }
}