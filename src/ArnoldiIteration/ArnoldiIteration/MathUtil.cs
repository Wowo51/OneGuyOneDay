using System;

namespace ArnoldiIteration
{
    public static class MathUtil
    {
        // Computes the dot product of two vectors.
        public static double Dot(double[] vector1, double[] vector2)
        {
            int length = vector1.Length;
            double result = 0;
            for (int i = 0; i < length; i++)
            {
                result += vector1[i] * vector2[i];
            }

            return result;
        }

        // Computes the Euclidean norm of a vector.
        public static double Norm(double[] vector)
        {
            return Math.Sqrt(Dot(vector, vector));
        }
    }
}