using System;

namespace EllipsoidMethod
{
    public class EllipsoidSolver
    {
        private int Dimension;
        private double[] CurrentCenter;
        private double[, ] Q;
        public EllipsoidSolver(int dimension, double[] initialCenter, double[, ] initialShape)
        {
            this.Dimension = dimension;
            this.CurrentCenter = new double[dimension];
            for (int i = 0; i < dimension; i++)
            {
                this.CurrentCenter[i] = initialCenter[i];
            }

            this.Q = new double[dimension, dimension];
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    this.Q[i, j] = initialShape[i, j];
                }
            }
        }

        // The Solve method applies the ellipsoid update iteratively using a separation oracle.
        // The oracle returns null if the current center is feasible.
        public double[] Solve(Func<double[], double[]?> separationOracle)
        {
            int maxIterations = 100;
            for (int iteration = 0; iteration < maxIterations; iteration++)
            {
                double[]? oracleResult = separationOracle(this.CurrentCenter);
                if (oracleResult == null)
                {
                    return this.CurrentCenter;
                }

                double[] g = oracleResult;
                double[] Qg = MatrixHelper.MultiplyMatrixVector(this.Q, g);
                double gTQg = MatrixHelper.InnerProduct(g, Qg);
                if (gTQg <= 0.0)
                {
                    return this.CurrentCenter;
                }

                double factor = 1.0 / Math.Sqrt(gTQg);
                double alpha = 1.0 / (this.Dimension + 1.0);
                double[] updatedCenter = new double[this.Dimension];
                for (int i = 0; i < this.Dimension; i++)
                {
                    updatedCenter[i] = this.CurrentCenter[i] - alpha * Qg[i] * factor;
                }

                double beta = 2.0 / (this.Dimension + 1.0);
                double[, ] outer = MatrixHelper.OuterProduct(Qg, Qg);
                double[, ] scaledOuter = MatrixHelper.ScaleMatrix(outer, beta / gTQg);
                double[, ] newQIntermediate = MatrixHelper.SubtractMatrices(this.Q, scaledOuter);
                double scale = (this.Dimension * this.Dimension) / (this.Dimension * this.Dimension - 1.0);
                double[, ] updatedQ = MatrixHelper.ScaleMatrix(newQIntermediate, scale);
                this.CurrentCenter = updatedCenter;
                this.Q = updatedQ;
            }

            return this.CurrentCenter;
        }
    }
}