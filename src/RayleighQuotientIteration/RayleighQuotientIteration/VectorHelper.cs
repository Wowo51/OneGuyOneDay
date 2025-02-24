namespace RayleighQuotientIteration
{
    public static class VectorHelper
    {
        public static double Dot(double[] a, double[] b)
        {
            int length = a.Length;
            double result = 0.0;
            for (int i = 0; i < length; i++)
            {
                result += a[i] * b[i];
            }

            return result;
        }

        public static double Norm(double[] a)
        {
            double dot = Dot(a, a);
            return System.Math.Sqrt(dot);
        }

        public static double[] Normalize(double[] a)
        {
            double norm = Norm(a);
            if (norm < 1e-12)
            {
                double[] copy = new double[a.Length];
                for (int i = 0; i < a.Length; i++)
                {
                    copy[i] = a[i];
                }

                return copy;
            }

            double[] normalized = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                normalized[i] = a[i] / norm;
            }

            return normalized;
        }
    }
}