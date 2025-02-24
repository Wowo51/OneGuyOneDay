using Microsoft.VisualStudio.TestTools.UnitTesting;
using CykAlgorithm;
using System.Collections.Generic;
using System.Text;

namespace CykAlgorithmTest
{
    [TestClass]
    public class CykParserTests
    {
        private Grammar CreateSimpleGrammar()
        {
            List<Production> productions = new List<Production>
            {
                new Production("S", new List<string> { "A", "B" }),
                new Production("A", new List<string> { "a" }),
                new Production("B", new List<string> { "b" })
            };
            return new Grammar("S", productions);
        }

        [TestMethod]
        public void TestParse_ValidInput()
        {
            Grammar grammar = CreateSimpleGrammar();
            CykParser parser = new CykParser();
            string input = "ab";
            bool result = parser.Parse(grammar, input);
            Assert.IsTrue(result, "Expected input 'ab' to be accepted.");
        }

        [TestMethod]
        public void TestParse_InvalidInput()
        {
            Grammar grammar = CreateSimpleGrammar();
            CykParser parser = new CykParser();
            string input = "aa";
            bool result = parser.Parse(grammar, input);
            Assert.IsFalse(result, "Expected input 'aa' to be rejected.");
        }

        [TestMethod]
        public void TestParse_EmptyInput()
        {
            Grammar grammar = CreateSimpleGrammar();
            CykParser parser = new CykParser();
            string input = "";
            bool result = parser.Parse(grammar, input);
            Assert.IsFalse(result, "Expected empty input to be rejected.");
        }

        [TestMethod]
        public void TestParse_NullInput()
        {
            Grammar grammar = CreateSimpleGrammar();
            CykParser parser = new CykParser();
            string? input = null;
            bool result = parser.Parse(grammar, input);
            Assert.IsFalse(result, "Expected null input to be rejected.");
        }

        [TestMethod]
        public void TestParse_RandomLargeInput()
        {
            Grammar grammar = CreateSimpleGrammar();
            CykParser parser = new CykParser();
            string input = GenerateRandomString(50);
            bool result = parser.Parse(grammar, input);
            Assert.IsFalse(result, "Expected random input to be rejected.");
        }

        private string GenerateRandomString(int length)
        {
            StringBuilder builder = new StringBuilder();
            System.Random random = new System.Random();
            for (int i = 0; i < length; i++)
            {
                char c = (random.Next(2) == 0) ? 'a' : 'b';
                builder.Append(c);
            }

            return builder.ToString();
        }
    }
}