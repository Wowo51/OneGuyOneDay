using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ReteAlgorithm;

namespace ReteAlgorithmTest
{
    [TestClass]
    public class ConditionTests
    {
        [TestMethod]
        public void Evaluate_WithValidFact_ReturnsTrue()
        {
            Func<Fact, bool> predicate = delegate (Fact f)
            {
                return f.Identifier == "Test";
            };
            Condition condition = new Condition(predicate);
            Fact fact = new Fact("Test");
            Assert.IsTrue(condition.Evaluate(fact));
        }

        [TestMethod]
        public void Evaluate_WithInvalidFact_ReturnsFalse()
        {
            Func<Fact, bool> predicate = delegate (Fact f)
            {
                return f.Identifier == "Test";
            };
            Condition condition = new Condition(predicate);
            Fact fact = new Fact("Other");
            Assert.IsFalse(condition.Evaluate(fact));
        }

        [TestMethod]
        public void Evaluate_NullPredicate_ReturnsFalse()
        {
            Condition condition = new Condition(null);
            Fact fact = new Fact("Anything");
            Assert.IsFalse(condition.Evaluate(fact));
        }
    }

    [TestClass]
    public class AlphaNodeTests
    {
        [TestMethod]
        public void Propagate_AddsMatchingFact()
        {
            Fact fact = new Fact("X");
            Func<Fact, bool> predicate = delegate (Fact f)
            {
                return f.Identifier == "X";
            };
            Condition condition = new Condition(predicate);
            AlphaNode node = new AlphaNode(condition);
            node.Propagate(fact);
            Assert.AreEqual(1, node.Memory.Count);
        }

        [TestMethod]
        public void Propagate_DoesNotAddNonMatchingFact()
        {
            Fact fact = new Fact("Y");
            Func<Fact, bool> predicate = delegate (Fact f)
            {
                return f.Identifier == "X";
            };
            Condition condition = new Condition(predicate);
            AlphaNode node = new AlphaNode(condition);
            node.Propagate(fact);
            Assert.AreEqual(0, node.Memory.Count);
        }
    }

    [TestClass]
    public class ProductionRuleTests
    {
        [TestMethod]
        public void Matches_ReturnsTrueWhenConditionsMet()
        {
            List<Condition> conditions = new List<Condition>();
            conditions.Add(new Condition(delegate (Fact f)
            {
                return f.Identifier == "A";
            }));
            conditions.Add(new Condition(delegate (Fact f)
            {
                return f.Attributes.ContainsKey("value") && ((int)f.Attributes["value"]) > 10;
            }));
            ProductionRule rule = new ProductionRule("RuleTest", conditions, delegate
            {
            });
            Fact fact1 = new Fact("A");
            Fact fact2 = new Fact("B");
            fact2.Attributes.Add("value", 15);
            List<Fact> facts = new List<Fact>();
            facts.Add(fact1);
            facts.Add(fact2);
            Assert.IsTrue(rule.Matches(facts));
        }

        [TestMethod]
        public void Matches_ReturnsFalseWhenConditionNotMet()
        {
            List<Condition> conditions = new List<Condition>();
            conditions.Add(new Condition(delegate (Fact f)
            {
                return f.Identifier == "A";
            }));
            conditions.Add(new Condition(delegate (Fact f)
            {
                return f.Attributes.ContainsKey("value") && ((int)f.Attributes["value"]) > 10;
            }));
            ProductionRule rule = new ProductionRule("RuleTest", conditions, delegate
            {
            });
            Fact fact1 = new Fact("A");
            Fact fact2 = new Fact("B");
            List<Fact> facts = new List<Fact>();
            facts.Add(fact1);
            facts.Add(fact2);
            Assert.IsFalse(rule.Matches(facts));
        }

        [TestMethod]
        public void Execute_ExecutesActionWhenMatches()
        {
            Boolean actionExecuted = false;
            List<Condition> conditions = new List<Condition>();
            conditions.Add(new Condition(delegate (Fact f)
            {
                return f.Identifier == "Test";
            }));
            ProductionRule rule = new ProductionRule("RuleTest", conditions, delegate
            {
                actionExecuted = true;
            });
            Fact fact = new Fact("Test");
            List<Fact> facts = new List<Fact>();
            facts.Add(fact);
            rule.Execute(facts);
            Assert.IsTrue(actionExecuted);
        }

        [TestMethod]
        public void Execute_DoesNotExecuteActionWhenNotMatches()
        {
            Boolean actionExecuted = false;
            List<Condition> conditions = new List<Condition>();
            conditions.Add(new Condition(delegate (Fact f)
            {
                return f.Identifier == "Test";
            }));
            ProductionRule rule = new ProductionRule("RuleTest", conditions, delegate
            {
                actionExecuted = true;
            });
            Fact fact = new Fact("NoTest");
            List<Fact> facts = new List<Fact>();
            facts.Add(fact);
            rule.Execute(facts);
            Assert.IsFalse(actionExecuted);
        }
    }

    [TestClass]
    public class ReteNetworkTests
    {
        [TestMethod]
        public void Evaluate_ExecutesRuleActionWhenConditionsMet()
        {
            Boolean ruleExecuted = false;
            List<Condition> conditions = new List<Condition>();
            conditions.Add(new Condition(delegate (Fact f)
            {
                return f.Identifier == "A";
            }));
            ProductionRule rule = new ProductionRule("Rule1", conditions, delegate
            {
                ruleExecuted = true;
            });
            ReteNetwork network = new ReteNetwork();
            network.AddRule(rule);
            Fact fact = new Fact("A");
            network.AddFact(fact);
            network.Evaluate();
            Assert.IsTrue(ruleExecuted);
        }

        [TestMethod]
        public void Evaluate_ExecutesRuleActionWhenNoConditions()
        {
            Boolean ruleExecuted = false;
            ProductionRule rule = new ProductionRule("RuleNoCond", new List<Condition>(), delegate
            {
                ruleExecuted = true;
            });
            ReteNetwork network = new ReteNetwork();
            network.AddRule(rule);
            network.Evaluate();
            Assert.IsTrue(ruleExecuted);
        }

        [TestMethod]
        public void Evaluate_HandlesLargeDataSets()
        {
            int ruleExecutedCount = 0;
            List<Condition> conditions = new List<Condition>();
            conditions.Add(new Condition(delegate (Fact f)
            {
                return f.Attributes.ContainsKey("value") && ((int)f.Attributes["value"]) % 2 == 0;
            }));
            ProductionRule rule = new ProductionRule("LargeRule", conditions, delegate
            {
                ruleExecutedCount++;
            });
            ReteNetwork network = new ReteNetwork();
            network.AddRule(rule);
            List<Fact> facts = new List<Fact>();
            for (int i = 0; i < 100; i++)
            {
                Fact fact = new Fact("Fact" + i.ToString());
                fact.Attributes.Add("value", i);
                facts.Add(fact);
                network.AddFact(fact);
            }

            network.Evaluate();
            Assert.AreEqual(1, ruleExecutedCount);
        }

        [TestMethod]
        public void AddNullRule_NoException()
        {
            ReteNetwork network = new ReteNetwork();
            network.AddRule(null);
            network.Evaluate();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void AddNullFact_NoException()
        {
            ReteNetwork network = new ReteNetwork();
            network.AddFact(null);
            network.Evaluate();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Evaluate_ExecutesMultipleRules()
        {
            Boolean rule1Executed = false;
            Boolean rule2Executed = false;
            List<Condition> conditions1 = new List<Condition>
            {
                new Condition(delegate (Fact f)
                {
                    return f.Identifier == "A";
                })
            };
            List<Condition> conditions2 = new List<Condition>
            {
                new Condition(delegate (Fact f)
                {
                    return f.Identifier == "B";
                })
            };
            ProductionRule rule1 = new ProductionRule("Rule1", conditions1, delegate
            {
                rule1Executed = true;
            });
            ProductionRule rule2 = new ProductionRule("Rule2", conditions2, delegate
            {
                rule2Executed = true;
            });
            ReteNetwork network = new ReteNetwork();
            network.AddRule(rule1);
            network.AddRule(rule2);
            network.AddFact(new Fact("A"));
            network.AddFact(new Fact("B"));
            network.Evaluate();
            Assert.IsTrue(rule1Executed);
            Assert.IsTrue(rule2Executed);
        }
    }
}