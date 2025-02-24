using System;

namespace CuttingPlaneMethod
{
    public class FeasibleRegion
    {
        public double LowerBound { get; private set; }
        public double UpperBound { get; private set; }

        public FeasibleRegion(double initialLowerBound, double initialUpperBound)
        {
            LowerBound = initialLowerBound;
            UpperBound = initialUpperBound;
        }

        public double GetCandidateSolution()
        {
            return (LowerBound + UpperBound) / 2.0;
        }

        public void AddCut(double newCut)
        {
            if (newCut > LowerBound)
            {
                LowerBound = newCut;
            }
        }
    }
}