using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CanonicalLRParser;

namespace CanonicalLRParserTest
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void TestGrammarAugmentation()
        {
            List<Production> productions = new List<Production>
            {
                new Production("S", new List<string> { "a" })
            };
            Grammar grammar = new Grammar(productions, "S");
            Grammar augGrammar = grammar.Augment();
            Assert.AreEqual("S'", augGrammar.StartSymbol);
            Assert.AreEqual(productions.Count + 1, augGrammar.Productions.Count);
        }

        [TestMethod]
        public void TestComputeFirstSets()
        {
            List<Production> productions = new List<Production>
            {
                new Production("S", new List<string> { "a" })
            };
            Grammar grammar = new Grammar(productions, "S");
            Dictionary<string, HashSet<string>> first = grammar.ComputeFirstSets();
            Assert.IsTrue(first.ContainsKey("S"));
            Assert.IsTrue(first["S"].Contains("a"));
        }

        [TestMethod]
        public void TestComputeFollowSets()
        {
            List<Production> productions = new List<Production>
            {
                new Production("S", new List<string> { "a" })
            };
            Grammar grammar = new Grammar(productions, "S");
            Dictionary<string, HashSet<string>> follow = grammar.ComputeFollowSets();
            Assert.IsTrue(follow.ContainsKey("S"));
            Assert.IsTrue(follow["S"].Contains("$"));
        }

        [TestMethod]
        public void TestLRItemAdvance()
        {
            Production prod = new Production("S", new List<string> { "a", "b" });
            LRItem item = new LRItem(prod, 0);
            LRItem nextItem = item.Advance();
            Assert.AreEqual(1, nextItem.Dot);
            Assert.AreEqual(prod, nextItem.Production);
        }

        [TestMethod]
        public void TestParseValid()
        {
            List<Production> productions = new List<Production>
            {
                new Production("S", new List<string> { "a" })
            };
            Grammar grammar = new Grammar(productions, "S");
            LRParser parser = new LRParser(grammar);
            List<string> tokens = new List<string>
            {
                "a"
            };
            bool result = parser.Parse(tokens);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestParseInvalid()
        {
            List<Production> productions = new List<Production>
            {
                new Production("S", new List<string> { "a" })
            };
            Grammar grammar = new Grammar(productions, "S");
            LRParser parser = new LRParser(grammar);
            List<string> tokens = new List<string>
            {
                "b"
            };
            bool result = parser.Parse(tokens);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestLargeInput()
        {
            List<Production> productions = new List<Production>
            {
                new Production("S", new List<string> { "a", "S" }),
                new Production("S", new List<string>())
            };
            Grammar grammar = new Grammar(productions, "S");
            LRParser parser = new LRParser(grammar);
            List<string> tokens = new List<string>();
            for (int i = 0; i < 1000; i++)
            {
                tokens.Add("a");
            }

            bool result = parser.Parse(tokens);
            Assert.IsTrue(result);
        }
    }
}