using System;

namespace HybridBfgsMethod
{
    public class HybridBfgsOptimizer
    {
        public double[] Optimize(Func<double[], double> objective, Func<double[], double[]> gradient, double[] initial, double tolerance, int maxIterations)
        {
            if (objective == null || gradient == null || initial == null)
            {
                return new double[0];
            }

            int dimension = initial.Length;
            double[] x = new double[dimension];
            for (int i = 0; i < dimension; i++)
            {
                x[i] = initial[i];
            }

            double[, ] H = MatrixMath.IdentityMatrix(dimension);
            for (int iter = 0; iter < maxIterations; iter++)
            {
                double[] grad = gradient(x);
                if (VectorMath.Norm(grad) < tolerance)
                {
                    break;
                }

                double[] p = VectorMath.Scale(MatrixMath.Multiply(H, grad), -1.0);
                double alpha = 1.0;
                double c = 1e-4;
                double f_x = objective(x);
                double dotGradP = VectorMath.Dot(grad, p);
                double[] xNew = VectorMath.Add(x, VectorMath.Scale(p, alpha));
                while (objective(xNew) > f_x + c * alpha * dotGradP)
                {
                    alpha *= 0.5;
                    xNew = VectorMath.Add(x, VectorMath.Scale(p, alpha));
                    if (alpha < 1e-8)
                    {
                        break;
                    }
                }

                double[] newGrad = gradient(xNew);
                double[] s = VectorMath.Subtract(xNew, x);
                double[] y = VectorMath.Subtract(newGrad, grad);
                double sy = VectorMath.Dot(s, y);
                if (sy < 1e-10)
                {
                    H = MatrixMath.IdentityMatrix(dimension);
                }
                else
                {
                    double rho = 1.0 / sy;
                    double[, ] I = MatrixMath.IdentityMatrix(dimension);
                    double[, ] s_y = MatrixMath.OuterProduct(s, y);
                    double[, ] y_s = MatrixMath.OuterProduct(y, s);
                    double[, ] term1 = MatrixMath.Subtract(I, MatrixMath.Scale(s_y, rho));
                    double[, ] term2 = MatrixMath.Subtract(I, MatrixMath.Scale(y_s, rho));
                    double[, ] H_term = MatrixMath.Multiply(MatrixMath.Multiply(term1, H), term2);
                    double[, ] s_s = MatrixMath.OuterProduct(s, s);
                    H = MatrixMath.Add(H_term, MatrixMath.Scale(s_s, rho));
                }

                x = xNew;
            }

            return x;
        }
    }
}