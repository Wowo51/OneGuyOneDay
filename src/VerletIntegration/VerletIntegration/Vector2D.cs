using System;

namespace VerletIntegration
{
    public struct Vector2D
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Vector2D operator +(Vector2D left, Vector2D right)
        {
            return new Vector2D(left.X + right.X, left.Y + right.Y);
        }

        public static Vector2D operator -(Vector2D left, Vector2D right)
        {
            return new Vector2D(left.X - right.X, left.Y - right.Y);
        }

        public static Vector2D operator *(Vector2D vector, double scalar)
        {
            return new Vector2D(vector.X * scalar, vector.Y * scalar);
        }

        public static Vector2D operator *(double scalar, Vector2D vector)
        {
            return new Vector2D(vector.X * scalar, vector.Y * scalar);
        }
    }
}