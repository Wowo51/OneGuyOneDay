using System;
using System.Collections.Generic;

namespace ReteAlgorithm
{
    public class AlphaNode
    {
        public Condition Condition { get; }
        public List<Fact> Memory { get; }

        public AlphaNode(Condition condition)
        {
            Condition = condition;
            Memory = new List<Fact>();
        }

        public void Propagate(Fact fact)
        {
            if (fact != null && Condition.Evaluate(fact))
            {
                Memory.Add(fact);
            }
        }
    }
}