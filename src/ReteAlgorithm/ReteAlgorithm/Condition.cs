using System;

namespace ReteAlgorithm
{
    public class Condition
    {
        public Func<Fact, bool> Predicate { get; }

        public Condition(Func<Fact, bool> predicate)
        {
            if (predicate == null)
            {
                Predicate = delegate (Fact f)
                {
                    return false;
                };
            }
            else
            {
                Predicate = predicate;
            }
        }

        public bool Evaluate(Fact fact)
        {
            if (fact == null)
            {
                return false;
            }

            return Predicate(fact);
        }
    }
}