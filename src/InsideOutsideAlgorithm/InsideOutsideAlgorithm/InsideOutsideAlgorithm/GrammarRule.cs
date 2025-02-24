using System;

namespace InsideOutsideAlgorithm
{
    public class GrammarRule
    {
        public string LHS { get; set; }
        public string[] RHS { get; set; }
        public double Probability { get; set; }

        public GrammarRule(string lhs, string[] rhs, double probability)
        {
            LHS = lhs;
            RHS = rhs;
            Probability = probability;
        }

        public bool IsUnary
        {
            get
            {
                return RHS.Length == 1;
            }
        }

        public bool IsBinary
        {
            get
            {
                return RHS.Length == 2;
            }
        }
    }
}