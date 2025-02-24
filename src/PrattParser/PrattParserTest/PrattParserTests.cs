using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrattParser;

namespace PrattParserTest
{
    [TestClass]
    public class PrattParserTests
    {
        [TestMethod]
        public void TestNumberExpression()
        {
            string input = "42";
            Lexer lexer = new Lexer(input);
            List<Token> tokens = lexer.Tokenize();
            Parser parser = new Parser(tokens);
            Expression expr = parser.ParseExpression();
            Assert.IsNotNull(expr);
            Assert.AreEqual("42", expr.ToString());
        }

        [TestMethod]
        public void TestBinaryAddition()
        {
            string input = "1+2";
            Lexer lexer = new Lexer(input);
            List<Token> tokens = lexer.Tokenize();
            Parser parser = new Parser(tokens);
            Expression expr = parser.ParseExpression();
            Assert.IsNotNull(expr);
            Assert.AreEqual("(1 + 2)", expr.ToString());
        }

        [TestMethod]
        public void TestOperatorPrecedence()
        {
            string input = "1+2*3";
            Lexer lexer = new Lexer(input);
            List<Token> tokens = lexer.Tokenize();
            Parser parser = new Parser(tokens);
            Expression expr = parser.ParseExpression();
            Assert.IsNotNull(expr);
            Assert.AreEqual("(1 + (2 * 3))", expr.ToString());
        }

        [TestMethod]
        public void TestUnaryExpression()
        {
            string input = "-5";
            Lexer lexer = new Lexer(input);
            List<Token> tokens = lexer.Tokenize();
            Parser parser = new Parser(tokens);
            Expression expr = parser.ParseExpression();
            Assert.IsNotNull(expr);
            Assert.AreEqual("(-5)", expr.ToString());
        }

        [TestMethod]
        public void TestParentheses()
        {
            string input = "(2+3)";
            Lexer lexer = new Lexer(input);
            List<Token> tokens = lexer.Tokenize();
            Parser parser = new Parser(tokens);
            Expression expr = parser.ParseExpression();
            Assert.IsNotNull(expr);
            Assert.AreEqual("(2 + 3)", expr.ToString());
        }

        [TestMethod]
        public void TestComplexExpression()
        {
            string input = "1+2-3*4/5";
            Lexer lexer = new Lexer(input);
            List<Token> tokens = lexer.Tokenize();
            Parser parser = new Parser(tokens);
            Expression expr = parser.ParseExpression();
            Assert.IsNotNull(expr);
            Assert.AreEqual("((1 + 2) - ((3 * 4) / 5))", expr.ToString());
        }

        [TestMethod]
        public void TestLargeExpression()
        {
            int count = 100;
            StringBuilder sb = new StringBuilder();
            sb.Append("1");
            for (int i = 0; i < count; i++)
            {
                sb.Append("+1");
            }

            string input = sb.ToString();
            Lexer lexer = new Lexer(input);
            List<Token> tokens = lexer.Tokenize();
            Parser parser = new Parser(tokens);
            Expression expr = parser.ParseExpression();
            Assert.IsNotNull(expr);
            string result = expr.ToString();
            Assert.IsTrue(result.Length > 0);
            int plusCount = 0;
            foreach (char ch in result)
            {
                if (ch == '+')
                {
                    plusCount++;
                }
            }

            Assert.AreEqual(count, plusCount);
        }

        [TestMethod]
        public void TestEmptyInput()
        {
            string input = "";
            Lexer lexer = new Lexer(input);
            List<Token> tokens = lexer.Tokenize();
            Parser parser = new Parser(tokens);
            Expression expr = parser.ParseExpression();
            Assert.IsNotNull(expr);
            Assert.AreEqual("0", expr.ToString());
        }

        [TestMethod]
        public void TestUnknownCharacters()
        {
            string input = "abc";
            Lexer lexer = new Lexer(input);
            List<Token> tokens = lexer.Tokenize();
            Parser parser = new Parser(tokens);
            Expression expr = parser.ParseExpression();
            Assert.IsNotNull(expr);
            Assert.AreEqual("0", expr.ToString());
        }
    }
}