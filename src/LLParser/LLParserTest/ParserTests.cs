using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LLParser;

namespace LLParserTest
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void TestEmptyInput()
        {
            Parser parser = new Parser("");
            ParseNode node = parser.Parse();
            Assert.AreEqual("Error", node.NodeType, "Empty input should return an error node.");
            Assert.AreEqual("Number expected", node.Value);
        }

        [TestMethod]
        public void TestNullInput()
        {
            Parser parser = new Parser(null);
            ParseNode node = parser.Parse();
            Assert.AreEqual("Error", node.NodeType, "Null input should return an error node.");
            Assert.AreEqual("Number expected", node.Value);
        }

        [TestMethod]
        public void TestValidNumber()
        {
            Parser parser = new Parser("123");
            ParseNode node = parser.Parse();
            Assert.AreEqual("Number", node.NodeType);
            Assert.AreEqual("123", node.Value);
            Assert.AreEqual(0, node.Children.Count);
        }

        [TestMethod]
        public void TestAddition()
        {
            Parser parser = new Parser("123+456");
            ParseNode node = parser.Parse();
            Assert.AreEqual("Expression", node.NodeType);
            Assert.AreEqual(3, node.Children.Count);
            ParseNode child1 = node.Children[0];
            ParseNode child2 = node.Children[1];
            ParseNode child3 = node.Children[2];
            Assert.AreEqual("Number", child1.NodeType);
            Assert.AreEqual("123", child1.Value);
            Assert.AreEqual("Plus", child2.NodeType);
            Assert.AreEqual("+", child2.Value);
            Assert.AreEqual("Number", child3.NodeType);
            Assert.AreEqual("456", child3.Value);
        }

        [TestMethod]
        public void TestTrailingUnexpectedInput()
        {
            Parser parser = new Parser("123a");
            ParseNode node = parser.Parse();
            Assert.AreEqual("Number", node.NodeType);
            Assert.AreEqual("123", node.Value);
            Assert.AreEqual(1, node.Children.Count);
            ParseNode errorChild = node.Children[0];
            Assert.AreEqual("Error", errorChild.NodeType);
            Assert.AreEqual("Unexpected input", errorChild.Value);
        }

        [TestMethod]
        public void TestComplexExpression()
        {
            Parser parser = new Parser("1 +2+ 3");
            ParseNode node = parser.Parse();
            Assert.AreEqual("Expression", node.NodeType);
            Assert.AreEqual(3, node.Children.Count);
            ParseNode innerExpr = node.Children[0];
            Assert.AreEqual("Expression", innerExpr.NodeType);
            Assert.AreEqual(3, innerExpr.Children.Count);
            Assert.AreEqual("Number", innerExpr.Children[0].NodeType);
            Assert.AreEqual("1", innerExpr.Children[0].Value);
            Assert.AreEqual("Plus", innerExpr.Children[1].NodeType);
            Assert.AreEqual("+", innerExpr.Children[1].Value);
            Assert.AreEqual("Number", innerExpr.Children[2].NodeType);
            Assert.AreEqual("2", innerExpr.Children[2].Value);
            Assert.AreEqual("Plus", node.Children[1].NodeType);
            Assert.AreEqual("Number", node.Children[2].NodeType);
            Assert.AreEqual("3", node.Children[2].Value);
        }

        [TestMethod]
        public void TestLargeExpression()
        {
            StringBuilder sb = new StringBuilder();
            Int32 count = 1000;
            for (Int32 i = 0; i < count; i++)
            {
                if (i > 0)
                {
                    sb.Append("+");
                }

                sb.Append("1");
            }

            Parser parser = new Parser(sb.ToString());
            ParseNode node = parser.Parse();
            Queue<ParseNode> queue = new Queue<ParseNode>();
            queue.Enqueue(node);
            bool foundError = false;
            while (queue.Count > 0)
            {
                ParseNode current = queue.Dequeue();
                if (current.NodeType == "Error")
                {
                    foundError = true;
                    break;
                }

                for (Int32 j = 0; j < current.Children.Count; j++)
                {
                    queue.Enqueue(current.Children[j]);
                }
            }

            Assert.IsFalse(foundError, "Error node found in large expression parse tree.");
        }
    }
}