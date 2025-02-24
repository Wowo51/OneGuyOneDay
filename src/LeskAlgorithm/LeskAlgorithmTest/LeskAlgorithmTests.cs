using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LeskAlgorithm;

namespace LeskAlgorithmTest
{
    [TestClass]
    public class LeskAlgorithmTests
    {
        [TestMethod]
        public void Disambiguate_ReturnsNull_WhenContextIsEmpty()
        {
            string context = "  ";
            Dictionary<string, string> definitions = new Dictionary<string, string>();
            definitions.Add("sense1", "Some definition text.");
            string? result = LeskDisambiguator.Disambiguate(context, definitions);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Disambiguate_ReturnsNull_WhenDefinitionsEmpty()
        {
            string context = "Some context text.";
            Dictionary<string, string> definitions = new Dictionary<string, string>();
            string? result = LeskDisambiguator.Disambiguate(context, definitions);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Disambiguate_ReturnsNull_WhenDefinitionsIsNull()
        {
            string context = "Some context text.";
            string? result = LeskDisambiguator.Disambiguate(context, null);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Disambiguate_ReturnsBestSense_ForSimpleOverlap()
        {
            string context = "The bank can secure your deposits.";
            Dictionary<string, string> definitions = new Dictionary<string, string>()
            {
                {
                    "financial",
                    "Financial institution where bank transactions occur."
                },
                {
                    "edge",
                    "A bank edge that can hold water."
                }
            };
            string? result = LeskDisambiguator.Disambiguate(context, definitions);
            Assert.AreEqual("edge", result);
        }

        [TestMethod]
        public void Disambiguate_ReturnsFirstSense_OnTie()
        {
            string context = "alpha beta";
            Dictionary<string, string> definitions = new Dictionary<string, string>()
            {
                {
                    "senseA",
                    "alpha gamma"
                },
                {
                    "senseB",
                    "beta gamma"
                }
            };
            string? result = LeskDisambiguator.Disambiguate(context, definitions);
            Assert.AreEqual("senseA", result);
        }

        [TestMethod]
        public void Disambiguate_IsCaseInsensitive()
        {
            string context = "Bank and deposit";
            Dictionary<string, string> definitions = new Dictionary<string, string>()
            {
                {
                    "financial",
                    "bank and deposit"
                }
            };
            string? result = LeskDisambiguator.Disambiguate(context, definitions);
            Assert.AreEqual("financial", result);
        }

        [TestMethod]
        public void Disambiguate_HandlesLargeInput()
        {
            StringBuilder contextBuilder = new StringBuilder();
            for (int i = 0; i < 1000; i++)
            {
                contextBuilder.Append("word" + (i % 10) + " ");
            }

            string context = contextBuilder.ToString();
            Dictionary<string, string> definitions = new Dictionary<string, string>();
            for (int j = 0; j < 50; j++)
            {
                StringBuilder defBuilder = new StringBuilder();
                for (int k = 0; k < 100; k++)
                {
                    defBuilder.Append("word" + (k % 10) + " ");
                }

                definitions.Add("sense" + j, defBuilder.ToString());
            }

            string? result = LeskDisambiguator.Disambiguate(context, definitions);
            Assert.IsNotNull(result);
        }
    }
}