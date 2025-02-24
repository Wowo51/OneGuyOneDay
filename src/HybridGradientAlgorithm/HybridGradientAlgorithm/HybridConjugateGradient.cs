using System;

namespace HybridGradientAlgorithm
{
    public class HybridConjugateGradient
    {
        public double[] Solve(double[] initialGuess)
        {
            if (initialGuess == null)
            {
                initialGuess = new double[0];
            }

            HarmonySearch harmony = new HarmonySearch();
            double[] hsSolution = harmony.Optimize(initialGuess);
            LineSearch ls = new LineSearch();
            double factor = ls.Perform(hsSolution);
            ConjugateGradient cg = new ConjugateGradient();
            double[] finalSolution = cg.Search(hsSolution, factor);
            return finalSolution;
        }
    }
}