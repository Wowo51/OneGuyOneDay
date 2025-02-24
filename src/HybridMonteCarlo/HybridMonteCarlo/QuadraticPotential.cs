using System;

namespace HybridMonteCarlo
{
    public class QuadraticPotential : IPotentialEnergy
    {
        public double Potential(double[] position)
        {
            double potential = 0.0;
            for (int i = 0; i < position.Length; i++)
            {
                potential += 0.5 * position[i] * position[i];
            }

            return potential;
        }

        public double[] Gradient(double[] position)
        {
            int dim = position.Length;
            double[] gradient = new double[dim];
            for (int i = 0; i < dim; i++)
            {
                gradient[i] = position[i];
            }

            return gradient;
        }
    }
}