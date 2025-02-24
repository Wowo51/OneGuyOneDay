using System;

namespace FrankWolfeAlgorithm
{
    public class Vector
    {
        public double[] Values { get; }

        public Vector(double[] values)
        {
            if (values == null)
            {
                this.Values = new double[0];
            }
            else
            {
                this.Values = (double[])values.Clone();
            }
        }

        public int Dimension
        {
            get
            {
                return Values.Length;
            }
        }

        public double Norm()
        {
            double sum = 0.0;
            for (int i = 0; i < Dimension; i++)
            {
                sum += Values[i] * Values[i];
            }

            return Math.Sqrt(sum);
        }

        public double Dot(Vector other)
        {
            if (other == null || other.Dimension != Dimension)
            {
                return 0.0;
            }

            double sum = 0.0;
            for (int i = 0; i < Dimension; i++)
            {
                sum += Values[i] * other.Values[i];
            }

            return sum;
        }

        public static Vector operator +(Vector a, Vector b)
        {
            if (a == null || b == null || a.Dimension != b.Dimension)
            {
                return new Vector(new double[0]);
            }

            double[] result = new double[a.Dimension];
            for (int i = 0; i < a.Dimension; i++)
            {
                result[i] = a.Values[i] + b.Values[i];
            }

            return new Vector(result);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            if (a == null || b == null || a.Dimension != b.Dimension)
            {
                return new Vector(new double[0]);
            }

            double[] result = new double[a.Dimension];
            for (int i = 0; i < a.Dimension; i++)
            {
                result[i] = a.Values[i] - b.Values[i];
            }

            return new Vector(result);
        }

        public static Vector operator *(double scalar, Vector vector)
        {
            if (vector == null)
            {
                return new Vector(new double[0]);
            }

            double[] result = new double[vector.Dimension];
            for (int i = 0; i < vector.Dimension; i++)
            {
                result[i] = scalar * vector.Values[i];
            }

            return new Vector(result);
        }

        public static Vector operator *(Vector vector, double scalar)
        {
            return scalar * vector;
        }
    }
}