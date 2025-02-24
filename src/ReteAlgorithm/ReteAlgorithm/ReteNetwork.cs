using System;
using System.Collections.Generic;

namespace ReteAlgorithm
{
    public class ReteNetwork
    {
        private List<ProductionRule> _rules;
        private Dictionary<Condition, AlphaNode> _alphaNodes;
        public ReteNetwork()
        {
            _rules = new List<ProductionRule>();
            _alphaNodes = new Dictionary<Condition, AlphaNode>();
        }

        public void AddRule(ProductionRule rule)
        {
            if (rule != null)
            {
                _rules.Add(rule);
                foreach (Condition condition in rule.Conditions)
                {
                    if (condition != null && !_alphaNodes.ContainsKey(condition))
                    {
                        _alphaNodes.Add(condition, new AlphaNode(condition));
                    }
                }
            }
        }

        public void AddFact(Fact fact)
        {
            if (fact != null)
            {
                foreach (AlphaNode node in _alphaNodes.Values)
                {
                    node.Propagate(fact);
                }
            }
        }

        public void Evaluate()
        {
            foreach (ProductionRule rule in _rules)
            {
                if (rule.Conditions.Count == 0)
                {
                    rule.Action();
                    continue;
                }

                Boolean ruleMatches = true;
                foreach (Condition condition in rule.Conditions)
                {
                    if (condition == null || !_alphaNodes.ContainsKey(condition) || _alphaNodes[condition].Memory.Count == 0)
                    {
                        ruleMatches = false;
                        break;
                    }
                }

                if (ruleMatches)
                {
                    rule.Action();
                }
            }
        }
    }
}