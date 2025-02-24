using System;

namespace BfgsMethod
{
    public class BfgsOptimizer
    {
        public double[] Optimize(IObjectiveFunction function, double[] initialPoint, double tolerance = 1e-6, int maxIterations = 1000)
        {
            int n = initialPoint.Length;
            double[] x = new double[n];
            for (int i = 0; i < n; i++)
            {
                x[i] = initialPoint[i];
            }

            double[][] H = MathUtils.IdentityMatrix(n);
            double[] grad = function.Gradient(x);
            int iter = 0;
            while (MathUtils.Norm(grad) > tolerance && iter < maxIterations)
            {
                double[] p = MathUtils.MultiplyMatrixVector(H, grad);
                p = MathUtils.Scale(p, -1.0);
                double alpha = LineSearch(function, x, grad, p);
                double[] s = MathUtils.Scale(p, alpha);
                double[] xNew = MathUtils.Add(x, s);
                double[] gradNew = function.Gradient(xNew);
                double[] y = MathUtils.Subtract(gradNew, grad);
                double dotSy = MathUtils.Dot(s, y);
                if (System.Math.Abs(dotSy) < 1e-10)
                {
                    break;
                }

                double rho = 1.0 / dotSy;
                double[][] I = MathUtils.IdentityMatrix(n);
                double[][] syT = MathUtils.OuterProduct(s, y);
                double[][] ysT = MathUtils.OuterProduct(y, s);
                double[][] term1 = MathUtils.SubtractMatrices(I, MathUtils.ScaleMatrix(syT, rho));
                double[][] term2 = MathUtils.SubtractMatrices(I, MathUtils.ScaleMatrix(ysT, rho));
                double[][] Hterm = MathUtils.MultiplyMatrices(MathUtils.MultiplyMatrices(term1, H), term2);
                double[][] ssT = MathUtils.OuterProduct(s, s);
                double[][] newH = MathUtils.AddMatrices(Hterm, MathUtils.ScaleMatrix(ssT, rho));
                x = xNew;
                grad = gradNew;
                H = newH;
                iter++;
            }

            return x;
        }

        private double LineSearch(IObjectiveFunction function, double[] x, double[] grad, double[] p)
        {
            double alpha = 1.0;
            double c = 1e-4;
            double f0 = function.Value(x);
            double[] xNew = MathUtils.Add(x, MathUtils.Scale(p, alpha));
            while (function.Value(xNew) > f0 + c * alpha * MathUtils.Dot(grad, p))
            {
                alpha *= 0.5;
                xNew = MathUtils.Add(x, MathUtils.Scale(p, alpha));
                if (alpha < 1e-8)
                {
                    break;
                }
            }

            return alpha;
        }
    }
}