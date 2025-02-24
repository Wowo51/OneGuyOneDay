using System;

namespace ElserDifferenceMap
{
    public class DifferenceMapSolver
    {
        public IProjection ProjectionA { get; }
        public IProjection ProjectionB { get; }
        public double Beta { get; }
        public int MaxIterations { get; }
        public double Tolerance { get; }

        public DifferenceMapSolver(IProjection projectionA, IProjection projectionB, double beta, int maxIterations, double tolerance)
        {
            if (projectionA == null)
            {
                projectionA = new IdentityProjection();
            }

            if (projectionB == null)
            {
                projectionB = new IdentityProjection();
            }

            this.ProjectionA = projectionA;
            this.ProjectionB = projectionB;
            this.Beta = beta;
            this.MaxIterations = maxIterations;
            this.Tolerance = tolerance;
        }

        public double[] Solve(double[] x0)
        {
            if (x0 == null)
            {
                return new double[0];
            }

            double[] x = new double[x0.Length];
            for (int i = 0; i < x0.Length; i++)
            {
                x[i] = x0[i];
            }

            for (int iteration = 0; iteration < this.MaxIterations; iteration++)
            {
                double[] pb = this.ProjectionB.Project(x);
                if (pb == null)
                {
                    pb = new double[x.Length];
                }

                double[] twoPbMinusX = new double[x.Length];
                for (int i = 0; i < x.Length; i++)
                {
                    twoPbMinusX[i] = 2 * pb[i] - x[i];
                }

                double[] pa = this.ProjectionA.Project(twoPbMinusX);
                if (pa == null)
                {
                    pa = new double[x.Length];
                }

                double[] delta = new double[x.Length];
                double norm = 0;
                for (int i = 0; i < x.Length; i++)
                {
                    delta[i] = pa[i] - pb[i];
                    norm += delta[i] * delta[i];
                }

                norm = Math.Sqrt(norm);
                if (norm < this.Tolerance)
                {
                    return pb;
                }

                for (int i = 0; i < x.Length; i++)
                {
                    x[i] = x[i] + this.Beta * delta[i];
                }
            }

            return this.ProjectionB.Project(x);
        }
    }
}