namespace OdlyzkoSchonhageAlgorithm
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using System.Threading.Tasks;

    public class OdlyzkoSchonhageCalculator
    {
        // Efficiently computes nontrivial zeros of the Riemann zeta function
        // on the critical line using parallel sampling and bisection for refinement.
        public List<Complex> ComputeZerosEfficient(double start, double end, int count)
        {
            if (end <= start || count <= 0)
            {
                return new List<Complex>();
            }

            int sampleCount = 8192;
            double[] ts = new double[sampleCount];
            double[] zValues = new double[sampleCount];
            double step = (end - start) / (sampleCount - 1);
            for (int i = 0; i < sampleCount; i++)
            {
                ts[i] = start + i * step;
            }

            Parallel.For(0, sampleCount, i =>
            {
                zValues[i] = RiemannSiegelZ(ts[i]);
            });
            List<Complex> zeros = new List<Complex>();
            double tolerance = 1e-8;
            for (int i = 0; i < sampleCount - 1; i++)
            {
                if (zValues[i] * zValues[i + 1] < 0)
                {
                    double zeroT = FindZeroBisection(RiemannSiegelZ, ts[i], ts[i + 1], tolerance);
                    zeros.Add(new Complex(0.5, zeroT));
                    if (zeros.Count == count)
                    {
                        break;
                    }
                }
            }

            return zeros;
        }

        // Retains the original ComputeZeros method (less efficient) for compatibility.
        public List<Complex> ComputeZeros(double start, double end, int count)
        {
            List<Complex> zeros = new List<Complex>();
            double sampleStep = (end - start) / (count * 100.0);
            if (sampleStep <= 0)
            {
                sampleStep = 1e-5;
            }

            double prevT = start;
            double prevValue = RiemannSiegelZ(prevT);
            double tolerance = 1e-8;
            for (double t = start + sampleStep; t <= end; t += sampleStep)
            {
                double currentValue = RiemannSiegelZ(t);
                if (prevValue * currentValue < 0)
                {
                    double zeroT = FindZeroBisection(RiemannSiegelZ, prevT, t, tolerance);
                    zeros.Add(new Complex(0.5, zeroT));
                    if (zeros.Count == count)
                    {
                        break;
                    }
                }

                prevT = t;
                prevValue = currentValue;
            }

            return zeros;
        }

        // Approximates the Riemann–Siegel theta function.
        private double RiemannSiegelTheta(double t)
        {
            return (t / 2) * Math.Log(t / (2 * Math.PI)) - (t / 2) - (Math.PI / 8);
        }

        // Computes the Z(t) function using a truncated Riemann–Siegel formula.
        // Uses parallel summation for large summation bounds.
        private double RiemannSiegelZ(double t)
        {
            double theta = RiemannSiegelTheta(t);
            int N = (int)Math.Floor(Math.Sqrt(t / (2 * Math.PI)));
            if (N < 1)
            {
                N = 1;
            }

            double sum = 0.0;
            if (N < 1000)
            {
                for (int n = 1; n <= N; n++)
                {
                    sum += Math.Cos(theta - t * Math.Log(n)) / Math.Sqrt(n);
                }
            }
            else
            {
                double parallelSum = 0.0;
                object lockObj = new object ();
                Parallel.For(1, N + 1, () => 0.0, (int n, ParallelLoopState state, double localSum) =>
                {
                    localSum += Math.Cos(theta - t * Math.Log(n)) / Math.Sqrt(n);
                    return localSum;
                }, localSum =>
                {
                    lock (lockObj)
                    {
                        parallelSum += localSum;
                    }
                });
                sum = parallelSum;
            }

            return 2 * sum;
        }

        // Uses the bisection method to find a zero of function f 
        // in the interval [a, b] where f(a) and f(b) have opposite signs.
        private double FindZeroBisection(Func<double, double> f, double a, double b, double tolerance)
        {
            double fa = f(a);
            double fb = f(b);
            double mid = a;
            for (int i = 0; i < 100; i++)
            {
                mid = (a + b) / 2;
                double fmid = f(mid);
                if (Math.Abs(b - a) < tolerance)
                {
                    break;
                }

                if (fa * fmid < 0)
                {
                    b = mid;
                    fb = fmid;
                }
                else
                {
                    a = mid;
                    fa = fmid;
                }
            }

            return mid;
        }
    }
}