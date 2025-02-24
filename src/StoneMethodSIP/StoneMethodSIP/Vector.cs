using System;

namespace StoneMethodSIP
{
    public class Vector
    {
        public int Size { get; }
        public double[] Values { get; }

        public Vector(int size)
        {
            Size = size;
            Values = new double[size];
        }

        public double Norm()
        {
            double sum = 0;
            for (int i = 0; i < Size; i++)
            {
                sum += Values[i] * Values[i];
            }

            return Math.Sqrt(sum);
        }

        public Vector Clone()
        {
            Vector copy = new Vector(Size);
            for (int i = 0; i < Size; i++)
            {
                copy.Values[i] = Values[i];
            }

            return copy;
        }

        public static Vector Add(Vector a, Vector b)
        {
            if (a.Size != b.Size)
            {
                return new Vector(a.Size);
            }

            Vector result = new Vector(a.Size);
            for (int i = 0; i < a.Size; i++)
            {
                result.Values[i] = a.Values[i] + b.Values[i];
            }

            return result;
        }

        public static Vector Subtract(Vector a, Vector b)
        {
            if (a.Size != b.Size)
            {
                return new Vector(a.Size);
            }

            Vector result = new Vector(a.Size);
            for (int i = 0; i < a.Size; i++)
            {
                result.Values[i] = a.Values[i] - b.Values[i];
            }

            return result;
        }

        public static Vector Multiply(double scalar, Vector a)
        {
            Vector result = new Vector(a.Size);
            for (int i = 0; i < a.Size; i++)
            {
                result.Values[i] = scalar * a.Values[i];
            }

            return result;
        }
    }
}