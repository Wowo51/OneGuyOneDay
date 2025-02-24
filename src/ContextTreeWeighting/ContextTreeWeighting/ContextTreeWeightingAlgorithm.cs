using System;

namespace ContextTreeWeighting
{
    public class ContextTreeWeightingAlgorithm
    {
        private CTWNode _root;
        public int MaxDepth { get; private set; }

        public ContextTreeWeightingAlgorithm(int maxDepth)
        {
            MaxDepth = maxDepth;
            _root = new CTWNode(0, maxDepth);
        }

        public void Update(string context, int bit)
        {
            _root.Update(context, bit);
        }

        public double GetWeightedProbability(string context)
        {
            CTWNode currentNode = _root;
            for (int index = 0; index < context.Length; index++)
            {
                char symbol = context[index];
                if (symbol == '0')
                {
                    if (currentNode.Child0 == null)
                    {
                        return currentNode.GetWeightedProbability();
                    }

                    currentNode = currentNode.Child0;
                }
                else if (symbol == '1')
                {
                    if (currentNode.Child1 == null)
                    {
                        return currentNode.GetWeightedProbability();
                    }

                    currentNode = currentNode.Child1;
                }
            }

            return currentNode.GetWeightedProbability();
        }
    }

    public class CTWNode
    {
        public int Count0 { get; set; }
        public int Count1 { get; set; }
        public CTWNode? Child0 { get; set; }
        public CTWNode? Child1 { get; set; }
        public int Depth { get; private set; }
        public int MaxDepth { get; private set; }

        public CTWNode(int depth, int maxDepth)
        {
            Depth = depth;
            MaxDepth = maxDepth;
            Count0 = 0;
            Count1 = 0;
            Child0 = null;
            Child1 = null;
        }

        public void Update(string context, int bit)
        {
            if (bit == 0)
            {
                Count0++;
            }
            else
            {
                Count1++;
            }

            if (context.Length > 0 && Depth < MaxDepth)
            {
                char nextSymbol = context[0];
                string remainingContext = context.Substring(1);
                if (nextSymbol == '0')
                {
                    if (Child0 == null)
                    {
                        Child0 = new CTWNode(Depth + 1, MaxDepth);
                    }

                    Child0.Update(remainingContext, bit);
                }
                else if (nextSymbol == '1')
                {
                    if (Child1 == null)
                    {
                        Child1 = new CTWNode(Depth + 1, MaxDepth);
                    }

                    Child1.Update(remainingContext, bit);
                }
            }
        }

        public double GetWeightedProbability()
        {
            if (Depth >= MaxDepth)
            {
                return KTProbabilityAdjusted();
            }

            double leftProbability = (Child0 != null) ? Child0.GetWeightedProbability() : KTProbabilityForEmpty();
            double rightProbability = (Child1 != null) ? Child1.GetWeightedProbability() : KTProbabilityForEmpty();
            double pKT = KTProbabilityAdjusted();
            double weighted = 0.5 * pKT + 0.5 * (leftProbability * rightProbability);
            return weighted;
        }

        private double KTProbabilityForEmpty()
        {
            // With no data, the KT estimator (for (0,0)) returns 1.
            return 1.0;
        }

        private double KTProbabilityAdjusted()
        {
            double total = Count0 + Count1;
            double logNumerator = LnGamma(Count0 + 0.5) + LnGamma(Count1 + 0.5);
            double logDenom = LnGamma(total + 1.0) + 2 * LnGamma(0.5);
            double probability = Math.Exp(logNumerator - logDenom);
            return probability;
        }

        private static double LnGamma(double x)
        {
            double[] coef = new double[]
            {
                76.18009172947146,
                -86.50532032941677,
                24.01409824083091,
                -1.231739572450155,
                0.001208650973866179,
                -0.000005395239384953
            };
            double y = x;
            double temp = x + 5.5;
            temp -= (x + 0.5) * Math.Log(temp);
            double ser = 1.000000000190015;
            for (int i = 0; i < coef.Length; i++)
            {
                y += 1.0;
                ser += coef[i] / y;
            }

            return -temp + Math.Log(2.5066282746310005 * ser / x);
        }
    }
}