using System;
using System.Collections.Generic;

namespace BerlekampMasseyAlgorithm
{
    public static class BerlekampMassey
    {
        // Computes the minimal polynomial (connection polynomial) for the given sequence.
        // The recurrence is assumed to be: sequence[n] + c1*sequence[n-1] + ... + cL*sequence[n-L] = 0.
        // Returns the list of coefficients "C" where C[0] is 1.
        public static double[] Compute(double[] sequence)
        {
            if (sequence == null)
            {
                return new double[0];
            }

            int n = sequence.Length;
            int L = 0;
            int m = 1;
            double b = 1.0;
            List<double> C = new List<double>
            {
                1.0
            };
            List<double> B = new List<double>
            {
                1.0
            };
            for (int i = 0; i < n; i++)
            {
                double d = sequence[i];
                for (int j = 1; j <= L; j++)
                {
                    d += C[j] * sequence[i - j];
                }

                if (Math.Abs(d) < 1e-9)
                {
                    m++;
                }
                else
                {
                    List<double> T = new List<double>(C);
                    int requiredSize = m + B.Count;
                    while (C.Count < requiredSize)
                    {
                        C.Add(0.0);
                    }

                    for (int j = 0; j < B.Count; j++)
                    {
                        C[j + m] -= (d / b) * B[j];
                    }

                    if (2 * L <= i)
                    {
                        L = i + 1 - L;
                        B = T;
                        b = d;
                        m = 1;
                    }
                    else
                    {
                        m++;
                    }
                }
            }

            return C.ToArray();
        }
    }
}