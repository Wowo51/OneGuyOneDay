using System;

namespace HybridMonteCarlo
{
    public interface IPotentialEnergy
    {
        double Potential(double[] position);
        double[] Gradient(double[] position);
    }
}