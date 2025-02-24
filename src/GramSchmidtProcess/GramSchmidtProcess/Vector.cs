using System;

namespace GramSchmidtProcess
{
    public class Vector
    {
        public double[] Components { get; private set; }

        public Vector(double[] components)
        {
            Components = components ?? new double[0];
        }

        public int Dimension
        {
            get
            {
                return Components.Length;
            }
        }

        public double Dot(Vector other)
        {
            if (other == null || other.Components == null || Dimension != other.Components.Length)
            {
                return 0;
            }

            double sum = 0;
            for (int index = 0; index < Dimension; index++)
            {
                sum += Components[index] * other.Components[index];
            }

            return sum;
        }

        public Vector Add(Vector other)
        {
            if (other == null || other.Components == null || Dimension != other.Components.Length)
            {
                return new Vector((double[])Components.Clone());
            }

            double[] result = new double[Dimension];
            for (int index = 0; index < Dimension; index++)
            {
                result[index] = Components[index] + other.Components[index];
            }

            return new Vector(result);
        }

        public Vector Subtract(Vector other)
        {
            if (other == null || other.Components == null || Dimension != other.Components.Length)
            {
                return new Vector((double[])Components.Clone());
            }

            double[] result = new double[Dimension];
            for (int index = 0; index < Dimension; index++)
            {
                result[index] = Components[index] - other.Components[index];
            }

            return new Vector(result);
        }

        public Vector Scale(double factor)
        {
            double[] result = new double[Dimension];
            for (int index = 0; index < Dimension; index++)
            {
                result[index] = Components[index] * factor;
            }

            return new Vector(result);
        }

        public double Norm()
        {
            double dotSelf = Dot(this);
            return Math.Sqrt(dotSelf);
        }

        public Vector Normalize()
        {
            double norm = Norm();
            if (norm == 0)
            {
                return new Vector((double[])Components.Clone());
            }

            return Scale(1.0 / norm);
        }
    }
}