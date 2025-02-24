using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using AssociationRuleLearning;

namespace AssociationRuleLearningTest
{
    [TestClass]
    public class AprioriTests
    {
        [TestMethod]
        public void TestGenerateAssociationRules_NullTransactions()
        {
            List<List<string>>? transactions = null;
            double minSupport = 0.5;
            double minConfidence = 0.6;
            List<AssociationRule> rules = Apriori.GenerateAssociationRules(transactions, minSupport, minConfidence);
            Assert.IsNotNull(rules);
            Assert.AreEqual(0, rules.Count);
        }

        [TestMethod]
        public void TestGenerateAssociationRules_EmptyTransactions()
        {
            List<List<string>> transactions = new List<List<string>>();
            double minSupport = 0.5;
            double minConfidence = 0.6;
            List<AssociationRule> rules = Apriori.GenerateAssociationRules(transactions, minSupport, minConfidence);
            Assert.IsNotNull(rules);
            Assert.AreEqual(0, rules.Count);
        }

        [TestMethod]
        public void TestGenerateAssociationRules_SimpleScenario()
        {
            List<List<string>> transactions = new List<List<string>>
            {
                new List<string>
                {
                    "A",
                    "B",
                    "C"
                },
                new List<string>
                {
                    "A",
                    "B"
                },
                new List<string>
                {
                    "A",
                    "C"
                },
                new List<string>
                {
                    "B",
                    "C"
                }
            };
            double minSupport = 0.5;
            double minConfidence = 0.6;
            List<AssociationRule> rules = Apriori.GenerateAssociationRules(transactions, minSupport, minConfidence);
            Assert.IsNotNull(rules);
            // Each 2-item frequent set generates 2 association rules; expect 3 frequent pairs to yield 6 rules.
            Assert.AreEqual(6, rules.Count);
            // Verify that a specific rule exists: A -> B.
            Boolean ruleExists = false;
            foreach (AssociationRule rule in rules)
            {
                if (rule.Antecedent.SequenceEqual(new List<string> { "A" }) && rule.Consequent.SequenceEqual(new List<string> { "B" }))
                {
                    ruleExists = true;
                    // Expected support for {A,B} is 2/4 = 0.5 and confidence for A->B is approximately 0.666.
                    Assert.AreEqual(0.5, rule.Support, 0.01);
                    Assert.AreEqual(0.666, rule.Confidence, 0.01);
                }
            }

            Assert.IsTrue(ruleExists);
        }

        [TestMethod]
        public void TestGenerateAssociationRules_SyntheticLargeDataset()
        {
            List<List<string>> transactions = new List<List<string>>();
            string[] items = new string[]
            {
                "A",
                "B",
                "C",
                "D",
                "E"
            };
            Random random = new Random(123);
            for (int i = 0; i < 100; i++)
            {
                List<string> transaction = new List<string>();
                List<string> availableItems = new List<string>(items);
                int itemsToPick = 3;
                for (int j = 0; j < itemsToPick; j++)
                {
                    int index = random.Next(availableItems.Count);
                    transaction.Add(availableItems[index]);
                    availableItems.RemoveAt(index);
                }

                transaction.Sort();
                transactions.Add(transaction);
            }

            double minSupport = 0.2;
            double minConfidence = 0.5;
            List<AssociationRule> rules = Apriori.GenerateAssociationRules(transactions, minSupport, minConfidence);
            Assert.IsNotNull(rules);
            // With synthetic data there should be at least one association rule generated.
            Assert.IsTrue(rules.Count > 0);
        }
    }
}