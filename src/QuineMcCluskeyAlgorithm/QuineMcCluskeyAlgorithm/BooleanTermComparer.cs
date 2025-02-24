using System;
using System.Collections.Generic;

namespace QuineMcCluskeyAlgorithm
{
    public class BooleanTermComparer : IEqualityComparer<BooleanTerm>
    {
        public bool Equals(BooleanTerm x, BooleanTerm y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return x.Term == y.Term;
        }

        public int GetHashCode(BooleanTerm obj)
        {
            return (obj.Term != null ? obj.Term.GetHashCode() : 0);
        }
    }
}