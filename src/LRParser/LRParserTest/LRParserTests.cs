using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using LRParser.Parser;

namespace LRParserTest
{
    [TestClass]
    public class LRParserTests
    {
        private void ValidateParser(ILRParser parser, string expectedRootLabel, List<Token> tokens)
        {
            ParseTree tree = parser.Parse(tokens);
            Assert.AreEqual(expectedRootLabel, tree.Node, "Root label mismatch.");
            Assert.AreEqual(tokens.Count, tree.Children.Count, "Children count mismatch.");
            for (int i = 0; i < tokens.Count; i++)
            {
                Assert.AreEqual(tokens[i].Value, tree.Children[i].Node, "Child token mismatch at index " + i);
            }
        }

        [TestMethod]
        public void TestCanonicalLRParserWithValidTokens()
        {
            List<Token> testTokens = new List<Token>
            {
                new Token("Token1"),
                new Token("Token2")
            };
            ILRParser parser = ParserFactory.CreateParser("CanonicalLR");
            ValidateParser(parser, "CanonicalLR", testTokens);
        }

        [TestMethod]
        public void TestLALRParserWithValidTokens()
        {
            List<Token> testTokens = new List<Token>
            {
                new Token("LALR1"),
                new Token("LALR2")
            };
            ILRParser parser = ParserFactory.CreateParser("LALR");
            ValidateParser(parser, "LALR", testTokens);
        }

        [TestMethod]
        public void TestSLRParserWithValidTokens()
        {
            List<Token> testTokens = new List<Token>
            {
                new Token("SLR1"),
                new Token("SLR2")
            };
            ILRParser parser = ParserFactory.CreateParser("SLR");
            ValidateParser(parser, "SLR", testTokens);
        }

        [TestMethod]
        public void TestOperatorPrecedenceParserWithValidTokens()
        {
            List<Token> testTokens = new List<Token>
            {
                new Token("OP1"),
                new Token("OP2")
            };
            ILRParser parser = ParserFactory.CreateParser("OperatorPrecedence");
            ValidateParser(parser, "OperatorPrecedence", testTokens);
        }

        [TestMethod]
        public void TestSimplePrecedenceParserWithValidTokens()
        {
            List<Token> testTokens = new List<Token>
            {
                new Token("SP1"),
                new Token("SP2")
            };
            ILRParser parser = ParserFactory.CreateParser("SimplePrecedence");
            ValidateParser(parser, "SimplePrecedence", testTokens);
        }

        [TestMethod]
        public void TestNullTokensForEachParser()
        {
            ILRParser canonicalParser = ParserFactory.CreateParser("CanonicalLR");
            ParseTree canonicalTree = canonicalParser.Parse(null);
            Assert.AreEqual("CanonicalLR", canonicalTree.Node);
            Assert.AreEqual(0, canonicalTree.Children.Count);
            ILRParser lalrParser = ParserFactory.CreateParser("LALR");
            ParseTree lalrTree = lalrParser.Parse(null);
            Assert.AreEqual("LALR", lalrTree.Node);
            Assert.AreEqual(0, lalrTree.Children.Count);
            ILRParser slrParser = ParserFactory.CreateParser("SLR");
            ParseTree slrTree = slrParser.Parse(null);
            Assert.AreEqual("SLR", slrTree.Node);
            Assert.AreEqual(0, slrTree.Children.Count);
            ILRParser opParser = ParserFactory.CreateParser("OperatorPrecedence");
            ParseTree opTree = opParser.Parse(null);
            Assert.AreEqual("OperatorPrecedence", opTree.Node);
            Assert.AreEqual(0, opTree.Children.Count);
            ILRParser spParser = ParserFactory.CreateParser("SimplePrecedence");
            ParseTree spTree = spParser.Parse(null);
            Assert.AreEqual("SimplePrecedence", spTree.Node);
            Assert.AreEqual(0, spTree.Children.Count);
        }

        [TestMethod]
        public void TestParserWithLargeInput()
        {
            List<Token> largeTokens = new List<Token>();
            for (int i = 0; i < 1000; i++)
            {
                largeTokens.Add(new Token("Token_" + i.ToString()));
            }

            ILRParser parser = ParserFactory.CreateParser("CanonicalLR");
            ValidateParser(parser, "CanonicalLR", largeTokens);
        }
    }
}