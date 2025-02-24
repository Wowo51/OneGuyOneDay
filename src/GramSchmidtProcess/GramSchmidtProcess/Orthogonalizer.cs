using System;
using System.Collections.Generic;

namespace GramSchmidtProcess
{
    public static class Orthogonalizer
    {
        public static List<Vector> Orthogonalize(List<Vector> vectors)
        {
            List<Vector> orthogonalSet = new List<Vector>();
            if (vectors == null)
            {
                return orthogonalSet;
            }

            foreach (Vector current in vectors)
            {
                if (current == null || current.Components == null)
                {
                    continue;
                }

                double[] componentsCopy = (double[])current.Components.Clone();
                Vector u = new Vector(componentsCopy);
                foreach (Vector w in orthogonalSet)
                {
                    double dotUW = u.Dot(w);
                    double dotWW = w.Dot(w);
                    if (dotWW != 0)
                    {
                        double factor = dotUW / dotWW;
                        Vector projection = w.Scale(factor);
                        u = u.Subtract(projection);
                    }
                }

                if (u.Norm() > 1e-10)
                {
                    orthogonalSet.Add(u);
                }
            }

            return orthogonalSet;
        }

        public static List<Vector> Orthonormalize(List<Vector> vectors)
        {
            List<Vector> orthogonal = Orthogonalize(vectors);
            List<Vector> orthonormal = new List<Vector>();
            foreach (Vector u in orthogonal)
            {
                orthonormal.Add(u.Normalize());
            }

            return orthonormal;
        }
    }
}