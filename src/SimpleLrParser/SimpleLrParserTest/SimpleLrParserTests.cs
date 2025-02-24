using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleLrParser;

namespace SimpleLrParserTest
{
    [TestClass]
    public class SimpleLrParserTests
    {
        [TestMethod]
        public void TestProductionEquality()
        {
            SimpleLrParser.Production prod1 = new SimpleLrParser.Production("A", new List<string> { "a", "B" });
            SimpleLrParser.Production prod2 = new SimpleLrParser.Production("A", new List<string> { "a", "B" });
            Assert.IsTrue(prod1.Equals(prod2));
            Assert.AreEqual(prod1.GetHashCode(), prod2.GetHashCode());
        }

        [TestMethod]
        public void TestParserValidInput()
        {
            // Grammar: E -> id, with augmented production S' -> E
            SimpleLrParser.Grammar grammar = new SimpleLrParser.Grammar("E");
            grammar.AddProduction("E", new List<string> { "id" });
            SimpleLrParser.SLRParser parser = new SimpleLrParser.SLRParser(grammar);
            List<string> tokens = new List<string>
            {
                "id"
            };
            bool result = parser.Parse(tokens);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestParserInvalidInput()
        {
            // Same grammar as valid input tests.
            SimpleLrParser.Grammar grammar = new SimpleLrParser.Grammar("E");
            grammar.AddProduction("E", new List<string> { "id" });
            SimpleLrParser.SLRParser parser = new SimpleLrParser.SLRParser(grammar);
            List<string> tokens = new List<string>
            {
                "wrongtoken"
            };
            bool result = parser.Parse(tokens);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestParserEmptyInputFails()
        {
            // Parser should fail if no tokens are provided.
            SimpleLrParser.Grammar grammar = new SimpleLrParser.Grammar("E");
            grammar.AddProduction("E", new List<string> { "id" });
            SimpleLrParser.SLRParser parser = new SimpleLrParser.SLRParser(grammar);
            List<string> tokens = new List<string>();
            bool result = parser.Parse(tokens);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestParserSyntheticLargeInput()
        {
            // Grammar: S -> A; A -> A x | x
            // Generates a long chain of repeated "x" tokens.
            SimpleLrParser.Grammar grammar = new SimpleLrParser.Grammar("S");
            grammar.AddProduction("S", new List<string> { "A" });
            grammar.AddProduction("A", new List<string> { "A", "x" });
            grammar.AddProduction("A", new List<string> { "x" });
            SimpleLrParser.SLRParser parser = new SimpleLrParser.SLRParser(grammar);
            List<string> tokens = new List<string>();
            for (System.Int32 i = 0; i < 100; i++)
            {
                tokens.Add("x");
            }

            bool result = parser.Parse(tokens);
            Assert.IsTrue(result);
        }
    }
}