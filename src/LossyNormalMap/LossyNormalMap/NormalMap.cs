using System;

namespace LossyNormalMap
{
    public struct Normal
    {
        public float X { get; }
        public float Y { get; }
        public float Z { get; }

        public Normal(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public class NormalMap
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Normal[, ] Normals { get; private set; }

        public NormalMap(int width, int height)
        {
            Width = width;
            Height = height;
            Normals = new Normal[width, height];
        }

        public Normal GetNormal(int x, int y)
        {
            return Normals[x, y];
        }

        public void SetNormal(int x, int y, Normal normal)
        {
            Normals[x, y] = normal;
        }
    }
}