using System;

namespace ItpMethod
{
    public static class ItpMethodSolver
    {
        public static double Solve(Func<double, double> f, double left, double right, double tol, int maxIter)
        {
            if (f == null)
            {
                return double.NaN;
            }

            double fLeft = f(left);
            double fRight = f(right);
            // Ensure the bracket contains a root.
            if (fLeft * fRight > 0)
            {
                return double.NaN;
            }

            if (fLeft == 0.0)
            {
                return left;
            }

            if (fRight == 0.0)
            {
                return right;
            }

            int iter = 0;
            while ((right - left) > tol && iter < maxIter)
            {
                double mid = (left + right) / 2.0;
                double fMid = f(mid);
                if (Math.Abs(fMid) <= tol)
                {
                    return mid;
                }

                // Compute candidate using secant method.
                double candidate = left - fLeft * (right - left) / (fRight - fLeft);
                // Truncate candidate to lie within a quarter interval around mid.
                double delta = (right - left) / 4.0;
                double diff = candidate - mid;
                candidate = mid + (diff >= 0 ? 1.0 : -1.0) * Math.Min(Math.Abs(diff), delta);
                double fCandidate = f(candidate);
                if (Math.Abs(fCandidate) <= tol)
                {
                    return candidate;
                }

                // Update interval maintaining the bracketing condition.
                if (Math.Sign(fCandidate) == Math.Sign(fLeft))
                {
                    left = candidate;
                    fLeft = fCandidate;
                }
                else
                {
                    right = candidate;
                    fRight = fCandidate;
                }

                iter = iter + 1;
            }

            return (left + right) / 2.0;
        }
    }
}