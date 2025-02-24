using System;

namespace DifferenceMapAlgorithm
{
    public class DifferenceMap
    {
        private double _beta;
        private IProjection _projectionA;
        private IProjection _projectionB;
        public DifferenceMap(double beta, IProjection projectionA, IProjection projectionB)
        {
            _beta = beta;
            if (projectionA == null)
            {
                _projectionA = new IdentityProjection();
            }
            else
            {
                _projectionA = projectionA;
            }

            if (projectionB == null)
            {
                _projectionB = new IdentityProjection();
            }
            else
            {
                _projectionB = projectionB;
            }
        }

        public double[] Solve(double[] initial, int maxIterations, double tolerance)
        {
            if (initial == null)
            {
                initial = new double[0];
            }

            double[] x = (double[])initial.Clone();
            int iteration = 0;
            while (iteration < maxIterations)
            {
                double[] pB = _projectionB.Project(x);
                double[] f = new double[x.Length];
                for (int i = 0; i < x.Length; i++)
                {
                    f[i] = 2.0 * pB[i] - x[i];
                }

                double[] pA = _projectionA.Project(f);
                double[] xNew = new double[x.Length];
                double norm = 0.0;
                for (int i = 0; i < x.Length; i++)
                {
                    xNew[i] = x[i] + _beta * (pA[i] - pB[i]);
                    double diff = xNew[i] - x[i];
                    norm += diff * diff;
                }

                norm = System.Math.Sqrt(norm);
                if (norm < tolerance)
                {
                    return xNew;
                }

                x = xNew;
                iteration++;
            }

            return x;
        }
    }

    public class IdentityProjection : IProjection
    {
        public double[] Project(double[] input)
        {
            if (input == null)
            {
                return new double[0];
            }

            return (double[])input.Clone();
        }
    }
}