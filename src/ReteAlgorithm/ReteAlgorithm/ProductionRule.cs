using System;
using System.Collections.Generic;

namespace ReteAlgorithm
{
    public class ProductionRule
    {
        public string Name { get; set; }
        public List<Condition> Conditions { get; }
        public Action Action { get; }

        public ProductionRule(string name, List<Condition> conditions, Action action)
        {
            Name = name;
            Conditions = conditions != null ? conditions : new List<Condition>();
            Action = action != null ? action : delegate
            {
            };
        }

        public bool Matches(List<Fact> facts)
        {
            if (facts == null)
            {
                return false;
            }

            foreach (Condition condition in Conditions)
            {
                bool conditionMet = false;
                foreach (Fact fact in facts)
                {
                    if (condition.Evaluate(fact))
                    {
                        conditionMet = true;
                        break;
                    }
                }

                if (!conditionMet)
                {
                    return false;
                }
            }

            return true;
        }

        public void Execute(List<Fact> facts)
        {
            if (Matches(facts))
            {
                Action();
            }
        }
    }
}